using SSL_SMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSL_SMS.BLL
{
    public class AccountBLL
    {
        AccountDAL _accountDal = new AccountDAL();

        internal string encrypt(string user_Password)
        {
            return _accountDal.encrypt(user_Password);
        }
    }
}