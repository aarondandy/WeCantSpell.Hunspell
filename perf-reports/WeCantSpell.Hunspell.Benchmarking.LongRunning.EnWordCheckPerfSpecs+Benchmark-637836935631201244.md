# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/24/2022 4:39:23 AM_
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
|TotalBytesAllocated |           bytes |    2,084,336.00 |    2,084,336.00 |    2,084,336.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          663.00 |          646.00 |          630.00 |           16.52 |
|[Counter] WordsChecked |      operations |      679,616.00 |      679,616.00 |      679,616.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,303,357.50 |    3,227,587.09 |    3,146,128.61 |       78,768.63 |
|TotalCollections [Gen0] |     collections |           41.21 |           40.26 |           39.24 |            0.98 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.74 |          999.91 |          998.45 |            1.27 |
|[Counter] WordsChecked |      operations |    1,077,088.63 |    1,052,383.03 |    1,025,822.77 |       25,683.20 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,084,336.00 |    3,303,357.50 |          302.72 |
|               2 |    2,084,336.00 |    3,146,128.61 |          317.85 |
|               3 |    2,084,336.00 |    3,233,275.15 |          309.28 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           41.21 |   24,268,269.23 |
|               2 |           26.00 |           39.24 |   25,481,084.62 |
|               3 |           26.00 |           40.33 |   24,794,292.31 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  630,975,000.00 |
|               2 |            0.00 |            0.00 |  662,508,200.00 |
|               3 |            0.00 |            0.00 |  644,651,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  630,975,000.00 |
|               2 |            0.00 |            0.00 |  662,508,200.00 |
|               3 |            0.00 |            0.00 |  644,651,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          630.00 |          998.45 |    1,001,547.62 |
|               2 |          663.00 |        1,000.74 |      999,258.22 |
|               3 |          645.00 |        1,000.54 |      999,459.84 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      679,616.00 |    1,077,088.63 |          928.43 |
|               2 |      679,616.00 |    1,025,822.77 |          974.83 |
|               3 |      679,616.00 |    1,054,237.67 |          948.55 |


