//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSL_SMS.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SmsLog
    {
        public int Id { get; set; }
        public Nullable<short> SmsType { get; set; }
        public Nullable<short> Status { get; set; }
        public string SendUser { get; set; }
        public Nullable<System.DateTime> SendDate { get; set; }
        public string MessageStatus { get; set; }
    }
}
