// -----------------------------------------------------------------------
// <copyright file="DevkitControllerBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.WebAPI
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The Devkit controller base class.
    /// </summary>
    public class DevkitControllerBase : ControllerBase
    {
        /// <summary>
        /// The mediator.
        /// </summary>
        private IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevkitControllerBase" /> class.
        /// </summary>
        public DevkitControllerBase()
        {
        }

        /// <summary>
        /// Gets the mediator.
        /// </summary>
        /// <value>
        /// The mediator.
        /// </value>
        protected IMediator Mediator => this._mediator ??= (IMediator)this.HttpContext.RequestServices.GetService(typeof(IMediator));
    }
}