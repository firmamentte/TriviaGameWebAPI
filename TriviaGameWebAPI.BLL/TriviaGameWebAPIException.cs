using System;

namespace TriviaGameWebAPI.BLL
{
    public class TriviaGameWebAPIException : Exception
    {
        public TriviaGameWebAPIException(string errorMessage) : base(errorMessage)
        { }
    }
}
