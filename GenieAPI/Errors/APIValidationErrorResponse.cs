using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenieAPI.Errors
{
    public class APIValidationErrorResponse : APIResponse
    {
        public APIValidationErrorResponse() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}