using MicroOndas.Business;
using MicroOndas.DataBase;
using MicroOndas.DataBase.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace MicroOndas.Interface.Controllers
{    
    
    public class MicroOndasController : Controller
    {        
        public IActionResult MicroOndas()
        {
            return View();
        }
    }


    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CadastrarController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CadastrarController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CadastrarViewModel cadastrar)
        {
            RetornoAquecimentoViewModel retorno = new RetornoAquecimentoViewModel();

            new Business.CadastrarBusiness().Business(ref retorno, _configuration, cadastrar);

            return Ok(retorno);
        }
    }

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LogarController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LogarController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LogarViewModel logar)
        {
            RetornoAquecimentoViewModel retorno = new RetornoAquecimentoViewModel();

            new Business.LogarBusiness().Business(ref retorno, _configuration, logar);

            return Ok(retorno);
        }        
    }
        
    [Authorize]    
    [Route("api/[controller]")]
    [ApiController]
    public class AquecimentoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AquecimentoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AquecimentoViewModel aquecimento)
        {
            RetornoAquecimentoViewModel retorno = new RetornoAquecimentoViewModel();

            new Business.AquecimentoBusiness().Business(ref retorno, _configuration, aquecimento);

            return Ok(retorno);
        }
    }

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProgressoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProgressoViewModel progresso)
        {
            RetornoAquecimentoViewModel retorno = new RetornoAquecimentoViewModel();

            new Business.ProgressoBusiness().Business(ref retorno, _configuration, progresso);

            return Ok(retorno);
        }
    }

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PausaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PausaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            RetornoAquecimentoViewModel retorno = new RetornoAquecimentoViewModel();

            new Business.PausaBusiness().Business(ref retorno, _configuration);

            return Ok(retorno);
        }
    }

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DadosProgramaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DadosProgramaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            RetornoAquecimentoViewModel retorno = new RetornoAquecimentoViewModel();

            new Business.DadosProgramaBusiness().Business(ref retorno, _configuration);

            return Ok(retorno);            
        }
    }

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CadastrarProgramaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CadastrarProgramaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CadastroProgramaViewModel programa)
        {
            RetornoAquecimentoViewModel retorno = new RetornoAquecimentoViewModel();

            new Business.CadastrarProgramaBusiness().Business(ref retorno, _configuration, programa);

            return Ok(retorno);

        }
    }   
    
}
