namespace kasuNhentaiCS;

public class nhentaiURL
{
    private static readonly Regex __domain = new(@"\.(?<domain>.*)", RegexOptions.Compiled);
    private static readonly Regex __pathname = new(@"(net|to)/(?<path>.*)", RegexOptions.Compiled);
    private static readonly Regex __bookId = new(@"/g/(?<id>.*)", RegexOptions.Compiled);
    private string _domain;
    private string _pathname;
    private string _firstParameter;
    private string _secondParameter;
    private string _bookId ;
    private string _href;
    private string _search;

    /// <summary>
    /// It Parses A <see langword="nhentai"/> URL into an object.
    /// </summary>
    /// <param name="url">full link of the <see langword="nhentai"/> url.</param>
    /// <returns><see cref="nhentaiURL"/> object.</returns>
    public nhentaiURL(string url)
    {
        _href = url;
        _domain = Regex.Replace(__domain.Match(url).ToString(), @"/.*","", RegexOptions.Compiled);
        _pathname = __pathname.Match(url).Groups["path"].ToString();
        _bookId = __bookId.Match(url).Groups["id"].ToString();
        _firstParameter = Regex.Match(_pathname, @"(.*?)/").Groups[1].ToString();
        _secondParameter = Regex.Match(_pathname, @"/(.*)").Groups[1].ToString();
        _search = Regex.Match(_secondParameter, @"\?q=(.*)").Groups[1].ToString();
    }

    public string bookId { get { return _bookId; } }
    public string domain { get { return _domain; } }
    public string href { get { return _href; } }
    public string pathname { get { return _pathname; } }
    public string search { get { return _search; } }
    public string firstDir { get { return _firstParameter; } }
    public string secondDir { get { return _secondParameter; } }
}

