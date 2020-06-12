using System;
using System.Collections.Generic;
using System.Text;

namespace CustomersManagement.Infrastructure.Constants
{
    public static class ResultCode
    {
        #region Known Codes

        /// <summary>
        /// 
        /// </summary>
        public const int Success = 0;

        /// <summary>
        /// 
        /// </summary>
        public const int BadRequest = 400;

        /// <summary>
        /// 
        /// </summary>
        public const int Unauthorized = 401;

        /// <summary>
        /// 
        /// </summary>
        public const int Forbidden = 403;

        /// <summary>
        /// 
        /// </summary>
        public const int NotFound = 404;

        /// <summary>
        /// 
        /// </summary>
        public const int MethodNotAllowed = 405;

        /// <summary>
        /// 
        /// </summary>
        public const int RequestTimeout = 408;

        /// <summary>
        /// 
        /// </summary>
        public const int Conflict = 409;

        /// <summary>
        /// 
        /// </summary>
        public const int PreconditionFailed = 412;

        /// <summary>
        /// 
        /// </summary>
        public const int UnsupportedMediaType = 415;

        /// <summary>
        /// 
        /// </summary>
        public const int UnprocessableEntity = 422;

        /// <summary>
        /// 
        /// </summary>
        public const int Locked = 423;

        /// <summary>
        /// 
        /// </summary>
        public const int MethodFailure = 424;

        /// <summary>
        /// 
        /// </summary>
        public const int TooManyRequests = 429;

        /// <summary>
        /// 
        /// </summary>
        public const int InternalServerError = 500;

        /// <summary>
        /// 
        /// </summary>
        public const int ServiceUnavailable = 503;

        /// <summary>
        /// 
        /// </summary>
        public const int BadGateway = 502;

        /// <summary>
        /// 
        /// </summary>
        public const int NotImplemented = 501;

        /// <summary>
        /// 
        /// </summary>
        public const int Expired = 410;

        /// <summary>
        /// 
        /// </summary>
        public const int InsufficientFunds = 452;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsSuccess(int code) =>
            code == 0 || (code >= 200 && code < 300);
    }
}
