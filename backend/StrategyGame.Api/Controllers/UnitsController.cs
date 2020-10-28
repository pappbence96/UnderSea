using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StrategyGame.Api.Dto;
using StrategyGame.Api.Services.UserAccessor;
using StrategyGame.Bll.Services.Units;
using StrategyGame.Bll.Services.User;

namespace StrategyGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UnitsController : ControllerBase
    {
        private readonly IUserAccessor userAccessor;
        private readonly IUnitsService unitsService;
        private readonly IUserService userService;

        public UnitsController(IUserAccessor userAccessor, IUnitsService unitsService, IUserService userService)
        {
            this.userAccessor = userAccessor;
            this.unitsService = unitsService;
            this.userService = userService;
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<UnitDto>>> GetUnitTypes()
        {
            var player = await userService.GetUserById(userAccessor.UserId);
            var unitTypes = await unitsService.GetAllUnitTypes();
            var unitsOfPlayer = await unitsService.GetAllUnitsOfCountry(player.Country.Id);
            var mappedUnits = unitTypes.Select(u => new UnitDto
            {
                Id = u.Id,
                Name = u.Name,
                Attack = u.Attack,
                Defense = u.Defense,
                Price = u.Price,
                Supply = u.Supply,
                Pay = u.Pay,
                Owned = unitsOfPlayer.FirstOrDefault(c => c.UnitId == u.Id)?.TotalCount ?? 0
            }).ToList();
            return Ok(mappedUnits);
        }
    }
}
