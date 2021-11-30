// cSPELL: ignoreRegExp /kasuNhentaiCS/g
global using System.ComponentModel;
namespace kasuNhentaiCS.library;
/// <summary>
/// Parser's Regex listing.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal struct matcherRegex
{
    /// <summary>
    /// Gets the <c>Time Element</c> value in the HTML string
    /// </summary>
    public static readonly Regex TimeReg = new(@"<time (.*?)>(?<date>.*)<\/time>", RegexOptions.IgnoreCase | RegexOptions.Compiled); 
    /// <summary>
    /// Gets the <c>Thumbnail Element</c> value in the HTML string
    /// </summary>
    public static readonly Regex ThumbnailReg = new(@"<img class=.* data-src=""(?<thumbs>.*?)"".*?src="".*?"" \/>", RegexOptions.IgnoreCase | RegexOptions.Compiled); 
    /// <summary>
    /// Gets the <c>Id Element</c> value in the HTML string
    /// </summary>
    public static readonly Regex IdReg = new(@"<a class=""gallerythumb"" href=""(?<id>.*?)/1/""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    /// <summary>
    /// Gets the "Script's JSON.parse" in the HTML <br/>
    /// this includes all the info of the book. [unparsed] 
    /// </summary>
    public static readonly Regex BookNet = new(@"JSON\.parse\(""(?<parse>.*)""\)", RegexOptions.Compiled);
    /// <summary>
    /// Gets the "Script's JSON.parse" in the HTML <br/>
    /// this includes all the info of the book. [unparsed] 
    /// </summary>
    public static readonly Regex BookTo = new(@"N\.gallery\((?<parse>.*?)\);", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    /// <summary>
    /// Gets all the <c>Div Elements</c> as list or array in the page that is a "book" or idk what you call it.
    /// </summary>
    public static readonly Regex pageInfoRegex = new(@"<div class=""gallery"".*?caption"">.*?<", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
    /// <summary>
    /// Its for the URL replacer like <br/> nhentai/tag/, nhentai/artist etc..
    /// </summary>
    public static readonly Regex URLregex = new(@"language\/.*|tag\/.*|character\/.*|artist\/.*|parody\/.*|group\/.*|category\/.*|(search\/|search\?q=).*|\?q=.*", RegexOptions.Compiled);
    /// <summary>
    /// Its still a URL replacer but this time it is really for the URL.
    /// </summary>
    public static readonly Regex TAGregex = new(@"https:\/\/nhentai\.(to|net)|\/language|\/tag|\/character|\/artist|\/parody|\/group|\/category|\/", RegexOptions.Compiled);

}
