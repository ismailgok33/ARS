using AdessoRideShare.Api.Common.Responses;
using AdessoRideShare.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdessoRideShare.Filters
{
    /// <summary>
    /// Custom Exception Filter
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            BaseResponse<int?> baseResponse = new BaseResponse<int?>();

            if (context.Exception.GetType() == typeof(CityNotValidException))
            {
                baseResponse = new BaseResponse<int?>()
                {
                    ResponseCode = 500,
                    Success = false,
                    ErrorMessage = "City must have valid X and Y values!"
                };

                context.Result = new OkObjectResult(baseResponse);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.HttpContext.Response.Headers.Add("CityNotValidException", "true");
            }
            else if (context.Exception.GetType() == typeof(NotEnoughSeatException))
            {
                baseResponse = new BaseResponse<int?>()
                {
                    ResponseCode = 500,
                    Success = false,
                    ErrorMessage = "There is not enough seat available!"
                };

                context.Result = new OkObjectResult(baseResponse);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.HttpContext.Response.Headers.Add("NotEnoughSeatException", "true");
            }
            else if (context.Exception.GetType() == typeof(TravelNotFoundException))
            {
                baseResponse = new BaseResponse<int?>()
                {
                    ResponseCode = 500,
                    Success = false,
                    ErrorMessage = "Travel cannot be found with the given ID!"
                };

                context.Result = new OkObjectResult(baseResponse);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.HttpContext.Response.Headers.Add("TravelNotFoundException", "true");
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { "An error occur.Try it again." }
                };

                if (_env.IsDevelopment()) json.DeveloperMessage = context.Exception;

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }
    }
}
