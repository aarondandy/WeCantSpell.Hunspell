``` ini

BenchmarkDotNet=v0.10.8, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-6700K CPU 4.00GHz (Skylake), ProcessorCount=4
Frequency=3914062 Hz, Resolution=255.4891 ns, Timer=TSC
dotnet cli version=1.0.4
  [Host]     : .NET Core 4.6.25211.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.25211.01, 64bit RyuJIT


```
 |                         Method |      Mean |     Error |    StdDev | Scaled | ScaledSD |
 |------------------------------- |----------:|----------:|----------:|-------:|---------:|
 | 'Check an assortment of words' | 11.850 ms | 0.2351 ms | 0.2199 ms |   1.00 |     0.03 |
 |             'Check root words' | 11.865 ms | 0.2343 ms | 0.3285 ms |   1.00 |     0.00 |
 |          'Check correct words' |  1.139 ms | 0.0005 ms | 0.0004 ms |   0.10 |     0.00 |
 |            'Check wrong words' | 10.926 ms | 0.2183 ms | 0.3647 ms |   0.92 |     0.04 |
