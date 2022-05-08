# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_5/8/2022 10:35:11 PM_
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
|TotalBytesAllocated |           bytes |    4,997,728.00 |    4,997,728.00 |    4,997,728.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           10.00 |           10.00 |           10.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] WordsChecked |      operations |      977,984.00 |      977,984.00 |      977,984.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,359,113.79 |    5,353,381.38 |    4,733,566.13 |      878,806.20 |
|TotalCollections [Gen0] |     collections |           12.72 |           10.71 |            9.47 |            1.76 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] WordsChecked |      operations |    1,244,387.76 |    1,047,580.29 |      926,291.30 |      171,969.82 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,997,728.00 |    4,967,464.22 |          201.31 |
|               2 |    4,997,728.00 |    4,733,566.13 |          211.26 |
|               3 |    4,997,728.00 |    6,359,113.79 |          157.25 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           10.00 |            9.94 |  100,609,240.00 |
|               2 |           10.00 |            9.47 |  105,580,610.00 |
|               3 |           10.00 |           12.72 |   78,591,580.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,092,400.00 |
|               2 |            0.00 |            0.00 |1,055,806,100.00 |
|               3 |            0.00 |            0.00 |  785,915,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,092,400.00 |
|               2 |            0.00 |            0.00 |1,055,806,100.00 |
|               3 |            0.00 |            0.00 |  785,915,800.00 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      977,984.00 |      972,061.81 |        1,028.74 |
|               2 |      977,984.00 |      926,291.30 |        1,079.57 |
|               3 |      977,984.00 |    1,244,387.76 |          803.61 |


