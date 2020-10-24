// -----------------------------------------------------------------------
// <copyright file="GetUserConsumer.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.ServiceBus.Consumers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Devkit.Communication.Security.DTOs;
    using Devkit.Communication.Security.Messages;
    using Devkit.Security.Data.Models;
    using Devkit.ServiceBus;
    using Devkit.ServiceBus.Exceptions;
    using MassTransit;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The GetUserConsumer consumes and handles the message IGetUser.
    /// </summary>
    /// <seealso cref="MessageConsumerBase{IGetUser, IGetUserResponse}" />
    public class GetUserConsumer : MessageConsumerBase<IGetUser, IUserDTO>
    {
        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly UserManager<UserAccount> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserConsumer" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public GetUserConsumer(UserManager<UserAccount> userManager)
        {
            this._userManager = userManager;
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Parameter provided by MassTransit.")]
        protected async override Task ConsumeMessage(ConsumeContext<IGetUser> context)
        {
            var user = await this._userManager.FindByNameAsync(context.Message.UserName);

            if (user == null)
            {
                await context.RespondAsync<IConsumerException>(new
                {
                    ErrorMessage = $"Could not find user by user name ({context.Message.UserName})"
                });
            }
            else
            {
                await context.RespondAsync<IUserDTO>(new
                {
                    user.Profile.FirstName,
                    user.Profile.LastName,
                    user.UserName,
                    user.PhoneNumber
                });
            }
        }
    }
}