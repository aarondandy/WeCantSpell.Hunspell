# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_2/25/2022 3:52:57 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.2,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    1,466,008.00 |    1,465,773.33 |    1,465,656.00 |          203.23 |
|TotalCollections [Gen0] |     collections |           70.00 |           70.00 |           70.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          908.00 |          865.33 |          840.00 |           37.17 |
|[Counter] WordsChecked |      operations |      870,240.00 |      870,240.00 |      870,240.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,745,317.02 |    1,695,854.25 |    1,613,325.28 |       71,940.84 |
|TotalCollections [Gen0] |     collections |           83.34 |           80.99 |           77.05 |            3.43 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.32 |          999.95 |          999.48 |            0.43 |
|[Counter] WordsChecked |      operations |    1,036,041.19 |    1,006,838.33 |      957,919.31 |       42,629.83 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,466,008.00 |    1,745,317.02 |          572.96 |
|               2 |    1,465,656.00 |    1,728,920.45 |          578.40 |
|               3 |    1,465,656.00 |    1,613,325.28 |          619.84 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           70.00 |           83.34 |   11,999,522.86 |
|               2 |           70.00 |           82.57 |   12,110,414.29 |
|               3 |           70.00 |           77.05 |   12,978,128.57 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  839,966,600.00 |
|               2 |            0.00 |            0.00 |  847,729,000.00 |
|               3 |            0.00 |            0.00 |  908,469,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  839,966,600.00 |
|               2 |            0.00 |            0.00 |  847,729,000.00 |
|               3 |            0.00 |            0.00 |  908,469,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          840.00 |        1,000.04 |      999,960.24 |
|               2 |          848.00 |        1,000.32 |      999,680.42 |
|               3 |          908.00 |          999.48 |    1,000,516.52 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      870,240.00 |    1,036,041.19 |          965.21 |
|               2 |      870,240.00 |    1,026,554.48 |          974.13 |
|               3 |      870,240.00 |      957,919.31 |        1,043.93 |


