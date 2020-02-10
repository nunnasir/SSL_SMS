using SSL_SMS.DAL;
using SSL_SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSL_SMS.ViewModels
{
    public class MessageGroupVm
    {
        public IEnumerable<MessageGroup> MessageGroups { get; set; }
        public SendSmsStatu SendSmsStatu { get; set; }
    }
}