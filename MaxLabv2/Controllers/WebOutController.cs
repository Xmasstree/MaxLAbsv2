using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;


namespace MaxLabv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebOutController : ControllerBase
    {
        [HttpGet]
        public IActionResult Aboba ([FromQuery] string a, [FromQuery] string sw)
        {
            WebOut webOut = Programs.Base(a, sw);
            if (webOut.st1 == "ex1") 
            {
                return BadRequest($"Inwalid parametrs: {webOut.st2}");
            }
            if (webOut.st1 == "ex2")
            {
                return BadRequest("Inwalid parametrs");
            }
            else
            {
                string fileName = "weboo.json";
                string jsonString = JsonSerializer.Serialize(webOut);
                System.IO.File.WriteAllText(fileName, jsonString);
                byte[] mas = System.IO.File.ReadAllBytes(fileName);
                return File(mas, "application/json", fileName);
            }
        }
    }
}
