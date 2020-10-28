using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyGame.Api.Dto
{
    public class UnitDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Owned { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Pay { get; set; }
        public int Supply { get; set; }
        public int Price { get; set; }
    }
}
