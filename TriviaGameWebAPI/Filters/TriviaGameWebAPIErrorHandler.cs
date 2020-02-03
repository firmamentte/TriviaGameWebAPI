using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Mime;
using System.Text;

namespace TriviaGameWebAPI.Filters
{
    public class TriviaGameWebAPIErrorHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var _bytes = Encoding.UTF8.GetBytes(context.Exception.Message);

            var _response = context.HttpContext.Response;
            _response.StatusCode = 500;
            _response.ContentType = MediaTypeNames.Text.Plain;
            _response.Body.Write(_bytes, 0, _bytes.Length);
        }
    }
}
