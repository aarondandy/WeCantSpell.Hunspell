# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/6/2022 6:10:54 AM_
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
|TotalBytesAllocated |           bytes |      169,824.00 |      169,608.00 |      169,432.00 |          199.04 |
|TotalCollections [Gen0] |     collections |           62.00 |           62.00 |           62.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,024.00 |          911.33 |          740.00 |          150.82 |
|[Counter] WordsChecked |      operations |      754,208.00 |      754,208.00 |      754,208.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      229,322.35 |      189,817.34 |      165,474.98 |       34,518.85 |
|TotalCollections [Gen0] |     collections |           83.72 |           69.38 |           60.50 |           12.54 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.90 |          999.48 |          999.26 |            0.36 |
|[Counter] WordsChecked |      operations |    1,018,447.04 |      843,968.73 |      736,002.97 |      152,517.55 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      169,568.00 |      165,474.98 |        6,043.21 |
|               2 |      169,432.00 |      174,654.68 |        5,725.58 |
|               3 |      169,824.00 |      229,322.35 |        4,360.67 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           62.00 |           60.50 |   16,527,983.87 |
|               2 |           62.00 |           63.91 |   15,646,727.42 |
|               3 |           62.00 |           83.72 |   11,944,308.06 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,024,735,000.00 |
|               2 |            0.00 |            0.00 |  970,097,100.00 |
|               3 |            0.00 |            0.00 |  740,547,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,024,735,000.00 |
|               2 |            0.00 |            0.00 |  970,097,100.00 |
|               3 |            0.00 |            0.00 |  740,547,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,024.00 |          999.28 |    1,000,717.77 |
|               2 |          970.00 |          999.90 |    1,000,100.10 |
|               3 |          740.00 |          999.26 |    1,000,739.32 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      754,208.00 |      736,002.97 |        1,358.69 |
|               2 |      754,208.00 |      777,456.19 |        1,286.25 |
|               3 |      754,208.00 |    1,018,447.04 |          981.89 |


