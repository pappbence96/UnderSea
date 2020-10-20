using Microsoft.AspNetCore.Identity;
using StrategyGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Dal
{
    public class ApplicationUser : IdentityUser<int>, IApplicationUser
    {
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
