``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
Intel Core2 Quad CPU Q6600 2.40GHz, 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT


```
|                     Method |      Mean |     Error |    StdDev |    Median |
|--------------------------- |----------:|----------:|----------:|----------:|
|      _default_Deserializer |  2.831 ms | 0.0780 ms | 0.2301 ms |  2.930 ms |
| _selector_ForLoop_Instance | 12.821 ms | 0.1098 ms | 0.1027 ms | 12.805 ms |
| _selector_ForEach_Instance | 12.921 ms | 0.1785 ms | 0.1670 ms | 12.898 ms |
|   _selector_ForLoop_Static |  2.887 ms | 0.0798 ms | 0.2341 ms |  2.990 ms |
|   _selector_ForEach_Static |  2.877 ms | 0.0794 ms | 0.2342 ms |  2.991 ms |
