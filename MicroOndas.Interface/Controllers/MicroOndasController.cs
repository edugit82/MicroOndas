using MicroOndas.Business;
using MicroOndas.DataBase.Models;
using MicroOndas.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace MicroOndas.Interface.Controllers
{
    public class MicroOndasController : Controller
    {
        private readonly IConfiguration _configuration;
        private string GenerateJwtToken(string username, IConfiguration _configuration)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public MicroOndasController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Cadastrar()
        {

            return View();
        }
        public IActionResult Logar()
        {

            return View();
        }
        public IActionResult MicroOndas()
        {
            if (!Logado())
            {
                return RedirectToAction("Logar", "MicroOndas");
            }

            return View();
        }
        public IActionResult CadastrarPrograma()
        {
            if (!Logado())
            {
                return RedirectToAction("Logar", "MicroOndas");
            }

            return View();
        }
        [HttpPost]
        public JsonResult PostCadastro([FromBody] CadastrarViewModel viewModel)
        {
            CadastrarBusiness cadastroBusiness = new CadastrarBusiness(viewModel);
            return Json(cadastroBusiness);
        }
        [HttpPost]
        public JsonResult PostLogar([FromBody] LogarViewModel viewModel)
        {
            LogarBusiness logarBusiness = new LogarBusiness(viewModel);

            //Login efetuado com sucesso
            if (logarBusiness.Cor == "green")
            {
                //Gera token
                string _token = GenerateJwtToken(viewModel.Login, _configuration);

                //Salva token no banco
                using (Context ctx = new Context())
                {
                    LoginModel? model = ctx.Login.FirstOrDefault(x => x.Usuario == viewModel.Login);
                    if (model != null)
                    {
                        model.Token = _token;
                        ctx.Entry(model).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                }

                HttpContext.Session.SetString("_Login", viewModel.Login);
            }

            return Json(logarBusiness);
        }
        [HttpPost]
        public JsonResult PostCadastrarPrograma([FromBody]CadastroProgramaViewModel viewModel)
        {
            bool logado = Logado();            
            CadastrarProgramaBusiness cadastroProgramaBusiness = new CadastrarProgramaBusiness(viewModel, logado);
            return Json(cadastroProgramaBusiness);
        }
        [HttpPost]
        public async Task<JsonResult> PostAquecimento([FromBody] AquecimentoViewModel aquecimentos)
        {
            return await Task.Run(() =>
            {
                bool logado = Logado();
                AquecimentoBusiness aquecimentoBusinness = new AquecimentoBusiness(aquecimentos,logado);
                return Json(aquecimentoBusinness);
            });
        }
        [HttpPost]
        public async Task<JsonResult> PostProgresso([FromBody] ProgressoViewModel progresso)
        {
            return await Task.Run(() =>
            {
                bool logado = Logado();
                ProgressoBusiness progressoBusinness = new ProgressoBusiness(progresso, logado);
                return Json(progressoBusinness);
            });
        }
        [HttpPost]
        public async Task<JsonResult> PostDescricaoPrograma([FromBody] DescricaoProgramaViewModel viewmodel)
        {
            return await Task.Run(() =>
            {
                bool logado = Logado();
                DescricaoProgramaBusiness descricaoProgramaBusiness = new DescricaoProgramaBusiness(viewmodel, logado);
                return Json(descricaoProgramaBusiness);
            });
        }
        [HttpPost]
        public async Task<JsonResult> PostDadosProgramaPorId([FromBody] DadosProgramaPorIdViewModel viewmodel)
        {
            return await Task.Run(() =>
            {
                DadosProgramaPorIdBusiness dadosProgramaPorIdBusiness = new DadosProgramaPorIdBusiness(viewmodel);
                return Json(dadosProgramaPorIdBusiness.Retorno);
            });
        }
        [HttpGet]
        public async Task<JsonResult> GetPausa()
        {
            return await Task.Run(() =>
            {
                bool logado = Logado();
                PausaBusiness pausaBusiness = new PausaBusiness(logado);
                return Json(pausaBusiness);
            });
        }
        [HttpGet]
        public async Task<JsonResult> GetDadosPrograma()
        {
            return await Task.Run(() =>
            {
                bool logado = Logado();
                DadosProgramaBusiness dadosProgramaBusiness = new DadosProgramaBusiness(logado);
                return Json(dadosProgramaBusiness);
            });
        }
        private bool Logado()
        {
            string login = HttpContext.Session.GetString("_Login") ?? "";
            bool logado = false;

            using (Context ctx = new Context())
            {
                LoginModel? model = ctx.Login.FirstOrDefault(x => x.Usuario == login);
                if (model != null)
                {
                    HttpClient client = new HttpClient();

                    client.BaseAddress = new Uri("https://localhost:44321/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.Token);

                    try
                    {
                        Task.Run(async () =>
                        {
                            var response = await client.GetAsync("api/confirmalogado");
                            response.EnsureSuccessStatusCode();
                            var retorno = await response.Content.ReadAsStringAsync();

                        }).Wait();

                        logado = true;
                    }
                    catch (Exception)
                    {
                        logado = false;
                    }
                }
            }

            return logado;
        }
    }

    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("confirmalogado")]
        public IActionResult ConfirmaLogado()
        {
            return Ok(true);
        }
    }
}
