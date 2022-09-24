namespace PruningLink.Model.Entity;

public class Url
{
    public int Id { get; set; }
    public string LongUrl { get; set; } = string.Empty;
    public string ShortUrl { get; set; } = string.Empty;
    public string HashUrl { get; set; } = string.Empty;
    public int Count { get; set; }

}
