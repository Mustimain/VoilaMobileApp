using System;
namespace VoilaMobileApp.Src.Utilts.Results
{
    public class Result : IResult
    {
        public Result(bool status, string messages) : this(status)
        {
            Message = messages;
        }

        public Result(bool success)
        {
            Status = success;
        }


        public bool Status { get; }
        public string Message { get; }
    }
}

