// cSPELL: ignoreRegExp /kasuNhentaiCS|nhentai|kagebunshin|esults/g
using kasuNhentaiCS.library;

namespace kasuNhentaiCS;

/// <summary>
/// [Brain of the operation]<br/>
/// Parses the Html string into an object. 
/// </summary>
internal struct _Matcher
{
    // no i wont talk about why i named it kagebunshin
    private static dynamic kagebunshin(string html)
    {
        var time = matcherRegex.TimeReg.Match(html).Groups["date"].ToString();
        var thumbnail = matcherRegex.ThumbnailReg.Match(html).Groups["thumbs"].ToString();
        var id = matcherRegex.IdReg.Match(html).Groups["id"].ToString();
        var img_source = thumbnail.Replace("/cover.jpg", "");
        return new
        {
            id,
            time,
            thumbnail,
            img_source
        };
    }
    /// <summary>
    /// Requests for the <see langword="Nhentai.net"/> website's Html into string and parses it in to an object. 
    /// </summary>
    /// <param name="url"><see langword="Nhentai.net"/> book url <br/> Examples: <br/>
    /// <example>
    /// https://nhentai.net/g/228922 <br/>
    /// https://nhentai.net/g/128323 <br/>
    /// https://nhentai.net/g/1 <br/>
    /// </example>
    /// </param>
    /// <returns>A <see cref="BookData"/> representation of the parsed html</returns>
    public static BookData BOOKnet(string url)
    {
        string html = "";
        string status = "";
        try
        {
            html = fetcher.fetch(url);
            status = "success";
        }
        catch (Exception e)
        {
            status = e.Message;
        }
        var clones = kagebunshin(html);
        string body = Regex.Unescape(Regex.Unescape(matcherRegex.BookNet.Match(html).Groups["parse"].ToString()));

        return new()
        {
            id = clones.id,
            body = body,
            time = clones.time,
            thumbnail = clones.thumbnail,
            img_source = clones.img_source.Replace("t.nhentai", "i.nhentai"),
            status = status
    };
    }
    /// <summary>
    /// Requests for the <see langword="Nhentai.to"/> website's Html into string and parses it in to an object. 
    /// </summary>
    /// <param name="url"><see langword="Nhentai.to"/> book url <br/> Examples: <br/>
    /// <example>
    /// https://nhentai.to/g/27382 <br/>
    /// https://nhentai.to/g/33234 <br/>
    /// https://nhentai.to/g/12345 <br/>
    /// </example>
    /// </param>
    /// <returns>A <see cref="BookData"/> representation of the parsed html</returns>
    public static BookData BOOKto(string url)
    {
        string html = "";
        string status = "";

        try
        {
            html = fetcher.fetch(url);
            status = "success";
        }
        catch (Exception e)
        {
            status = e.Message;
        }
        var clones = kagebunshin(html);
        var body = Regex.Unescape(matcherRegex.BookTo.Match(html.Replace(@"[\r|\n]+| ", "")).Groups["parse"].ToString());
        return new()
        {
            id = clones.id,
            body = body,
            time = clones.time,
            thumbnail = clones.thumbnail,
            img_source = clones.img_source,
            status = status
    };
    }
    /// <summary>
    /// Requests for the <see langword="Nhentai.net"/> or <see langword="Nhentai.to"/> website's Html into string and parses it in to an object.
    /// </summary>
    /// <param name="url"><see langword="Nhentai.net"/> or <see langword="Nhentai.to"/> url <br/> Examples: <br/>
    /// <example>
    /// For .net: <br/>
    /// https://nhentai.net/ <br/>
    /// https://nhentai.net/tag/crossdressing <br/>
    /// and more..
    /// For .to: <br/>
    /// https://nhentai.to/ <br/>
    /// https://nhentai.to/tag/crossdressing <br/>
    /// and more..
    /// </example>
    /// Heck you can even get the "More Like This" section in the <br/>
    /// book url. But it's only available in the .net domain
    /// </param>
    /// <returns>A <see langword="PageData"/> representation of the parsed html</returns>
    public static PageData PageInfo(string url)
    {
        var html = fetcher.fetch(url);
        int totalPage;

        if (Regex.IsMatch(html, @"<h2>(0|No) [r|R]esults")) 
            return new(){
                status = "No results were found"
            };

        List<string> body = new List<string>();

        foreach (Match m in matcherRegex.pageInfoRegex.Matches(html))
        {
            body.Add(m.Value);
        }

        try
        {
            totalPage = int.Parse(Regex.Match(html, @"page=(?<page>.*?)""").Groups["page"].ToString());
        }
        catch
        {
            totalPage = 1;
        }

        return new()
        {
            body = body,
            totalPage = totalPage,
            status = "success"
        };
    }
}