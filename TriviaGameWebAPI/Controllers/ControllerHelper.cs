using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TriviaGameWebAPI.Controllers
{
    public class ControllerHelper
    {
        public static IEnumerable<string> GetModelStateErrors(ModelStateDictionary modelState)
        {
            try
            {
                return modelState.Values.SelectMany(modelErrorCollection => modelErrorCollection.Errors).
                                         Select(modelError => modelError.ErrorMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
