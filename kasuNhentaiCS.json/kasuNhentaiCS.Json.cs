// cSPELL: ignoreRegExp /kasuNhentaiCS/g
global using System.Text.Json;
global using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Dynamic;
using System.IO;

#nullable enable annotations

namespace kasuNhentaiCS.Json;

/// <summary>
/// Json parser. <br/>
/// by: https://github.com/IchimakiKasura
/// </summary>
public sealed class JsonDeserializer
{
    private readonly static Regex nth = new(@":(?<num>.*)", RegexOptions.Compiled);
    private JsonElement DataList;
    private JsonElement TemporaryList;

    /// <summary>
    /// It parses the json file to access its properties by using a <see cref="selector"/>.
    /// </summary>
    /// <param name="SerializedJson">Serialized Object here or an Object in a string format.</param>
    /// <param name="options">Provides the ability for the user to define custom behavior when parsing JSON to create a <see cref="JsonDocument"/></param>
    public JsonDeserializer(string SerializedJson, JDO options = default)
    {
        JsonDocumentOptions ironic = default(JsonDocumentOptions);
        
        try{
            // kinda what the fuck?
            ironic = JsonSerializer.Deserialize<JsonDocumentOptions>(JsonSerializer.Serialize(options));
        } catch {}

        DataList = JsonDocument.Parse(SerializedJson, ironic).RootElement;
    }
    /// <summary>
    /// Selector, It's kinda like the html selector "div>a>span"
    /// </summary>
    /// <example> parseData.selector("array:2>object>string") </example>
    /// <param name="selector">location of where you want to get idk. <br/>
    /// example: "sample>yo>epic"</param>
    /// <returns><see cref="JsonElement"/> or <see cref="string"/> or <see cref="int"/></returns>
    public dynamic selector(string selector)
    {
        TemporaryList = DataList;

        string[] selectors;

        selectors = selector.Split(">");

        foreach (var item in selectors)
        {
            if (Regex.IsMatch(item, @":"))
            {
                int num = int.Parse(nth.Match(item).Groups["num"].ToString());

                TemporaryList = TemporaryList.GetProperty(nth.Replace(item, ""))[num];
            }
            else
            {
                TemporaryList = TemporaryList.GetProperty(item);
            }
        }

        string Data = TemporaryList.GetRawText();

        if (Regex.IsMatch(Data, @"^""")) return TemporaryList.GetString();

        if (Regex.IsMatch(Data, @"^[0-9]+")) return TemporaryList.GetInt32();

        if (Regex.IsMatch(Data, @"^\["))
        {
            _selector obj = new(){
                data = TemporaryList,
                count = TemporaryList.GetArrayLength()
            };
            return obj;
        }

        if (Regex.IsMatch(Data, @"^\{")) return TemporaryList;

        return TemporaryList;
    }
    /// <summary>
    /// Gets a string that represents the original input data backing this value.
    /// </summary>
    /// <returns><see cref="JsonElement.GetRawText"/></returns>
    public string rawText()
    {
        return TemporaryList.GetRawText();
    }
    /// <summary>
    /// Converts the value of a specified type into a JSON string.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Provides options to be used with <see cref="JsonSerializer"/>.</param>
    /// <returns>A JSON string representation of the value.</returns>
    public static string serializeObj(object value, JsonSerializerOptions? options = default)
    {
        return JsonSerializer.Serialize(value, options); ;
    }
    /// <summary>
    /// Asynchronously converts a value of a type specified by a generic type parameter to UTF-8 encoded JSON text and writes it to a stream.
    /// </summary>
    /// <param name="stream">The UTF-8 stream to write to.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Provides options to be used with <see cref="JsonSerializer"/>.</param>
    /// <param name="InputType">The type of the value to convert.</param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    public static Task serializeObjAsync(Stream stream, object value, Type? InputType, JsonSerializerOptions? options = default, CancellationToken cancellationToken = default)
    {
        return JsonSerializer.SerializeAsync(stream, value, InputType, options, cancellationToken);
    }
    /// <summary>
    /// [selector alternative] <br/>
    /// Parse a string Json into an object.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="options">Provides options to be used with <see cref="JsonSerializer"/>.</param>
    /// <returns>Properties of the given JSON string</returns>
    public static dynamic deserializeObj(string value, JsonSerializerOptions? options = null)
    {
        if (string.IsNullOrEmpty(value)) return null;
        try
        {
            return JsonSerializer.Deserialize<ExpandoObject>(value, options);
        }
        catch
        {
            return null;
        }
    }
    
    // I don't really know how the fuck these guys work. These serialize and deserialize.

    /// <summary>
    /// [selector alternative] <br/>
    ///  (ASYNC) Parse a string Json into an object.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="options">Provides options to be used with <see cref="JsonSerializer"/>.</param>
    /// <returns>Properties of the given JSON string</returns>
    public static Task deserializeObjAsync(string value, JsonSerializerOptions? options = null)
    {
        if (string.IsNullOrEmpty(value)) return null;
        try
        {
            var Data = Task.Run(()=>JsonSerializer.Deserialize<ExpandoObject>(value, options));
            return Data;
        }
        catch
        {
            return null;
        }
    }
}
