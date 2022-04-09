# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_4/9/2022 2:16:08 PM_
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
|TotalBytesAllocated |           bytes |    2,627,648.00 |    2,627,552.00 |    2,627,504.00 |           83.14 |
|TotalCollections [Gen0] |     collections |           46.00 |           46.00 |           46.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          662.00 |          656.67 |          653.00 |            4.73 |
|[Counter] WordsChecked |      operations |      646,464.00 |      646,464.00 |      646,464.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,020,284.54 |    4,000,298.91 |    3,968,581.78 |       27,776.16 |
|TotalCollections [Gen0] |     collections |           70.38 |           70.03 |           69.47 |            0.49 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.14 |          999.70 |          999.14 |            0.51 |
|[Counter] WordsChecked |      operations |      989,139.97 |      984,204.92 |      976,365.65 |        6,864.52 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,627,504.00 |    4,012,030.42 |          249.25 |
|               2 |    2,627,504.00 |    4,020,284.54 |          248.74 |
|               3 |    2,627,648.00 |    3,968,581.78 |          251.98 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           46.00 |           70.24 |   14,237,093.48 |
|               2 |           46.00 |           70.38 |   14,207,863.04 |
|               3 |           46.00 |           69.47 |   14,393,752.17 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  654,906,300.00 |
|               2 |            0.00 |            0.00 |  653,561,700.00 |
|               3 |            0.00 |            0.00 |  662,112,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  654,906,300.00 |
|               2 |            0.00 |            0.00 |  653,561,700.00 |
|               3 |            0.00 |            0.00 |  662,112,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          655.00 |        1,000.14 |      999,856.95 |
|               2 |          653.00 |          999.14 |    1,000,860.18 |
|               3 |          662.00 |          999.83 |    1,000,170.09 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      646,464.00 |      987,109.15 |        1,013.06 |
|               2 |      646,464.00 |      989,139.97 |        1,010.98 |
|               3 |      646,464.00 |      976,365.65 |        1,024.21 |


