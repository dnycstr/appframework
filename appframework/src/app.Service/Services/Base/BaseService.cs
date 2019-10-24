using app.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Service.Services.Base
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseService 
    {
        /// <summary>
        /// Gets or sets the DbContext. This will serve as you main database connection for this service.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public IApplicationDbContext Context { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        protected BaseService(IApplicationDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Return an IServiceResult.
        /// </summary>
        /// <param name="isSuccess">if set to <c>true</c> [is success].</param>
        /// <param name="message">The error message.</param>
        /// <returns></returns>
        protected ServiceResult Result(bool isSuccess, string message = null)
        {
            return new ServiceResult { IsSuccess = isSuccess, ErrorMessage = message };
        }

        /// <summary>
        /// Results the specified data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="isSuccess">if set to <c>true</c> [is success].</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        protected ServiceResult<T> Result<T>(T data, bool isSuccess, string message = null)
        {
            return new ServiceResult<T> { IsSuccess = isSuccess, ErrorMessage = message, Data = data };
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>    
    }
}
