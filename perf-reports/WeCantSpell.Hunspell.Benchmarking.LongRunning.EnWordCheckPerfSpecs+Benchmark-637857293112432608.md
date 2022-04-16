# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_4/16/2022 6:08:31 PM_
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
|TotalBytesAllocated |           bytes |    4,965,088.00 |    4,965,045.33 |    4,965,024.00 |           36.95 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          811.00 |          805.33 |          800.00 |            5.51 |
|[Counter] WordsChecked |      operations |      928,256.00 |      928,256.00 |      928,256.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,202,875.53 |    6,164,603.99 |    6,120,590.21 |       41,442.11 |
|TotalCollections [Gen0] |     collections |           16.24 |           16.14 |           16.03 |            0.11 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.42 |          999.87 |          999.44 |            0.50 |
|[Counter] WordsChecked |      operations |    1,159,668.56 |    1,152,523.31 |    1,144,299.52 |        7,741.08 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,965,024.00 |    6,170,346.22 |          162.07 |
|               2 |    4,965,088.00 |    6,202,875.53 |          161.22 |
|               3 |    4,965,024.00 |    6,120,590.21 |          163.38 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |           16.16 |   61,896,838.46 |
|               2 |           13.00 |           16.24 |   61,573,030.77 |
|               3 |           13.00 |           16.03 |   62,400,015.38 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  804,658,900.00 |
|               2 |            0.00 |            0.00 |  800,449,400.00 |
|               3 |            0.00 |            0.00 |  811,200,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  804,658,900.00 |
|               2 |            0.00 |            0.00 |  800,449,400.00 |
|               3 |            0.00 |            0.00 |  811,200,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          805.00 |        1,000.42 |      999,576.27 |
|               2 |          800.00 |          999.44 |    1,000,561.75 |
|               3 |          811.00 |          999.75 |    1,000,246.86 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      928,256.00 |    1,153,601.86 |          866.85 |
|               2 |      928,256.00 |    1,159,668.56 |          862.32 |
|               3 |      928,256.00 |    1,144,299.52 |          873.90 |


