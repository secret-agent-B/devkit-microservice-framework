// -----------------------------------------------------------------------
// <copyright file="FakeUploadBase64ImageConsumer.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.FileStore.Fakes.Consumers
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Devkit.Communication.FileStore.DTOs;
    using Devkit.Communication.FileStore.Messages;
    using Devkit.ServiceBus.Test;
    using MassTransit;

    /// <summary>
    /// The TestUploadBase64ImageConsumer is the test consumer for IUploadBase64Image.
    /// </summary>
    public class FakeUploadBase64ImageConsumer : FakeMessageConsumerBase<IUploadBase64Image, IFileDTO>
    {
        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override async Task ConsumeMessage(ConsumeContext<IUploadBase64Image> context)
        {
            var contents = Encoding.UTF8.GetBytes(context.Message.Contents).ToList();

            await context.RespondAsync<IFileDTO>(new
            {
                Contents = contents,
                CreatedOn = DateTime.UtcNow,
                context.Message.FileName,
                FileSize = contents.Count,
                Id = this.Faker.Random.Hexadecimal(24, string.Empty)
            });
        }
    }
}