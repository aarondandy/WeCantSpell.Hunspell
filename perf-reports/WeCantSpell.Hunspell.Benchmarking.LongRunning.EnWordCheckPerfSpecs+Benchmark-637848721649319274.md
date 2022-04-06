# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_4/6/2022 8:02:44 PM_
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
|TotalBytesAllocated |           bytes |    7,954,920.00 |    7,954,920.00 |    7,954,920.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           11.00 |           11.00 |           11.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          612.00 |          612.00 |          612.00 |            0.00 |
|[Counter] WordsChecked |      operations |      671,328.00 |      671,328.00 |      671,328.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   13,013,285.63 |   13,000,648.15 |   12,988,059.19 |       12,613.29 |
|TotalCollections [Gen0] |     collections |           17.99 |           17.98 |           17.96 |            0.02 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,001.16 |        1,000.19 |          999.22 |            0.97 |
|[Counter] WordsChecked |      operations |    1,098,211.30 |    1,097,144.80 |    1,096,082.40 |        1,064.45 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,954,920.00 |   13,000,599.62 |           76.92 |
|               2 |    7,954,920.00 |   12,988,059.19 |           76.99 |
|               3 |    7,954,920.00 |   13,013,285.63 |           76.84 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           11.00 |           17.98 |   55,626,245.45 |
|               2 |           11.00 |           17.96 |   55,679,954.55 |
|               3 |           11.00 |           17.99 |   55,572,018.18 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  611,888,700.00 |
|               2 |            0.00 |            0.00 |  612,479,500.00 |
|               3 |            0.00 |            0.00 |  611,292,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  611,888,700.00 |
|               2 |            0.00 |            0.00 |  612,479,500.00 |
|               3 |            0.00 |            0.00 |  611,292,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          612.00 |        1,000.18 |      999,818.14 |
|               2 |          612.00 |          999.22 |    1,000,783.50 |
|               3 |          612.00 |        1,001.16 |      998,843.46 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      671,328.00 |    1,097,140.71 |          911.46 |
|               2 |      671,328.00 |    1,096,082.40 |          912.34 |
|               3 |      671,328.00 |    1,098,211.30 |          910.57 |


