using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Johari.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdjectivesController : Controller
    {
        private readonly IUnitofWork _unitofWork;


        public AdjectivesController(IUnitofWork unitofWork)
            => _unitofWork = unitofWork;

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitofWork.Adjective.List() });

        }
    }
}
