using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Entities;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly INumberAndColor _numberAndColor;
        private readonly IRuleta _ruleta;

        public HomeController(DataContext context, INumberAndColor numberAndColor, IRuleta ruleta)
        {
            _context = context;
            _numberAndColor = numberAndColor;
            _ruleta = ruleta;
        }

        [HttpGet]
        [Route("api/[controller]/getnumberandcolor")]
        public IActionResult GetNumberAndColor()
        {
            var respuesta = _numberAndColor.GetNumberAndColor();
            return Ok(respuesta);
        }

       
        [HttpGet]
        [Route("api/[controller]/getplayers")]
        public IActionResult GetPlayers()
        {
            try
            {
                var respuesta = _ruleta.GetAll();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
               //
            }
        }

        
        [HttpGet]
        [Route("api/[controller]/getplayerbyname/{name}")]
        public IActionResult GetPlayerbyName(string name)
        {
            var respuesta = _ruleta.GetByName(name); 
            return Ok(respuesta);
        }

    
        [HttpPost]
        [Route("api/[controller]/saveplayer")]
        public IActionResult SavePlayer([FromBody] RuletaEntity entidad)
        {
            _ruleta.Save(entidad);
            return Ok(new { message = "created" });
        }

    }
}
