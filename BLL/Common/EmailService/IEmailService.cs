using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Common.EmailService
{
    public interface IEmailService
    {
        public void SendEmail(Email email);    

    }
}
