using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Bll.Services.Identity
{
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string CountryName { get; set; }

        public bool Valid 
        {
            get =>
                !string.IsNullOrWhiteSpace(Username) &&
                !string.IsNullOrWhiteSpace(Password) &&
                !string.IsNullOrWhiteSpace(ConfirmPassword) &&
                !string.IsNullOrWhiteSpace(CountryName);
        }
    }
}
