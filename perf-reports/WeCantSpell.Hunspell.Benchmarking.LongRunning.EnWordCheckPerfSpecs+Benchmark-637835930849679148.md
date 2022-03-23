# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/23/2022 12:44:44 AM_
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
|TotalBytesAllocated |           bytes |    6,035,592.00 |    6,035,378.67 |    6,035,232.00 |          189.03 |
|TotalCollections [Gen0] |     collections |           45.00 |           45.00 |           45.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,046.00 |          895.00 |          631.00 |          229.42 |
|[Counter] WordsChecked |      operations |      638,176.00 |      638,176.00 |      638,176.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,567,844.91 |    7,108,033.67 |    5,768,049.98 |    2,133,101.19 |
|TotalCollections [Gen0] |     collections |           71.34 |           53.00 |           43.01 |           15.91 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.34 |        1,000.04 |          999.68 |            0.33 |
|[Counter] WordsChecked |      operations |    1,011,720.67 |      751,600.64 |      609,915.62 |      225,570.31 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,035,592.00 |    5,988,206.13 |          166.99 |
|               2 |    6,035,312.00 |    5,768,049.98 |          173.37 |
|               3 |    6,035,232.00 |    9,567,844.91 |          104.52 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           45.00 |           44.65 |   22,398,071.11 |
|               2 |           45.00 |           43.01 |   23,251,886.67 |
|               3 |           45.00 |           71.34 |   14,017,395.56 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,913,200.00 |
|               2 |            0.00 |            0.00 |1,046,334,900.00 |
|               3 |            0.00 |            0.00 |  630,782,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,913,200.00 |
|               2 |            0.00 |            0.00 |1,046,334,900.00 |
|               3 |            0.00 |            0.00 |  630,782,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,008.00 |        1,000.09 |      999,913.89 |
|               2 |        1,046.00 |          999.68 |    1,000,320.17 |
|               3 |          631.00 |        1,000.34 |      999,655.78 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      638,176.00 |      633,165.63 |        1,579.37 |
|               2 |      638,176.00 |      609,915.62 |        1,639.57 |
|               3 |      638,176.00 |    1,011,720.67 |          988.42 |


