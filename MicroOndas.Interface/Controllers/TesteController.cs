using MicroOndas.Business;
using MicroOndas.DataBase;
using MicroOndas.DataBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace MicroOndas.Interface.Controllers
{
    public class TesteController : Controller
    {
        private readonly IConfiguration _configuration;
        public TesteController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }        
    }
}
