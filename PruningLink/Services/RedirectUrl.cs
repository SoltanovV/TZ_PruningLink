using Microsoft.AspNetCore.Authentication;

namespace PruningLink.Services
{
    public class RedirectUrl //: IRedirectUrl
    {
        ApplicationContext _db;
        public RedirectUrl(ApplicationContext db)
        {
            _db = db;
        }
        //public async Task<Url> RedirectUrl(string url)
        //{
        //    try
        //    {
        //        var search = await _db.Urls.FirstOrDefaultAsync(x => x.ShortUrl == url)
        //        if (true)
        //        {
                    
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
