// cSPELL: ignoreRegExp /kasuNhentaiCS|[n|N]hentai/g
global using System.Text.RegularExpressions;
global using System.Collections.Generic;
global using System;
using System.Threading.Tasks;
using kasuNhentaiCS.library;
using kasuNhentaiCS.Json;
using System.Net.Http;
using System.Linq;

namespace kasuNhentaiCS;
[EditorBrowsable(EditorBrowsableState.Never)]
internal class fetcher
{
    public static string fetch(string url)
    {
        TimeSpan delay = default;
        HttpResponseMessage res;
        int status;

        try
        {
            using (var request = new HttpClient())
            {
                request.DefaultRequestVersion = new Version("2.0");
                var result = request.GetAsync(url).Result;
                status = (int)result.StatusCode;
                res = result;
            }

            if (status != 200)
            {
                if (status == 429)
                {
                    delay = res.Headers.RetryAfter.Delta ?? TimeSpan.FromSeconds(1);
                    Task.Delay(delay);
                }
                return fetch(url);
            }
            else return res.Content.ReadAsStringAsync().Result;
        }
        catch
        {
            throw new Exception("[404 | ERROR] URL is Invalid");
        }
    }
}

/// <summary>
/// A class for Book and Page. <br/>
/// For this time only "book" method is currently supported <br/>
/// while "page" method is still work in progress.
/// </summary>
public static class Parser
{

    /// <summary>
    /// Nhentai.net | Nhentai.to<br/>
    /// Request Book object data from the url 
    /// </summary>
    /// <param name="url">Accepts <see cref="nhentaiURL"/>.</param>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.Exception"></exception>
    public static BookObj book(nhentaiURL url)
    {
        string parodies = "none";
        string chr = "none";
        string tags = "none";
        string Artist = "none";
        string Groups = "none";
        string Lang = "none";
        string Ctg = "none";
        string newUrl;
        BookData data;

        if (url.firstDir != "g")
        {
            throw new ArgumentException("[Book] URL is invalid!", nameof(url));
        }

        if (url.domain == ".net")
        {
            data = Matcher.BOOKnet(url.href);
            newUrl = $"https://nhentai.net{data.id}";
        }
        else
        {
            data = Matcher.BOOKto(url.href);
            newUrl = $"https://nhentai.to{data.id}";
        }

        var newBody = new JsonDeserializer(data.body, new JDO
        {
            AllowTrailingCommas = true
        });

        for (int i = 0; i < newBody.selector($"tags").count; i++)
        {
            string value = newBody.selector($"tags:{i}>name");
            switch (newBody.selector($"tags:{i}>type"))
            {
                case "tag":
                    if (tags == "none")
                    {
                        tags = value;
                    }
                    else tags += $", {value}";
                    break;
                case "character":
                    if (chr == "none")
                    {
                        chr = value;
                    }
                    else chr += $", {value}";
                    break;
                case "parody":
                    if (parodies == "none")
                    {
                        parodies = value;
                    }
                    else parodies += $", {value}";
                    break;
                case "artist":
                    if (Artist == "none")
                    {
                        Artist = value;
                    }
                    else Artist += $", {value}";
                    break;
                case "language":
                    if (Lang == "none")
                    {
                        Lang = value;
                    }
                    else Lang += $", {value}";
                    break;
                case "category":
                    if (Ctg == "none")
                    {
                        Ctg = value;
                    }
                    else Ctg += $", {value}";
                    break;
                case "group":
                    if (Groups == "none")
                    {
                        Groups = value;
                    }
                    else Groups += $", {value}";
                    break;
            }
        }

        return new BookObj
        {
            id = int.Parse(data.id.Replace("/g/", "")),
            url = url.href,
            title = new()
            {
                origin = newBody.selector("title>japanese"),
                translated = newBody.selector("title>english")
            },
            images = new()
            {
                cover = data.thumbnail,
                page_source = data.img_source
            },
            tag_table = new()
            {
                parodies = parodies,
                characters = chr,
                tag = tags,
                artist = Artist,
                groups = Groups,
                languages = Lang,
                categories = Ctg
            },
            number_pages = newBody.selector("num_pages"),
            uploaded = data.time
        };
    }
    /// <summary>
    /// Nhentai.net | Nhentai.to<br/>
    /// Request Book object data from the url 
    /// </summary>
    /// <param name="url">".net" or ".to" are fully supported.<br/>
    /// but It doesnt support numbers yet only full links.<br/>
    /// <example>
    /// e.g: <br/>
    /// https://nhentai.net/g/227834/ <br/>
    /// https://nhentai.to/g/132446/
    /// </example><br/>
    /// [Warning] You are using a string type.<br/>
    /// Though it still supports it but its better to <br/>
    /// parse the link using <see cref="nhentaiURL"/> Or you're just lazy no offense.
    /// </param>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.Exception"></exception>
    public static BookObj book(string url)
    {
        var parsedURL = new nhentaiURL(url);
        return book(parsedURL);
    }
    /// <summary>
    /// Nhentai.net | Nhentai.to<br/>
    ///  (ASYNC) Request Book object data from the url
    /// </summary>
    /// <param name="url">Accepts <see cref="nhentaiURL"/>.</param>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.Exception"></exception>
    public static async Task<BookObj> bookAsync(nhentaiURL url)
    {
        BookObj Data = await Task.Run(() => book(url));
        return Data;
    }
    /// <summary>
    /// Nhentai.net | Nhentai.to<br/>
    ///  (ASYNC) Request Book object data from the url
    /// </summary>
    /// <param name="url">".net" or ".to" are fully supported.<br/>
    /// but It doesnt support numbers yet only full links.<br/>
    /// <example>
    /// e.g: <br/>
    /// https://nhentai.net/g/227834/ <br/>
    /// https://nhentai.to/g/132446/
    /// </example><br/>
    /// [Warning] You are using a string type.<br/>
    /// Though it still supports it but its better to <br/>
    /// parse the link using <see cref="nhentaiURL"/> Or you're just lazy no offense.
    /// </param>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.Exception"></exception>
    public static async Task<BookObj> bookAsync(string url)
    {
        var parsedURL = new nhentaiURL(url);
        BookObj Data = await Task.Run(() => book(parsedURL));
        return Data;
    }

