# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_11/20/2023 04:03:44_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 6.2.9200.0
ProcessorCount=16
CLR=4.0.30319.42000,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    1,090,568.00 |    1,090,568.00 |    1,090,568.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            8.00 |            8.00 |            8.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] WordsChecked |      operations |      298,368.00 |      298,368.00 |      298,368.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,613,422.36 |    1,401,960.11 |      979,214.12 |      366,108.78 |
|TotalCollections [Gen0] |     collections |           11.84 |           10.28 |            7.18 |            2.69 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] WordsChecked |      operations |      441,415.49 |      383,561.62 |      267,902.74 |      100,163.53 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,090,568.00 |      979,214.12 |        1,021.23 |
|               2 |    1,090,568.00 |    1,613,243.84 |          619.87 |
|               3 |    1,090,568.00 |    1,613,422.36 |          619.80 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            8.00 |            7.18 |  139,214,700.00 |
|               2 |            8.00 |           11.83 |   84,501,175.00 |
|               3 |            8.00 |           11.84 |   84,491,825.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,113,717,600.00 |
|               2 |            0.00 |            0.00 |  676,009,400.00 |
|               3 |            0.00 |            0.00 |  675,934,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,113,717,600.00 |
|               2 |            0.00 |            0.00 |  676,009,400.00 |
|               3 |            0.00 |            0.00 |  675,934,600.00 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      298,368.00 |      267,902.74 |        3,732.70 |
|               2 |      298,368.00 |      441,366.64 |        2,265.69 |
|               3 |      298,368.00 |      441,415.49 |        2,265.44 |


