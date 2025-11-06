using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheapFlight.Application.Common
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public static ResponseDto<T> Ok(T data, string? message = null) =>
            new() { Success = true, Data = data, Message = message };

        public static ResponseDto<T> Fail(IEnumerable<string> errors, string? message = null) =>
            new() { Success = false, Errors = errors, Message = message };
    }
}
