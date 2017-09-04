using System;

namespace Survey.Utils
{
   
    public class SurveyException : Exception
    {
        public SurveyException()
        { }

        public SurveyException(string message) : base(message)
        { }

        public SurveyException(string message, Exception ex) : base(message, ex)
        { }

        public SurveyException(string format, params object[] args) : base(string.Format(format, args))
        { }
    }
    
}
