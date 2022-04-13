# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_4/13/2022 10:49:42 PM_
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
|TotalBytesAllocated |           bytes |    1,054,640.00 |    1,054,592.00 |    1,054,496.00 |           83.14 |
|TotalCollections [Gen0] |     collections |           45.00 |           45.00 |           45.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          661.00 |          659.33 |          658.00 |            1.53 |
|[Counter] WordsChecked |      operations |      629,888.00 |      629,888.00 |      629,888.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,602,295.04 |    1,599,452.75 |    1,595,225.58 |        3,732.66 |
|TotalCollections [Gen0] |     collections |           68.38 |           68.25 |           68.07 |            0.16 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.30 |          999.98 |          999.81 |            0.28 |
|[Counter] WordsChecked |      operations |      957,107.87 |      955,323.18 |      952,754.92 |        2,279.81 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,054,640.00 |    1,595,225.58 |          626.87 |
|               2 |    1,054,640.00 |    1,600,837.64 |          624.67 |
|               3 |    1,054,496.00 |    1,602,295.04 |          624.10 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           45.00 |           68.07 |   14,691,617.78 |
|               2 |           45.00 |           68.31 |   14,640,113.33 |
|               3 |           45.00 |           68.38 |   14,624,800.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  661,122,800.00 |
|               2 |            0.00 |            0.00 |  658,805,100.00 |
|               3 |            0.00 |            0.00 |  658,116,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  661,122,800.00 |
|               2 |            0.00 |            0.00 |  658,805,100.00 |
|               3 |            0.00 |            0.00 |  658,116,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          661.00 |          999.81 |    1,000,185.78 |
|               2 |          659.00 |        1,000.30 |      999,704.25 |
|               3 |          658.00 |          999.82 |    1,000,176.29 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      629,888.00 |      952,754.92 |        1,049.59 |
|               2 |      629,888.00 |      956,106.75 |        1,045.91 |
|               3 |      629,888.00 |      957,107.87 |        1,044.81 |


