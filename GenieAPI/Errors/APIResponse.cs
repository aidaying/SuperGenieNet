using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenieAPI.Errors
{
    public class APIResponse
    {
        public APIResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "This is a bad request.",
                401 => "You arenot authorised",
                404 => "The resource you are looking for is not found.",
                500 => "Internal server error, please try again later.",
                _ => null
            };
        }
    }
}