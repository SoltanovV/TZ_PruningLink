namespace PruningLink.Services.Interface
{
    public interface IUrlServices
    {
        public Task<Url> DeletedUrl(int id);
        public Task<Url> RefactorUrl(int id, string LongUrl);
    }
}
