// -----------------------------------------------------------------------
// <copyright file="ConsulHostedService.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.WebAPI.ServiceRegistry
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Consul;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The consul hosted service that is used for registration to service registry.
    /// </summary>
    /// <seealso cref="IHostedService" />
    public class ConsulHostedService : IHostedService
    {
        /// <summary>
        /// The Consul client.
        /// </summary>
        private readonly IConsulClient _consulClient;

        /// <summary>
        /// The Consul configuration.
        /// </summary>
        private readonly ConsulServiceConfiguration _consulConfig;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<ConsulHostedService> _logger;

        /// <summary>
        /// The cancellation token source.
        /// </summary>
        private CancellationTokenSource _cts;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsulHostedService"/> class.
        /// </summary>
        /// <param name="consulClient">The consul client.</param>
        /// <param name="consulConfig">The consul configuration.</param>
        /// <param name="logger">The logger.</param>
        public ConsulHostedService(IConsulClient consulClient, ConsulServiceConfiguration consulConfig, ILogger<ConsulHostedService> logger)
        {
            this._logger = logger;
            this._consulConfig = consulConfig;
            this._consulClient = consulClient;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns>A task.</returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // create a registration object.
            var registration = new AgentServiceRegistration
            {
                ID = this._consulConfig.ServiceId,
                Name = this._consulConfig.ServiceName,
                Address = this._consulConfig.ServiceAddress,
                Port = this._consulConfig.Port,
                Tags = this._consulConfig.Tags.ToArray(),
                Meta = this._consulConfig.Meta,
                Checks = new[]
                {
                    new AgentServiceCheck
                    {
                        Interval = TimeSpan.FromSeconds(this._consulConfig.LiveCheckInSeconds),
                        HTTP = $"https://{this._consulConfig.ServiceAddress}/health",
                        TLSSkipVerify = true
                    }
                }
            };

            try
            {
                // Create a linked token so we can trigger cancellation outside of this token's cancellation
                this._cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

                this._logger.LogInformation($"Registering {this._consulConfig.ServiceName} [{this._consulConfig.ServiceAddress}:{this._consulConfig.Port}] in Consul");

                // make sure that the service is not registered in Consul.
                await this._consulClient.Agent.ServiceDeregister(registration.ID, this._cts.Token);

                // finally, register into Consul.
                await this._consulClient.Agent.ServiceRegister(registration, this._cts.Token);
            }
            catch
            {
                // make sure that the service is not registered in Consul.
                await this._consulClient.Agent.ServiceDeregister(registration.ID, this._cts.Token);

                throw;
            }
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns>A task.</returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                this._cts.Cancel();
                this._logger.LogInformation("Removing registration from Consul.");
                await this._consulClient.Agent.ServiceDeregister(this._consulConfig.ServiceId, cancellationToken);
            }
            catch (HttpRequestException ex)
            {
                this._logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to remove registration from Consul.");

                throw;
            }
        }
    }
}