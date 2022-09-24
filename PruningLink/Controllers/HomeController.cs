using Microsoft.AspNetCore.Mvc;
using PruningLink.Model;
using PruningLink.Model.Entity;
using System.Formats.Asn1;
using System.Text;

namespace PruningLink.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private ApplicationContext _db;
        private ILogger<HomeController> _logger;
        private IShortUrlServces _shortUrlServces;
        public HomeController(ApplicationContext db, ILogger<HomeController> logger, IShortUrlServces shortUrlServces)
        {
            _db = db;
            _logger = logger;
            _shortUrlServces = shortUrlServces;
        }

        [HttpPost]
        [Route("createShort")]
        public async Task<ActionResult> CreateShort(string longUrl)
        {
            try
            {
                _logger.LogInformation("Запрос Redirect получен");

                Url model = new Url();

                //Uri uri = new Uri(longUrl);

                Uri uriResult;

                bool check = Uri.TryCreate(longUrl, UriKind.Absolute, out uriResult) 
                    && (uriResult.Scheme == Uri.UriSchemeHttp 
                    || uriResult.Scheme == Uri.UriSchemeHttps);


                if (check == true)
                {
                    var search = await _db.Urls.FirstOrDefaultAsync(u => u.LongUrl == longUrl);
                    if (search == null)
                    {
                        var result = await _shortUrlServces.ShortUrlAsync(longUrl, model);
                        _logger.LogInformation("Запрос Redirect выполнен");
                        return Ok(result);
                    }
                    else
                    {
                        //var result = await _shortUrlServces.ReturnUrlInDBAsync(search.Id); 
                        _logger.LogInformation("Запрос Redirect выполнен");
                        return Ok(search);
                    }
                    
                }
                return Ok("Некорректный адрес");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("redirect")]
        public async Task<IActionResult> GetShortUrl()
        {
            try
            {
                _logger.LogInformation("Запрос redirect получчен");
                var result = _db.Urls;

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

    }
}
