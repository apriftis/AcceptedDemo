using System;
using System.Collections.Generic;
using System.Text;

namespace CustomersManagement.Infrastructure.Diagnostics
{
    public interface IResult<out T> : IResultStatus
    {
        T Data { get; }
    }
}