    /// <summary>
    /// Nhentai.net | Nhentai.to<br/>
    /// Request Page object data from the url 
    /// </summary>
    /// <param name="num_page">Page yes page.</param>
    /// <param name="url">Accepts <see cref="nhentaiURL"/>.</param>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.Exception"></exception>
    public static PageObj page(nhentaiURL url, int? num_page = 1)
    {

        if (url.domain == "g")
        {
            throw new ArgumentException("[Page] URL is invalid!", nameof(url));
        }

        if (num_page == null || num_page == 0) num_page = 1;

        var pageURL = $"{url.href}?page={num_page}";

        if (url.firstDir == "search") pageURL = $"{url.href}&page={num_page}";

        var PageInfo = Matcher.PageInfo(url.href);
        List<string> data = PageInfo.body;
        var totalPage = PageInfo.totalPage;

        List<string> tags = new List<string>
        {
            "language",
            "tag",
            "character",
            "artist",
            "parody",
            "group",
            "category"
        };

        resultsObject[] dataList = new resultsObject[data.Count];

        foreach (var s in data.Select((value, i) => (value, i)))
        {
            int id = int.Parse(Regex.Match(s.value, @"href=""/g/(?<id>.*?)/""").Groups["id"].Value);
            string[] Lang = Regex.Match(s.value, @"data-tags=""(.*?)""").Value.Split(" ");
            string languages = "";

            foreach (string lang in Lang)
            {                
                if (url.domain == ".net")
                {
                    switch (lang.Replace("data-tags=\"", ""))
                    {
                        case "6346":
                            languages += "japansese, ";
                            break;
                        case "29963":
                            languages += "chinese, ";
                            break;
                        case "12227":
                            languages += "english, ";
                            break;
                        case "17249":
                            languages += "translated, ";
                            break;
                    }
                }
                else
                {
                    switch (lang)
                    {
                        case "2":
                            languages += "japansese, ";
                            break;
                        case "10197":
                            languages += "chinese, ";
                            break;
                        case "19":
                            languages += "english, ";
                            break;
                        case "17":
                            languages += "translated, ";
                            break;
                    }
                }
            }

            dataList[s.i] = new()
            {
                id = id,
                title = Regex.Match(s.value, @"caption"">(?<title>.*?)<").Groups["title"].Value,
                thumbnail = Regex.Match(s.value, @"data-src=""(?<thumb>.*?)""").Groups["thumb"].Value,
                url = $"{matcherRegex.URLregex.Replace(url.href, "")}g/{id}",
                languages = languages.Remove(languages.Length - 2)
            };
        }

        PageObj list = new()
        {
            CurrentUrl = url.href,
            typePage = "homepage",
            CurrentPage = (int)num_page,
            Total = data.Count,
            TotalPage = totalPage,
            results = dataList
        };

        for (var i = 0; i < tags.Count; i++)
        {
            if (url.href == tags[i])
            {
                list.typePage = $"{tags[i]} / {matcherRegex.TAGregex.Replace(url.href, "")}";
            }
        }

        if (url.firstDir == "search")
        {
            Regex urlReplace = new(@"https:\/\/nhentai\.(to|net)\/search|\?q=|\/|&", RegexOptions.Compiled);
            list.typePage = $"search / {urlReplace.Replace(url.href, "")}";
        }

        return list;
    }
    /// <summary>
    /// Nhentai.net | Nhentai.to<br/>
    ///  (ASYNC) Request Page object data from the url 
    /// </summary>
    /// <param name="num_page">Page yes page.</param>
    /// <param name="url">Accepts <see cref="nhentaiURL"/>.</param>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.Exception"></exception>
    public static async Task<PageObj> pageAsync(nhentaiURL url, int? num_page = 1)
    {
        PageObj Data = await Task.Run(() => page(url, num_page));
        return Data;
    }
    /// <summary>
    /// Nhentai.net | Nhentai.to<br/>
    /// Request Page object data from the url 
    /// </summary>
    /// <param name="num_page">Page yes page.</param>
    /// <param name="url">".net" or ".to" are fully supported.<br/>
    /// but It doesnt support numbers yet only full links.<br/>
    /// e.g: <br/>
    /// https://nhentai.net <br/>
    /// https://nhentai.net/search/?q=konosuba+aqua <br/>
    /// https://nhentai.net/tag/crossdressing <br/>
    /// https://nhentai.to/search?q=konosuba+aqua <br/>
    /// [Warning] You are using a string type.<br/>
    /// Though it still supports it but its better to <br/>
    /// parse the link using <see cref="nhentaiURL"/> Or you're just lazy no offense.
    /// </param>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.Exception"></exception>
    public static PageObj page(string url, int? num_page = 1)
    {
        var parserURL = new nhentaiURL(url);
        return page(parserURL, num_page);
    }

    /// <summary>
    /// Nhentai.net | Nhentai.to<br/>
    ///  (ASYNC) Request Page object data from the url 
    /// </summary>
    /// <param name="num_page">Page yes page.</param>
    /// <param name="url">".net" or ".to" are fully supported.<br/>
    /// but It doesnt support numbers yet only full links.<br/>
    /// e.g: <br/>
    /// https://nhentai.net <br/>
    /// https://nhentai.net/search/?q=konosuba+aqua <br/>
    /// https://nhentai.net/tag/crossdressing <br/>
    /// https://nhentai.to/search?q=konosuba+aqua <br/>
    /// [Warning] You are using a string type.<br/>
    /// Though it still supports it but its better to <br/>
    /// parse the link using <see cref="nhentaiURL"/> Or you're just lazy no offense.
    /// </param>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.Exception"></exception>
    public static async Task<PageObj> pageAsync(string url, int? num_page = 1)
    {
        var parseURL = new nhentaiURL(url);
        PageObj Data = await Task.Run(() => page(parseURL, num_page));
        return Data;
    }
}