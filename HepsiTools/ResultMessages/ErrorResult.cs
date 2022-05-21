using HepsiTools.GenericRepositories.ResultMessage;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HepsiTools.ResultMessages
{
    public class ErrorResult : IActionResult, IErrorResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public object Errors { get; set; }

        public ErrorResult(string message)
        {
            Message = message;
            Succeeded = false;
        }

        public ErrorResult(string message, object errors)
        {
            Message = message;
            Errors = errors;
            Succeeded = false;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this);

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
