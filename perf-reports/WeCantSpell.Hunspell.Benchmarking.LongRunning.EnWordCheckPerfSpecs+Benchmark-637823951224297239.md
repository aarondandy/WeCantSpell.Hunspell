# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/9/2022 3:58:42 AM_
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
|TotalBytesAllocated |           bytes |    1,481,352.00 |    1,481,352.00 |    1,481,352.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           65.00 |           65.00 |           65.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          785.00 |          779.67 |          776.00 |            4.73 |
|[Counter] WordsChecked |      operations |      779,072.00 |      779,072.00 |      779,072.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,909,176.25 |    1,900,329.02 |    1,886,776.02 |       11,918.51 |
|TotalCollections [Gen0] |     collections |           83.77 |           83.38 |           82.79 |            0.52 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.52 |        1,000.16 |          999.84 |            0.34 |
|[Counter] WordsChecked |      operations |    1,004,073.14 |      999,420.21 |      992,292.42 |        6,268.18 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,481,352.00 |    1,886,776.02 |          530.00 |
|               2 |    1,481,352.00 |    1,909,176.25 |          523.79 |
|               3 |    1,481,352.00 |    1,905,034.78 |          524.92 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           65.00 |           82.79 |   12,078,821.54 |
|               2 |           65.00 |           83.77 |   11,937,101.54 |
|               3 |           65.00 |           83.59 |   11,963,052.31 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  785,123,400.00 |
|               2 |            0.00 |            0.00 |  775,911,600.00 |
|               3 |            0.00 |            0.00 |  777,598,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  785,123,400.00 |
|               2 |            0.00 |            0.00 |  775,911,600.00 |
|               3 |            0.00 |            0.00 |  777,598,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          785.00 |          999.84 |    1,000,157.20 |
|               2 |          776.00 |        1,000.11 |      999,886.08 |
|               3 |          778.00 |        1,000.52 |      999,483.80 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      779,072.00 |      992,292.42 |        1,007.77 |
|               2 |      779,072.00 |    1,004,073.14 |          995.94 |
|               3 |      779,072.00 |    1,001,895.07 |          998.11 |


