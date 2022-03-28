# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/28/2022 10:48:40 PM_
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
|TotalBytesAllocated |           bytes |    4,877,200.00 |    4,876,968.00 |    4,876,712.00 |          244.88 |
|TotalCollections [Gen0] |     collections |           23.00 |           23.00 |           23.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,031.00 |          770.33 |          638.00 |          225.75 |
|[Counter] WordsChecked |      operations |      704,480.00 |      704,480.00 |      704,480.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,646,785.36 |    6,656,939.83 |    4,730,685.47 |    1,668,399.16 |
|TotalCollections [Gen0] |     collections |           36.06 |           31.39 |           22.31 |            7.87 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.34 |        1,000.00 |          999.63 |            0.35 |
|[Counter] WordsChecked |      operations |    1,104,575.80 |      961,604.20 |      683,316.92 |      241,034.25 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,877,200.00 |    4,730,685.47 |          211.39 |
|               2 |    4,876,712.00 |    7,593,348.66 |          131.69 |
|               3 |    4,876,992.00 |    7,646,785.36 |          130.77 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           23.00 |           22.31 |   44,824,830.43 |
|               2 |           23.00 |           35.81 |   27,923,247.83 |
|               3 |           23.00 |           36.06 |   27,729,708.70 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,030,971,100.00 |
|               2 |            0.00 |            0.00 |  642,234,700.00 |
|               3 |            0.00 |            0.00 |  637,783,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,030,971,100.00 |
|               2 |            0.00 |            0.00 |  642,234,700.00 |
|               3 |            0.00 |            0.00 |  637,783,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,031.00 |        1,000.03 |      999,971.97 |
|               2 |          642.00 |          999.63 |    1,000,365.58 |
|               3 |          638.00 |        1,000.34 |      999,660.34 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      704,480.00 |      683,316.92 |        1,463.45 |
|               2 |      704,480.00 |    1,096,919.86 |          911.64 |
|               3 |      704,480.00 |    1,104,575.80 |          905.32 |


