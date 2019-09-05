using System;
using DerivcoTestTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DerivcoTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AreaSurfaceController : Controller
    {
        private readonly ISurfaceAreasInterface _surfaceAreasService;
        public AreaSurfaceController(ISurfaceAreasInterface surfaceAreasService)
        {
            _surfaceAreasService = surfaceAreasService;
        }

        [HttpPost]
        [Route("~/api/GetArea")]
        public IActionResult GetArea([FromBody] CalculatingSurfaceIncomeModel model)
        {
            if (!ModelState.IsValid)
                throw new ArgumentException("ModelState is not valid.");

            return Ok(_surfaceAreasService.CalculateSurfaceAreas(model));
        }

    }
}