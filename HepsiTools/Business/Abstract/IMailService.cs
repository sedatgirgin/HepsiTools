using HepsiTools.Models;
using System.Threading.Tasks;

namespace HepsiTools.Business.Abstract
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
