using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using System.IO;
using Microsoft.AspNetCore.Authentication;


namespace MaxLabv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebOutController : ControllerBase
    {
        [HttpGet]
        public IActionResult Aboba ([FromQuery] Weboo weboo, HttpContext context)
        {
            string fileName = "weboo.json";
            string jsonString = JsonSerializer.Serialize(weboo);
            System.IO.File.WriteAllText(fileName, jsonString);
            byte[] mas = System.IO.File.ReadAllBytes(fileName);
            return Ok(Results.Json(weboo));
            //return File(mas, "application/json", fileName) ;
        }
    }
}
