// cSPELL: ignoreRegExp /kasuNhentaiCS|[n|N]hentai/g
#nullable enable annotations
namespace kasuNhentaiCS.library;

/// <summary>
/// Properties for the <see cref="Matcher.BOOKnet(string)"/>.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal struct BookData
{
    /// <summary>
    /// Get the Id of the object.
    /// </summary>
    public string id { readonly get; set; }
    /// <summary>
    /// Get the body or data of the object.
    /// </summary>
    public string body { readonly get; set; }
    /// <summary>
    /// Get the time or date of the object.
    /// </summary>
    public string time { readonly get; set; }
    /// <summary>
    /// Get the thumbnail url of the object.
    /// </summary>
    public string thumbnail { readonly get; set; }
    /// <summary>
    /// Get the image page url of the object.
    /// </summary>
    public string img_source { readonly get; set; }
    /// <summary>
    /// Get the Status if its a success of a failed request.
    /// </summary>
    public string status { readonly get; set; }
    
}

/// <summary>
/// Properties for the <see cref="Matcher.PageInfo(string)"/>.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal struct PageData
{
    /// <summary>
    /// Get the body or data of the object.
    /// </summary>
    public List<string> body { readonly get; set; }
    /// <summary>
    /// Get the total array length of the data.
    /// </summary>
    public int totalPage { readonly get; set; }
    /// <summary>
    /// Get the Status if its a success of a failed request.
    /// </summary>
    public string status { readonly get; set; }
}

/// <summary>
/// Book Object properties.
/// </summary>
[Serializable]
[EditorBrowsable(EditorBrowsableState.Always)]
public struct BookObj
{
    /// <summary>
    /// • ID or Code or whatever you call.
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// • Url of the selected code.
    /// </summary>
    public string url { get; set; }
    /// <summary>
    /// • titles?
    /// </summary>
    public Title title { get; set; }
    /// <summary>
    /// • images?
    /// </summary>
    public Images images { get; set; }
    /// <summary>
    /// • tags?
    /// </summary>
    public Tag_table tag_table { get; set; }
    /// <summary>
    /// • haha 69 pages seems cool.
    /// </summary>
    public int number_pages { get; set; }
    /// <summary>
    /// • Hokusai made The Dream of the Fisherman's Wife in 1814, the earliest known Tentacle hentai.
    /// In 1722 the government made a law banning hentai manga, which means it was common even earlier.
    /// Suzumi-fune is probably the oldest hentai anime, it's from 1932.
    /// </summary>
    public string uploaded { get; set; }
}

/// <summary>
/// Page Object properties.
/// </summary>
[Serializable]
[EditorBrowsable(EditorBrowsableState.Always)]
public struct PageObj
{   
    /// <summary>
    /// • Gives the Url.
    /// </summary>
    public string CurrentUrl { get; set; }
    /// <summary>
    /// • idk.
    /// </summary>
    public string typePage { get; set; }
    /// <summary>
    /// • Gives an index number of what page you are currently in.
    /// </summary>
    public int CurrentPage { get; set; }
    /// <summary>
    /// • Total arrays.
    /// </summary>
    public int Total { get; set; }
    /// <summary>
    /// • Total Pages ofc.
    /// </summary>
    public int TotalPage { get; set; }
    /// <summary>
    /// • results in array. wha-
    /// </summary>
    public resultsObject[] results { get; set; }
}

/// <summary>
/// Waoh how did you find this? <br/>
/// this is illegal!
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public struct resultsObject
{
    /// <summary>
    /// • ID or Code or whatever you call.
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// • taitoru.
    /// </summary>    
    public string title { get; set; }
    /// <summary>
    /// • Cover picture.
    /// </summary>    
    public string thumbnail { get; set; }
    /// <summary>
    /// • It's just the combination of the currenturl with id innit.
    /// </summary>    
    public string url { get; set; }
    /// <summary>
    /// • NANI TTE-
    /// </summary>    
    public string languages { get; set; }
}
/// <summary>
/// Waoh how did you find this? <br/>
/// this is illegal!
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public struct Title
{
    /// <summary>
    /// original
    /// </summary>
    public string origin { get; set; }
    /// <summary>
    /// トランスレイト??? wait it's supposed to be in english cuz' it's translated.
    /// </summary>
    public string translated { get; set; }
}
/// <summary>
/// Waoh how did you find this? <br/>
/// this is illegal!
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public struct Images
{
    /// <summary>
    /// • Doujin cover image.
    /// </summary>
    public string cover { get; set; }
    /// <summary>
    /// • Doujin image source.
    /// </summary>
    public string page_source { get; set; }
}
/// <summary>
/// Waoh how did you find this? <br/>
/// this is illegal!
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public struct Tag_table
{
    /// <summary>
    /// • I Like konosuba
    /// </summary>
    public string parodies { get; set; }
    /// <summary>
    /// • I love megumin
    /// </summary>
    public string characters { get; set; }
    /// <summary>
    /// • what?
    /// </summary>
    public string tag { get; set; }
    /// <summary>
    /// • I don't really know about this
    /// </summary>
    public string artist { get; set; }
    /// <summary>
    /// • Can't think 1 group tag.
    /// </summary>
    public string groups { get; set; }
    /// <summary>
    /// • english ofc.
    /// </summary>
    public string languages { get; set; }
    /// <summary>
    /// • ca
    /// </summary>
    public string categories { get; set; }
}
