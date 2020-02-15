using SSL_SMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSL_SMS.BLL
{
    public class SendSmsBLL
    {
        SendSmsDAL _smsdal = new SendSmsDAL();

        internal string SendSms(string contacts, string messages)
        {
            return _smsdal.SendSms(contacts, messages);
        }
    }
}