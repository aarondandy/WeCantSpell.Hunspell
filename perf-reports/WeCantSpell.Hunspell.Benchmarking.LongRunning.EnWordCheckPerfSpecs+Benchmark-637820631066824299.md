# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/5/2022 7:45:06 AM_
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
|TotalBytesAllocated |           bytes |    5,888,064.00 |    5,887,954.67 |    5,887,744.00 |          182.49 |
|TotalCollections [Gen0] |     collections |           62.00 |           62.00 |           62.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,039.00 |          855.67 |          746.00 |          159.79 |
|[Counter] WordsChecked |      operations |      762,496.00 |      762,496.00 |      762,496.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,888,478.22 |    7,029,085.96 |    5,666,806.86 |    1,193,158.54 |
|TotalCollections [Gen0] |     collections |           83.07 |           74.02 |           59.67 |           12.57 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.33 |          999.93 |          999.50 |            0.42 |
|[Counter] WordsChecked |      operations |    1,021,602.35 |      910,275.58 |      733,844.51 |      154,531.78 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,888,056.00 |    5,666,806.86 |          176.47 |
|               2 |    5,887,744.00 |    7,888,478.22 |          126.77 |
|               3 |    5,888,064.00 |    7,531,972.80 |          132.77 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           62.00 |           59.67 |   16,758,758.06 |
|               2 |           62.00 |           83.07 |   12,038,267.74 |
|               3 |           62.00 |           79.31 |   12,608,751.61 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,039,043,000.00 |
|               2 |            0.00 |            0.00 |  746,372,600.00 |
|               3 |            0.00 |            0.00 |  781,742,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,039,043,000.00 |
|               2 |            0.00 |            0.00 |  746,372,600.00 |
|               3 |            0.00 |            0.00 |  781,742,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,039.00 |          999.96 |    1,000,041.39 |
|               2 |          746.00 |          999.50 |    1,000,499.46 |
|               3 |          782.00 |        1,000.33 |      999,670.84 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      762,496.00 |      733,844.51 |        1,362.69 |
|               2 |      762,496.00 |    1,021,602.35 |          978.85 |
|               3 |      762,496.00 |      975,379.88 |        1,025.24 |


