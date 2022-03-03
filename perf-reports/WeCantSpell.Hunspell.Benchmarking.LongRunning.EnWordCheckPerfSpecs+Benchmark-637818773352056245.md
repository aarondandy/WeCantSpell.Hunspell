# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/3/2022 4:08:55 AM_
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
|TotalBytesAllocated |           bytes |    2,848,792.00 |    2,848,789.33 |    2,848,784.00 |            4.62 |
|TotalCollections [Gen0] |     collections |           61.00 |           61.00 |           61.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          741.00 |          737.67 |          735.00 |            3.06 |
|[Counter] WordsChecked |      operations |      745,920.00 |      745,920.00 |      745,920.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,875,731.55 |    3,860,880.19 |    3,841,500.19 |       17,559.27 |
|TotalCollections [Gen0] |     collections |           82.99 |           82.67 |           82.26 |            0.38 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.01 |          999.73 |          999.21 |            0.44 |
|[Counter] WordsChecked |      operations |    1,014,813.93 |    1,010,923.39 |    1,005,848.03 |        4,598.88 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,848,792.00 |    3,865,408.82 |          258.70 |
|               2 |    2,848,792.00 |    3,841,500.19 |          260.31 |
|               3 |    2,848,784.00 |    3,875,731.55 |          258.02 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           61.00 |           82.77 |   12,081,906.56 |
|               2 |           61.00 |           82.26 |   12,157,101.64 |
|               3 |           61.00 |           82.99 |   12,049,693.44 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  736,996,300.00 |
|               2 |            0.00 |            0.00 |  741,583,200.00 |
|               3 |            0.00 |            0.00 |  735,031,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  736,996,300.00 |
|               2 |            0.00 |            0.00 |  741,583,200.00 |
|               3 |            0.00 |            0.00 |  735,031,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          737.00 |        1,000.01 |      999,994.98 |
|               2 |          741.00 |          999.21 |    1,000,787.04 |
|               3 |          735.00 |          999.96 |    1,000,042.59 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      745,920.00 |    1,012,108.20 |          988.04 |
|               2 |      745,920.00 |    1,005,848.03 |          994.19 |
|               3 |      745,920.00 |    1,014,813.93 |          985.40 |


