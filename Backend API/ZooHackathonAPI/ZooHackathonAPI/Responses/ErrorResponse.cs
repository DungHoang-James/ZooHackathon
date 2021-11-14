using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooHackathonAPI.Responses
{
    public class ErrorResponse : Exception
    {
        public ErrorDetailResponse Error { get; private set; }

        public ErrorResponse(int errorCode, string message)
        {
            Error = new ErrorDetailResponse
            {
                Code = errorCode,
                Message = message
            };
        }
    }

    public class ErrorDetailResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
