using Swashbuckle.AspNetCore.SwaggerUI;

namespace PruningLink.Services
{
    public class UrlServices: IUrlServices
    {
		private ApplicationContext _db;
		public UrlServices(ApplicationContext db)
		{
			_db = db;
		}

        public async Task<Url> DeletedUrl(int id)
        {
			try
			{
				var search = await _db.Urls.FirstOrDefaultAsync(u => u.Id == id);
				if (search != null)
				{
					_db.Urls.Remove(search);
					await _db.SaveChangesAsync();
					return search;
				};
				return null;
			}
			catch (Exception)
			{

				throw;
			}
        }

		public async Task<Url> RefactorUrl(int id, string LongUrl)
		{
			try
			{
                var search = await _db.Urls.FirstOrDefaultAsync(u => u.Id == id);
                if (search != null)
				{
					search.LongUrl = LongUrl;
					_db.Update(search);
					await _db.SaveChangesAsync();
					var res = await _db.Urls.FirstOrDefaultAsync(u => u.Id == id);
					return res;
                }
				return null;
            }
			catch (Exception)
			{

				throw;
			}
		}
    }
}
