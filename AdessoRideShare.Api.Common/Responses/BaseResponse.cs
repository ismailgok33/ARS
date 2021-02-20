using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Api.Common.Responses
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; } = null;
        public int ResponseCode { get; set; } = 200;
    }
}
