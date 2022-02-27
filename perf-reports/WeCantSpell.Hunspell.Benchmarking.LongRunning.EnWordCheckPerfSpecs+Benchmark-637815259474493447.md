# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_2/27/2022 2:32:27 AM_
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
|TotalBytesAllocated |           bytes |    3,594,920.00 |    3,594,757.33 |    3,594,608.00 |          156.43 |
|TotalCollections [Gen0] |     collections |           65.00 |           65.00 |           65.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          786.00 |          785.33 |          785.00 |            0.58 |
|[Counter] WordsChecked |      operations |      795,648.00 |      795,648.00 |      795,648.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,583,768.03 |    4,579,717.24 |    4,573,407.46 |        5,537.38 |
|TotalCollections [Gen0] |     collections |           82.88 |           82.81 |           82.70 |            0.10 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.93 |        1,000.51 |          999.99 |            0.48 |
|[Counter] WordsChecked |      operations |    1,014,505.43 |    1,013,654.75 |    1,012,261.93 |        1,216.04 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,594,744.00 |    4,573,407.46 |          218.66 |
|               2 |    3,594,608.00 |    4,581,976.22 |          218.25 |
|               3 |    3,594,920.00 |    4,583,768.03 |          218.16 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           65.00 |           82.70 |   12,092,461.54 |
|               2 |           65.00 |           82.85 |   12,069,390.77 |
|               3 |           65.00 |           82.88 |   12,065,720.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  786,010,000.00 |
|               2 |            0.00 |            0.00 |  784,510,400.00 |
|               3 |            0.00 |            0.00 |  784,271,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  786,010,000.00 |
|               2 |            0.00 |            0.00 |  784,510,400.00 |
|               3 |            0.00 |            0.00 |  784,271,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          786.00 |          999.99 |    1,000,012.72 |
|               2 |          785.00 |        1,000.62 |      999,376.31 |
|               3 |          785.00 |        1,000.93 |      999,072.36 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      795,648.00 |    1,012,261.93 |          987.89 |
|               2 |      795,648.00 |    1,014,196.88 |          986.00 |
|               3 |      795,648.00 |    1,014,505.43 |          985.70 |


