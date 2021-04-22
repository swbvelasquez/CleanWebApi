using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanWebApi.Api.Responses
{
    public class ApiResponse<T> where T:class
    {
        public T Data { get; set; }

        public ApiResponse(T data)
        {
            Data = data;
        }
    }
}
