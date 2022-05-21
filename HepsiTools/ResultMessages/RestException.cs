using System;

namespace HepsiTools.ResultMessages
{
    public class RestException : Exception
    {
        public object Errors { get; set; }

        public RestException(object errors)
        {
            Errors = errors;
        }
    }
}
