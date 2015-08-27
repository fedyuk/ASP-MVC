using ASP.NET_Test.DataBaseInitialization;
using ASP.NET_Test.Models;
using ASP.NET_Test.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.Validators
{
    public class AccountPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            IAccountService accountService = new AccountService();
            var isExistsAccount = accountService.GetByPassword((string)value);
            return isExistsAccount != null;
        }
    }
}