# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/17/2022 12:53:02 AM_
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
|TotalBytesAllocated |           bytes |    4,674,680.00 |    4,674,338.67 |    4,674,016.00 |          332.39 |
|TotalCollections [Gen0] |     collections |           42.00 |           42.00 |           42.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,063.00 |          909.67 |          646.00 |          229.35 |
|[Counter] WordsChecked |      operations |      621,600.00 |      621,600.00 |      621,600.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,233,033.65 |    5,403,880.14 |    4,396,588.31 |    1,586,804.34 |
|TotalCollections [Gen0] |     collections |           64.99 |           48.56 |           39.50 |           14.26 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.86 |          999.77 |          999.68 |            0.09 |
|[Counter] WordsChecked |      operations |      961,925.19 |      718,624.15 |      584,621.68 |      211,066.62 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,674,320.00 |    4,582,018.45 |          218.24 |
|               2 |    4,674,680.00 |    4,396,588.31 |          227.45 |
|               3 |    4,674,016.00 |    7,233,033.65 |          138.25 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           42.00 |           41.17 |   24,289,150.00 |
|               2 |           42.00 |           39.50 |   25,315,516.67 |
|               3 |           42.00 |           64.99 |   15,385,811.90 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,020,144,300.00 |
|               2 |            0.00 |            0.00 |1,063,251,700.00 |
|               3 |            0.00 |            0.00 |  646,204,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,020,144,300.00 |
|               2 |            0.00 |            0.00 |1,063,251,700.00 |
|               3 |            0.00 |            0.00 |  646,204,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,020.00 |          999.86 |    1,000,141.47 |
|               2 |        1,063.00 |          999.76 |    1,000,236.78 |
|               3 |          646.00 |          999.68 |    1,000,315.94 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      621,600.00 |      609,325.56 |        1,641.16 |
|               2 |      621,600.00 |      584,621.68 |        1,710.51 |
|               3 |      621,600.00 |      961,925.19 |        1,039.58 |


