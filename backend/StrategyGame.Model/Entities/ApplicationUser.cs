using Microsoft.AspNetCore.Identity;
using StrategyGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Model.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
