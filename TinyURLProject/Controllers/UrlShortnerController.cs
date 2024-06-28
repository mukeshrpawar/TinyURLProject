using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;
using TinyURLProject.Context;
using TinyURLProject.Model;


namespace TinyURLProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShortnerController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UrlShortnerController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] string originalUrl)
        {
            var shortUrl = NanoidDotNet.Nanoid.Generate(size:10);
            var urlmapping = new UrlMapping
            {
                OriginalUrl = originalUrl,
                ShortUrl = shortUrl
            };
            _context.UrlMappings.Add(urlmapping);
            await _context.SaveChangesAsync();
            return Ok(new { ShortUrl = shortUrl });
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> RedirectToOriginalUrl(string shortUrl)
        {
            var urlmapping=await _context.UrlMappings.FirstOrDefaultAsync(x=>x.ShortUrl == shortUrl);
            if (urlmapping == null) 
            {
                return NotFound();
            }
            return Redirect(urlmapping.OriginalUrl);
        }
    }
}
