using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace PruningLink.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private ApplicationContext _db;
        private ILogger<HomeController> _logger;
        private IShortUrlServces _shortUrlServces;
        private IUrlServices _urlServices;

        public HomeController(ApplicationContext db, ILogger<HomeController> logger, 
            IShortUrlServces shortUrlServces, IUrlServices urlServices)
        {
            _db = db;
            _logger = logger;
            _shortUrlServces = shortUrlServces;
            _urlServices = urlServices;
        }

        // Метод для создание обрезанных ссылок
        [HttpPost]
        [Route("CreateShortUrl")]
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
                        return Ok(result.HashUrl);
                    }
                    // Если запись емеется в БД то возвраащается она
                    else
                    {
                        //var result = await _shortUrlServces.ReturnUrlInDBAsync(search.Id); 
                        _logger.LogInformation("Запрос Redirect выполнен");
                        return Ok(search.HashUrl);
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
        [Route("/Redirect/{hash}")]
        public async Task<IActionResult> RedirectUrl(/*RouteContext context,*/ string hash)
        {
            try
            {
                var searh = await _db.Urls.FirstOrDefaultAsync(x => x.HashUrl == hash);
                if (searh != null)
                {
                   return Redirect(searh.LongUrl);
                }
               
                _logger.LogInformation("Запрос Redirect получчен");
                var result = _db.Urls;
              
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            
        }

        // Вывод данных из БД
        [HttpGet]
        [Route("GetShortUrl")]
        public async Task<IActionResult> GetShortUrl()
        {
            try
            {
                _logger.LogInformation("Запрос GetShortUrl получчен");
                var result = _db.Urls;
               
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [Route("/RefactorUrl")]
        public async Task<IActionResult> RefactorUrl(int id,string url)
        {
            try
            {
                _logger.LogInformation("Запрос RefactorUrl получчен");
                var result = await _urlServices.RefactorUrl(id, url);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/DeletedUrl/{id}")]
        public async Task<IActionResult> DeletedUrl(int id)
        {
            try
            {
                _logger.LogInformation("Запрос DeletedUrl получчен");
                var result = await _urlServices.DeletedUrl(id);
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
