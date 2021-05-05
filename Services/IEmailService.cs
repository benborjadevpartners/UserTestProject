using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserTestProject.Models;

namespace UserTestProject.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string recipient, string body, string subject, string bodyHtml);
    }
}
