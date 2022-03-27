# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/27/2022 1:01:30 AM_
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
|TotalBytesAllocated |           bytes |    2,550,672.00 |    2,550,533.33 |    2,550,464.00 |          120.09 |
|TotalCollections [Gen0] |     collections |           23.00 |           23.00 |           23.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          639.00 |          637.00 |          634.00 |            2.65 |
|[Counter] WordsChecked |      operations |      696,192.00 |      696,192.00 |      696,192.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,023,843.27 |    4,003,910.89 |    3,992,377.98 |       17,332.90 |
|TotalCollections [Gen0] |     collections |           36.28 |           36.11 |           36.00 |            0.15 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.26 |          999.97 |          999.48 |            0.43 |
|[Counter] WordsChecked |      operations |    1,098,286.06 |    1,092,904.89 |    1,089,786.65 |        4,679.81 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,550,464.00 |    3,992,377.98 |          250.48 |
|               2 |    2,550,464.00 |    3,995,511.43 |          250.28 |
|               3 |    2,550,672.00 |    4,023,843.27 |          248.52 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           23.00 |           36.00 |   27,775,360.87 |
|               2 |           23.00 |           36.03 |   27,753,578.26 |
|               3 |           23.00 |           36.28 |   27,560,413.04 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  638,833,300.00 |
|               2 |            0.00 |            0.00 |  638,332,300.00 |
|               3 |            0.00 |            0.00 |  633,889,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  638,833,300.00 |
|               2 |            0.00 |            0.00 |  638,332,300.00 |
|               3 |            0.00 |            0.00 |  633,889,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          639.00 |        1,000.26 |      999,739.12 |
|               2 |          638.00 |          999.48 |    1,000,520.85 |
|               3 |          634.00 |        1,000.17 |      999,825.71 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      696,192.00 |    1,089,786.65 |          917.61 |
|               2 |      696,192.00 |    1,090,641.97 |          916.89 |
|               3 |      696,192.00 |    1,098,286.06 |          910.51 |


