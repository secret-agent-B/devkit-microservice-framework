// -----------------------------------------------------------------------
// <copyright file="FakeGetUsersConsumer.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Communication.Security.Fakes.Consumers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Communication.Security.DTOs;
    using Devkit.Communication.Security.Messages;
    using Devkit.ServiceBus.Interfaces;
    using Devkit.ServiceBus.Test;
    using MassTransit;

    /// <summary>
    /// The TestGetUsersConsumer is a test consumer for the IGetUsers message.
    /// </summary>
    public class FakeGetUsersConsumer : FakeMessageConsumerBase<IGetUsers, IListResponse<IUserDTO>>
    {
        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ConsumeMessage(ConsumeContext<IGetUsers> context)
        {
            await context.RespondAsync<IListResponse<IUserDTO>>(new
            {
                Items = context.Message.UserNames.Select(x => new
                {
                    FirstName = this.Faker.Person.FirstName,
                    LastName = this.Faker.Person.LastName,
                    UserName = x,
                    PhoneNumber = this.Faker.Person.Phone
                }).ToList()
            });
        }
    }
}