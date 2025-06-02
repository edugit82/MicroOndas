using MicroOndas.Business;
using MicroOndas.DataBase;
using MicroOndas.DataBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace MicroOndas.Interface.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MicroOndasController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MicroOndasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GenerateJwtToken(string username)
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
        private bool Logado()
        {            
            bool logado = false;

            using (Context ctx = new Context())
            {
                List<LoginModel> tokensvalidos = ctx.Login.ToList().Where(x => x.TokenTime > DateTime.Now).ToList();
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("https://localhost:44328/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                tokensvalidos.ForEach(a => 
                {
                    if(logado)
                        return;

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", a.Token);

                    try
                    {
                        Task.Run(async () =>
                        {
                            var response = await client.GetAsync("MicroOndas/ConfirmaLogado");
                            response.EnsureSuccessStatusCode();
                            var retorno = await response.Content.ReadAsStringAsync();

                        }).Wait();

                        logado = true;
                    }
                    catch (Exception)
                    {
                        logado = false;
                    }
                });
            }

            return logado;
        }
        [Authorize]
        [HttpGet("ConfirmaLogado")]
        public IActionResult ConfirmaLogado()
        {
            return Ok(true);
        }

        [HttpPost("TextoBotao")]
        public async Task<JsonResult> TextoBotao([FromBody] BotaoTextoViewModel viewmodel)
        {
            return await Task.Run(() =>
            {
                BotaoTextoBusiness botaoTextoBusiness = new BotaoTextoBusiness(viewmodel, Logado());
                return new JsonResult(new { botaoTextoBusiness.Retorno, botaoTextoBusiness.Cor });
            });
        }

        [HttpGet("Aquecimento")]
        public async Task<JsonResult> Aquecimento()
        {
            return await Task.Run(() =>
            {
                AquecimentoBusiness aquecimentoBusiness;

                using (Context ctx = new Context())
                {
                    VariaveisModel? timer = ctx.Variaveis.ToList().FirstOrDefault(a => a.Index == 1);
                    VariaveisModel? potencia = ctx.Variaveis.ToList().FirstOrDefault(a => a.Index == 2);

                    AquecimentoViewModel viewmodel = new AquecimentoViewModel
                    {
                        Tempo = timer?.Valor ?? "00:00",
                        Potencia = potencia?.Valor ?? "10"
                    };

                    aquecimentoBusiness = new AquecimentoBusiness(viewmodel, Logado());
                }

                return new JsonResult(new { aquecimentoBusiness.Retorno, aquecimentoBusiness.Cor });
            });
        }
        [HttpPost("AquecimentoPost")]
        public async Task<JsonResult> AquecimentoPost([FromBody] AquecimentoViewModel viewmodel)
        {
            return await Task.Run(() =>
            {
                using (Context ctx = new Context())
                {
                    ProgramadoModel? dadosprograma = ctx.Programado.ToList().FirstOrDefault(a => a.Index == viewmodel.Id);
                    viewmodel.Potencia = dadosprograma is null ? "10" : dadosprograma.Potencia.ToString("00");
                    viewmodel.Tempo = dadosprograma is null ? "00:00" : dadosprograma.Tempo.ToString(@"mm\:ss");

                    VariaveisModel variaveis = new VariaveisModel();

                    //Timer
                    variaveis = ctx.Variaveis.First(a => a.Index == 1);
                    variaveis.Valor = dadosprograma?.Tempo.ToString("mm:ss");
                    ctx.Entry(variaveis).State = EntityState.Modified;
                    ctx.SaveChanges();

                    //Potência
                    variaveis = ctx.Variaveis.First(a => a.Index == 2);
                    variaveis.Valor = dadosprograma?.Potencia.ToString("00");
                    ctx.Entry(variaveis).State = EntityState.Modified;
                    ctx.SaveChanges();

                    //Id
                    variaveis = ctx.Variaveis.First(a => a.Index == 3);
                    variaveis.Valor = dadosprograma?.Index.ToString("00");
                    ctx.Entry(variaveis).State = EntityState.Modified;
                    ctx.SaveChanges();
                }

                AquecimentoBusiness aquecimentoBusiness; aquecimentoBusiness = new AquecimentoBusiness(viewmodel, Logado());
                return new JsonResult(new { aquecimentoBusiness.Retorno, aquecimentoBusiness.Cor });
            });
        }

        [HttpPost("Progresso")]
        public async Task Progresso()
        {
            await Task.Run(() =>
            {
                ProgressoBusiness progressoBusiness = new ProgressoBusiness(Logado());
            });
        }

        [HttpGet("Pausa")]
        public async Task<JsonResult> Pausa()
        {
            return await Task.Run(() =>
            {
                PausaBusiness pausaBusiness = new PausaBusiness(Logado());
                return new JsonResult(new { pausaBusiness.Retorno, pausaBusiness.Cor });
            });
        }

        [HttpGet("MensagemDisplay")]
        public async Task<JsonResult> MensagemDisplay()
        {
            return await Task.Run(() =>
            {
                MensagemDisplayBusiness mensagemDisplayBusiness = new MensagemDisplayBusiness(Logado());
                return new JsonResult(new { mensagemDisplayBusiness.Retorno, mensagemDisplayBusiness.Cor });
            });
        }
        [HttpGet("MensagemProgresso")]
        public async Task<JsonResult> MensagemProgresso()
        {
            return await Task.Run(() =>
            {
                MensagemProgressoBusiness mensagemProgressoBusiness = new MensagemProgressoBusiness(Logado());
                return new JsonResult(new { mensagemProgressoBusiness.Retorno, mensagemProgressoBusiness.Cor });
            });
        }
        [HttpPost("Cadastrar")]
        public async Task<JsonResult> Cadastrar([FromBody] CadastrarViewModel viewmodel)
        {
            return await Task.Run(() =>
            {
                CadastrarBusiness cadastrarBusiness = new CadastrarBusiness(viewmodel, true);
                return new JsonResult(new { cadastrarBusiness.Retorno, cadastrarBusiness.Cor });
            });
        }
        [HttpPost("Logar")]
        public async Task<JsonResult> Logar([FromBody] LogarViewModel viewmodel)
        {
            return await Task.Run(() =>
            {
                LogarBusiness logarBusiness = new LogarBusiness(viewmodel, true);

                //Login efetuado com sucesso
                if (logarBusiness.Cor == "green")
                {                    

                    //Gera token
                    string _token = GenerateJwtToken(viewmodel.Login);

                    //Salva token no banco
                    using (Context ctx = new Context())
                    {
                        LoginModel? model = ctx.Login.FirstOrDefault(x => x.Usuario == viewmodel.Login);
                        if (model != null)
                        {
                            model.Token = _token;
                            model.TokenTime = DateTime.Now.AddMinutes(30);
                            ctx.Entry(model).State = EntityState.Modified;
                            ctx.SaveChanges();
                        }
                    }
                }

                return new JsonResult(new { logarBusiness.Retorno, logarBusiness.Cor });
            });
        }
        [HttpPost("CadastrarPrograma")]
        public async Task<JsonResult> CadastrarPrograma([FromBody] CadastroProgramaViewModel viewmodel)
        {
            return await Task.Run(() =>
            {
                CadastrarProgramaBusiness cadastrarProgramaBusiness = new CadastrarProgramaBusiness(viewmodel, Logado());
                return new JsonResult(new { cadastrarProgramaBusiness.Retorno, cadastrarProgramaBusiness.Cor });
            });
        }
        [HttpGet("GetTituloProgramas")]
        public async Task<JsonResult> GetTituloProgramas()
        {
            return await Task.Run(() =>
            {
                GetProgramas getProgramas = new GetProgramas();
                var retorno = getProgramas.Retorno.Select(a => new { a.Index, a.Nome }).ToList();
                return new JsonResult(new { retorno });
            });
        }
        [HttpPost("DescricaoPrograma")]
        public async Task<JsonResult> DescricaoPrograma([FromBody] DescricaoProgramaViewModel viewmodel)
        {
            return await Task.Run(() =>
            {
                DescricaoProgramaBusiness descricaoProgramaBusiness = new DescricaoProgramaBusiness(viewmodel, Logado());
                return new JsonResult(new { descricaoProgramaBusiness.Retorno, descricaoProgramaBusiness.Cor });
            });
        }
        [HttpGet("LimpaVariaveis")]
        public async Task LimpaVariaveis()
        {
            await Task.Run(() =>
            {
                LimpaVariaveisBusiness.LimpaCompleto();
            });
        }
    }
}
