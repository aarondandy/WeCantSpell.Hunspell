# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_4/9/2022 2:41:44 PM_
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
|TotalBytesAllocated |           bytes |    7,954,920.00 |    7,954,898.67 |    7,954,856.00 |           36.95 |
|TotalCollections [Gen0] |     collections |           11.00 |           11.00 |           11.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,034.00 |          881.00 |          602.00 |          242.00 |
|[Counter] WordsChecked |      operations |      671,328.00 |      671,328.00 |      671,328.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   13,203,378.53 |    9,599,156.42 |    7,694,401.42 |    3,123,035.15 |
|TotalCollections [Gen0] |     collections |           18.26 |           13.27 |           10.64 |            4.32 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.14 |          999.78 |          999.19 |            0.51 |
|[Counter] WordsChecked |      operations |    1,114,262.50 |      810,090.64 |      649,342.43 |      263,562.94 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,954,920.00 |    7,899,689.32 |          126.59 |
|               2 |    7,954,920.00 |    7,694,401.42 |          129.96 |
|               3 |    7,954,856.00 |   13,203,378.53 |           75.74 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           11.00 |           10.92 |   91,544,681.82 |
|               2 |           11.00 |           10.64 |   93,987,109.09 |
|               3 |           11.00 |           18.26 |   54,771,490.91 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,991,500.00 |
|               2 |            0.00 |            0.00 |1,033,858,200.00 |
|               3 |            0.00 |            0.00 |  602,486,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,991,500.00 |
|               2 |            0.00 |            0.00 |1,033,858,200.00 |
|               3 |            0.00 |            0.00 |  602,486,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,007.00 |        1,000.01 |      999,991.56 |
|               2 |        1,034.00 |        1,000.14 |      999,862.86 |
|               3 |          602.00 |          999.19 |    1,000,807.97 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      671,328.00 |      666,667.00 |        1,500.00 |
|               2 |      671,328.00 |      649,342.43 |        1,540.02 |
|               3 |      671,328.00 |    1,114,262.50 |          897.45 |


