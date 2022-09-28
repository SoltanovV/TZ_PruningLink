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
                
                var hashUrl = RandomString(5);
                var shortUrl = uri.Scheme + "://" + "localhost:7180/Redirect/" + hashUrl;

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

        // Метод для рандомного хеша
        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
    }
}
