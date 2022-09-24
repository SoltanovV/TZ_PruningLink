namespace PruningLink.Services.Interface
{
    public interface IRedirectUrl
    {
        public Task<Url> RedirectUrl(Url url);
    }
}
