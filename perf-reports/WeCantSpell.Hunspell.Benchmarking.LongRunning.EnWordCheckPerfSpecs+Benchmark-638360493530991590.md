# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_11/20/2023 3:55:53 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.22631.0
ProcessorCount=16
CLR=6.0.25,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    7,898,272.00 |    7,898,272.00 |    7,898,272.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            9.00 |            9.00 |            9.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] WordsChecked |      operations |      621,600.00 |      621,600.00 |      621,600.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   10,261,820.73 |   10,145,860.45 |    9,956,877.29 |      165,066.29 |
|TotalCollections [Gen0] |     collections |           11.69 |           11.56 |           11.35 |            0.19 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] WordsChecked |      operations |      807,613.08 |      798,486.92 |      783,613.80 |       12,990.84 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,898,272.00 |   10,261,820.73 |           97.45 |
|               2 |    7,898,272.00 |   10,218,883.32 |           97.86 |
|               3 |    7,898,272.00 |    9,956,877.29 |          100.43 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            9.00 |           11.69 |   85,519,500.00 |
|               2 |            9.00 |           11.64 |   85,878,833.33 |
|               3 |            9.00 |           11.35 |   88,138,655.56 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  769,675,500.00 |
|               2 |            0.00 |            0.00 |  772,909,500.00 |
|               3 |            0.00 |            0.00 |  793,247,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  769,675,500.00 |
|               2 |            0.00 |            0.00 |  772,909,500.00 |
|               3 |            0.00 |            0.00 |  793,247,900.00 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      621,600.00 |      807,613.08 |        1,238.22 |
|               2 |      621,600.00 |      804,233.87 |        1,243.42 |
|               3 |      621,600.00 |      783,613.80 |        1,276.14 |


