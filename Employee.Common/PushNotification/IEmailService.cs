using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Common.PushNotification
{
    public  interface IEmailService
    {
        Task SendMailAsync(MailRequest mailrequest);
    }
}
