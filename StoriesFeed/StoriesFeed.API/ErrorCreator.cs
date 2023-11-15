using StoriesFeed.Domain.Models;
using System.Net;
using System.Text.Json;

namespace StoriesFeed.API
{
    public static class ErrorCreator
    {
        public static Error CreateError(Exception exception)
        {
            Error error = new Error();

            switch (exception)
            {
                case HttpRequestException:
                case JsonException:
                case ArgumentNullException:                
                    error.Code = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case ApplicationException:
                    error.Code = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    error.Code = (int)HttpStatusCode.InternalServerError;                    
                    break;                
            }

            error.Message = exception.Message;

            return error;
        }
    }
}
