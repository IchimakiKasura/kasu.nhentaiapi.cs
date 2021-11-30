using System.ComponentModel;
// cSPELL: ignoreRegExp /kasuNhentaiCS/g
namespace kasuNhentaiCS.Json;

/// <summary>
/// Waoh how did you find this? <br/>
/// this is illegal!
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public struct _selector
{
    /// <summary>
    /// Get the <see cref="JsonElement"/> data.
    /// </summary>
    /// <value></value>
    public JsonElement data { get; set; }
    /// <summary>
    /// Get the total length of the array.
    /// </summary>
    /// <value></value>
    public int count { get; set; }
}
/// <summary>
/// Its a short version for <see cref="JsonDocumentOptions"/>. <br/>
/// Provides options to be used with <see cref="JsonSerializer"/> or <see cref="JsonDocument"/>
/// </summary>
[Serializable]
public struct JDO
{
    /// <summary>
    /// Gets or sets a value that indicates whether an extra comma at the end of a list of JSON values in an object or array is allowed (and ignored) within the JSON payload being read.
    /// </summary>
    /// <returns><see langword="true"/> if an extra comma at the end of a list of JSON values in an object or array is allowed; otherwise, <see langword="false"/>. Default is <see langword="false"/></returns>
    public bool AllowTrailingCommas { readonly get; set; }
    /// <summary>
    /// Gets or sets a value that determines how the JsonDocument handles comments when reading through the JSON data.
    /// </summary>
    /// <returns>One of the enumeration values that indicates how comments are handled.</returns>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public JsonCommentHandling CommentHandling { readonly get; set; }
    /// <summary>
    /// Gets or sets the maximum depth allowed when parsing JSON data, with the default (that is, 0) indicating a maximum depth of 64.
    /// </summary>
    /// <returns>The maximum depth allowed when parsing JSON data.</returns>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public int MaxDepth { readonly get; set; }
}
