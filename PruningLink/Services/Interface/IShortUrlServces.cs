using PruningLink.Model.Entity;

namespace PruningLink.Services.Interface;

public interface IShortUrlServces
{
    public Task<Url> ShortUrlAsync(string longUrl, Url model);
   // public Task<Url> ReturnUrlInDBAsync(int id);

}
