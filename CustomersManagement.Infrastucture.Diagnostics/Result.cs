using CustomersManagement.Infrastructure.Constants;
using System;

namespace CustomersManagement.Infrastructure.Diagnostics
{
    public class Result<T> : IResult<T>
    {
        #region Properties

        public T Data { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorText { get; set; }

        public string CorrelationId { get; set; }

        public int EventId { get; set; }

        public bool Success => ResultCode.IsSuccess(ErrorCode);

        #endregion

        public static IResult<T> CreateSuccessful(T data)
        {
            return CreateSuccessful(data, 0, null);
        }

        public static IResult<T> CreateSuccessful(T data,
            int eventId, string correlationId)
        {
            if (typeof(T) != typeof(object) && data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            return new Result<T>()
            {
                Data = data,
                EventId = eventId,
                CorrelationId = correlationId,
                ErrorCode = ResultCode.Success
            };
        }

        public static IResult<T> CreateFailed<Y>(IResult<Y> result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            return CreateFailed(result.EventId, result.ErrorCode,
                result.CorrelationId, result.ErrorText);
        }

        public static IResult<T> CreateFailed(int errorCode, string errorText)
        {
            return CreateFailed(0, errorCode, null, errorText);
        }

        public static IResult<T> CreateFailed(int eventId,
            int errorCode, string correlationId, string errorText)
        {
            if (ResultCode.IsSuccess(errorCode))
            {
                throw new ArgumentOutOfRangeException(nameof(errorCode));
            }

            return new Result<T>()
            {
                EventId = eventId,
                ErrorCode = errorCode,
                ErrorText = errorText,
                CorrelationId = correlationId
            };
        }
    }
}
