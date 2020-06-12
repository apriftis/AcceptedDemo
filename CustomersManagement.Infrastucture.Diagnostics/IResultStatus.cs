using System;
using System.Collections.Generic;
using System.Text;

namespace CustomersManagement.Infrastructure.Diagnostics
{
    public interface IResultStatus
    {
        bool Success { get; }
        int ErrorCode { get; }
        string ErrorText { get; }
        string CorrelationId { get; }
        int EventId { get; }
    }
}
