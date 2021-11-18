# kasuNhentaiCS.Json
## _**Usage :**_
```cs
using kasuNhentaiCS.Json; // Always include this

class Program
{
    string _object = @"{""_String"":""string"",""_Array"":[""array 1"",""array 2""],""_number"": ""123"",""__object"":{""_string"":""object string"",""_stringTwo"":""object string 2""}}";

    static void Main()
    {
        var _selector = new JsonDeserializer(_object);
        _selector.selector("_String");    //output: string   Type: [System.String]
        _selector.selector("_Array:0");   //output: array 1  Type: [System.String]
        _selector.selector("_number");    //output: 123      Type: [System.Int32]

        var _default = JsonDeserializer.deserializeObj(_object);
        _default._String;                 //output: string   Type: [System.Text.Json.JsonElement]
        _default._Array[0];               //output: array 1  Type: [System.Text.Json.JsonElement]
        _default._number;                 //output: 123      Type: [System.Text.Json.JsonElement]
    }
}
```
---
## _**What's the difference?**_

- **SELECTOR**
    - **Selector Pros**
        - [Selector](#usage- "kasuNhentaiCS.Json.JsonDeserializer.selector") automatically assign a returnType. If the object's value is a string it will automatically give a the string with a String type.
        - If accessing an array inside the object you'll just simple do **`<name>:<number>`** to iterate in the array.
        - In the selected array you can do **`.Count`** to get the length of the array or you can get the whole list in a string by using **`.data`**.

    - **Selector Cons**
        - Using [Selector](#usage- "kasuNhentaiCS.Json.JsonDeserializer.selector") might be time consuming to add the `selector()` everytime you want to pull a value in the object.
        - At the [Benchmark](#benchmarks-) there's a **`(mean) 0.046 ms`** tiny difference on speed compared to the default deserializer.
        - It needs to be Instantiated before using.

- **DESERIALIZEOBJ**
    - **DeserializeObj Pros**
        - You can type the name directly.
        - You can use those `[0]` in array because we love it.
        - only **`(mean) 0.046 ms`** faster than the [Selector](#usage- "kasuNhentaiCS.Json.JsonDeserializer.selector")
        - No need to Instantiate.

    - **DeserializeObj Cons**
        - Always return a **`JsonElement`** Type instead of the value. It still gives the value on the selected name but the type is different that you can't just assign it to the string type `string objStr = _default._String` because it will cause an Error.<br/>
        Example:![](https://user-images.githubusercontent.com/80595346/141485987-87746bba-0910-4867-92dd-bb9e86e3bd30.png) </br>
        > Don't ask me why I have a "console.log()" in a C#.
        - Unlike the [Selector](#usage- "kasuNhentaiCS.Json.JsonDeserializer.selector") arrays, there's 

### TL;DR
- [Selector](#usage- "kasuNhentaiCS.Json.JsonDeserializer.selector") - it'll automatically gives you the value with returnType.
- [DeserializeObj](#usage- "kasuNhentaiCS.Json.JsonDeserializer.deserializeObj") - it'll still give you the value but always returns with a `JsonElement` type.
---
<center>

## _**Benchmarks :**_
| Method                    | Mean      | Error     | StdDev    | Median    |
|:--------------------------|----------:|----------:|----------:|----------:|
|default Deserializer       |  2.831 ms | 0.0780 ms | 0.2301 ms |  2.930 ms |
|selector ForEach Static    |  2.877 ms | 0.0794 ms | 0.2342 ms |  2.991 ms |
|selector ForLoop Static    |  2.887 ms | 0.0798 ms | 0.2341 ms |  2.990 ms |
|selector ForLoop Instance  | 12.821 ms | 0.1098 ms | 0.1027 ms | 12.805 ms |
|selector ForEach Instance  | 12.921 ms | 0.1785 ms | 0.1670 ms | 12.898 ms |
>BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000 <br/>
>Intel Core2 Quad CPU Q6600 2.40GHz, 1 CPU, 4 logical and 4 physical cores<br/>
>.NET SDK=6.0.100<br/>
>  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT<br/>
>  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
</center>

_NOTE:_ If you're thinking `"wHy There's no ForLoop/Foreach default deserializer"` It is because its in the system or .NET framework which I cant modify, It is already built-in to the .NET. If you're still thinking `"what's the Instance and Static, and why can't we use the static?"` It's a regex.
