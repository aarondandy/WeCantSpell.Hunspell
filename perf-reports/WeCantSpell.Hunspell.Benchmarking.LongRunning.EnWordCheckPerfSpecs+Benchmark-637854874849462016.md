# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_4/13/2022 10:58:04 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.4,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    2,859,816.00 |    2,859,773.33 |    2,859,752.00 |           36.95 |
|TotalCollections [Gen0] |     collections |           15.00 |           15.00 |           15.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,099.00 |        1,067.00 |        1,009.00 |           50.32 |
|[Counter] WordsChecked |      operations |      861,952.00 |      861,952.00 |      861,952.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,833,803.71 |    2,683,943.42 |    2,602,323.88 |      129,955.10 |
|TotalCollections [Gen0] |     collections |           14.86 |           14.08 |           13.65 |            0.68 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.07 |          999.87 |          999.70 |            0.19 |
|[Counter] WordsChecked |      operations |      854,130.98 |      808,956.01 |      784,361.12 |       39,174.13 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,859,752.00 |    2,833,803.71 |          352.88 |
|               2 |    2,859,816.00 |    2,615,702.67 |          382.31 |
|               3 |    2,859,752.00 |    2,602,323.88 |          384.27 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           15.00 |           14.86 |   67,277,113.33 |
|               2 |           15.00 |           13.72 |   72,888,406.67 |
|               3 |           15.00 |           13.65 |   73,261,493.33 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,156,700.00 |
|               2 |            0.00 |            0.00 |1,093,326,100.00 |
|               3 |            0.00 |            0.00 |1,098,922,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,156,700.00 |
|               2 |            0.00 |            0.00 |1,093,326,100.00 |
|               3 |            0.00 |            0.00 |1,098,922,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,009.00 |          999.84 |    1,000,155.30 |
|               2 |        1,093.00 |          999.70 |    1,000,298.35 |
|               3 |        1,099.00 |        1,000.07 |      999,929.39 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      861,952.00 |      854,130.98 |        1,170.78 |
|               2 |      861,952.00 |      788,375.95 |        1,268.43 |
|               3 |      861,952.00 |      784,361.12 |        1,274.92 |


