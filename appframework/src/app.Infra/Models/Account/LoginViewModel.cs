using System;
using System.Collections.Generic;
using System.Text;

namespace app.Infra.Models.Account
{
    public class LoginViewModel : LoginInputViewModel
    {
        public bool AllowRememberLogin { get; set; } = true;
        public bool EnableLocalLogin { get; set; } = true;
     }
}
