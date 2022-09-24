using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PruningLink.Model;
using PruningLink.Model.Entity;
using System.Formats.Asn1;
using System.Text;
using System.Web;

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

        // Метод для создание обрезанных ссылок
        [HttpPost]
        [Route("createShort")]
        public async Task<ActionResult> CreateShort(string longUrl)
        {
            try
            {
                _logger.LogInformation("Запрос Redirect получен");

                Url model = new Url();

                Uri uriResult;

                // Проверка на правильность переданной ссылки
                bool check = Uri.TryCreate(longUrl, UriKind.Absolute, out uriResult) 
                    && (uriResult.Scheme == Uri.UriSchemeHttp 
                    || uriResult.Scheme == Uri.UriSchemeHttps);


                if (check == true)
                {
                    var search = await _db.Urls.FirstOrDefaultAsync(u => u.LongUrl == longUrl);
                    // Проверка на существующий Url в БД
                    // Если она не существует то создается новая запись
                    if (search == null)
                    {
                        var result = await _shortUrlServces.ShortUrlAsync(longUrl, model);
                        _logger.LogInformation("Запрос Redirect выполнен");
                        return Ok(result.ShortUrl);
                    }
                    // Если запись емеется в БД то возвраащается она
                    else
                    {
                        //var result = await _shortUrlServces.ReturnUrlInDBAsync(search.Id); 
                        _logger.LogInformation("Запрос Redirect выполнен");
                        return Ok(search.ShortUrl);
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
        // Редирект
        [HttpGet]
        [Route("redirect/{shortUrl}")]
        public async Task<IActionResult> RedirectUrl(/*RouteContext context,*/ [FromQuery]string shortUrl)
        {
            try
            {
                string myencod = HttpUtility.UrlEncode(shortUrl);
                var d = myencod;
                //string url = context.HttpContext.Request.Path.Value.TrimEnd('/');
                var searh = await _db.Urls.FirstOrDefaultAsync(x => x.ShortUrl == myencod);
                if (searh != null)
                {
                   return Redirect(searh.LongUrl);
                }
                //if (url.StartsWith("/redirect", StringComparison.OrdinalIgnoreCase))
                //{
                //    context.Handler = async ctx =>
                //    {
                //        ctx.Response.ContentType = "text/html;charset=utf-8";
                //        Redirect()
                //    };
                //}
                _logger.LogInformation("Запрос redirect получчен");
                var result = _db.Urls;
              
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            
        }

        // Вывод данных ид БД
        [HttpGet]
        [Route("GetShortUrl")]
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
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

    }
}
