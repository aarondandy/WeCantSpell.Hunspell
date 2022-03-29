# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/29/2022 4:03:57 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.3,IsMono=False,MaxGcGeneration=2
```

### NBench Settings
```ini
RunMode=Throughput, TestMode=Measurement
NumberOfIterations=3, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,694,976.00 |    7,694,976.00 |    7,694,976.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           21.00 |           21.00 |           21.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          615.00 |          613.67 |          613.00 |            1.15 |
|[Counter] WordsChecked |      operations |      654,752.00 |      654,752.00 |      654,752.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   12,568,269.85 |   12,536,974.17 |   12,507,893.24 |       30,249.17 |
|TotalCollections [Gen0] |     collections |           34.30 |           34.21 |           34.13 |            0.08 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,001.22 |          999.81 |          998.55 |            1.34 |
|[Counter] WordsChecked |      operations |    1,069,412.02 |    1,066,749.12 |    1,064,274.68 |        2,573.85 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,694,976.00 |   12,534,759.43 |           79.78 |
|               2 |    7,694,976.00 |   12,507,893.24 |           79.95 |
|               3 |    7,694,976.00 |   12,568,269.85 |           79.57 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           21.00 |           34.21 |   29,232,904.76 |
|               2 |           21.00 |           34.13 |   29,295,695.24 |
|               3 |           21.00 |           34.30 |   29,154,961.90 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  613,891,000.00 |
|               2 |            0.00 |            0.00 |  615,209,600.00 |
|               3 |            0.00 |            0.00 |  612,254,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  613,891,000.00 |
|               2 |            0.00 |            0.00 |  615,209,600.00 |
|               3 |            0.00 |            0.00 |  612,254,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          613.00 |          998.55 |    1,001,453.51 |
|               2 |          615.00 |          999.66 |    1,000,340.81 |
|               3 |          613.00 |        1,001.22 |      998,783.36 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      654,752.00 |    1,066,560.68 |          937.59 |
|               2 |      654,752.00 |    1,064,274.68 |          939.61 |
|               3 |      654,752.00 |    1,069,412.02 |          935.09 |


