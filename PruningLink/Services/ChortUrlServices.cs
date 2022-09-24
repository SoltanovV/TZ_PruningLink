using PruningLink.Model;
using PruningLink.Model.Entity;
using PruningLink.Services.Interface;
using System.Text;

namespace PruningLink.Services
{
    public class ChortUrlServices : IShortUrlServces
    {
        private ApplicationContext _db;
        public ChortUrlServices(ApplicationContext db)
        {
            _db= db;
        }
        public async Task<Url> ShortUrlAsync(string longUrl, Url model)
        {
			try
			{
                Uri uri = new Uri(longUrl);

                var hashUrl = Convert.ToBase64String(Encoding.UTF8.GetBytes(longUrl.Remove(6)));
                var shortUrl = uri.Scheme + "://" + "localhost:7180/api/Home/redirect/" + uri.DnsSafeHost + "/" + hashUrl;

                model.LongUrl = longUrl;
                model.ShortUrl = shortUrl;
                model.HashUrl = hashUrl;

                var result = await _db.Urls.AddAsync(model);
                await _db.SaveChangesAsync();

                return result.Entity;

            }
			catch (Exception)
			{

				throw;
			}
        }
        //public async Task<Url> ReturnUrlInDBAsync(int id)
        //{
        //    try
        //    {
        //        _db
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
