# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/1/2022 5:22:55 AM_
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
|TotalBytesAllocated |           bytes |    6,265,392.00 |    6,265,336.00 |    6,265,224.00 |           96.99 |
|TotalCollections [Gen0] |     collections |           64.00 |           64.00 |           64.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          800.00 |          791.67 |          787.00 |            7.23 |
|[Counter] WordsChecked |      operations |      787,360.00 |      787,360.00 |      787,360.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,958,078.47 |    7,911,903.07 |    7,834,139.21 |       67,739.27 |
|TotalCollections [Gen0] |     collections |           81.29 |           80.82 |           80.02 |            0.69 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.31 |          999.67 |          999.08 |            0.61 |
|[Counter] WordsChecked |      operations |    1,000,076.72 |      994,282.86 |      984,501.50 |        8,518.92 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,265,392.00 |    7,834,139.21 |          127.65 |
|               2 |    6,265,392.00 |    7,958,078.47 |          125.66 |
|               3 |    6,265,224.00 |    7,943,491.53 |          125.89 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           64.00 |           80.02 |   12,496,171.88 |
|               2 |           64.00 |           81.29 |   12,301,556.25 |
|               3 |           64.00 |           81.14 |   12,323,815.62 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  799,755,000.00 |
|               2 |            0.00 |            0.00 |  787,299,600.00 |
|               3 |            0.00 |            0.00 |  788,724,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  799,755,000.00 |
|               2 |            0.00 |            0.00 |  787,299,600.00 |
|               3 |            0.00 |            0.00 |  788,724,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          800.00 |        1,000.31 |      999,693.75 |
|               2 |          787.00 |          999.62 |    1,000,380.69 |
|               3 |          788.00 |          999.08 |    1,000,919.04 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      787,360.00 |      984,501.50 |        1,015.74 |
|               2 |      787,360.00 |    1,000,076.72 |          999.92 |
|               3 |      787,360.00 |      998,270.37 |        1,001.73 |


