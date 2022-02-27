# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_02/27/2022 23:02:33_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 6.2.9200.0
ProcessorCount=16
CLR=4.0.30319.42000,IsMono=False,MaxGcGeneration=2
```

### NBench Settings
```ini
RunMode=Throughput, TestMode=Measurement
NumberOfIterations=2, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |  126,517,816.00 |   78,777,168.00 |   31,036,520.00 |   67,515,471.88 |
|TotalCollections [Gen0] |     collections |        1,218.00 |        1,218.00 |        1,218.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          402.00 |          401.50 |          401.00 |            0.71 |
|TotalCollections [Gen2] |     collections |          115.00 |          115.00 |          115.00 |            0.00 |
|    Elapsed Time |              ms |       21,710.00 |       21,556.50 |       21,403.00 |          217.08 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,911,232.67 |    3,670,430.69 |    1,429,628.72 |    3,168,972.54 |
|TotalCollections [Gen0] |     collections |           56.91 |           56.51 |           56.10 |            0.57 |
|TotalCollections [Gen1] |     collections |           18.74 |           18.63 |           18.52 |            0.15 |
|TotalCollections [Gen2] |     collections |            5.37 |            5.34 |            5.30 |            0.05 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.01 |        1,000.00 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            2.76 |            2.74 |            2.72 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  126,517,816.00 |    5,911,232.67 |          169.17 |
|               2 |   31,036,520.00 |    1,429,628.72 |          699.48 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,218.00 |           56.91 |   17,572,208.37 |
|               2 |        1,218.00 |           56.10 |   17,823,888.92 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          401.00 |           18.74 |   53,373,939.65 |
|               2 |          402.00 |           18.52 |   54,003,723.13 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          115.00 |            5.37 |  186,112,606.96 |
|               2 |          115.00 |            5.30 |  188,778,232.17 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       21,403.00 |        1,000.00 |      999,997.65 |
|               2 |       21,710.00 |        1,000.02 |      999,976.82 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            2.76 |  362,761,861.02 |
|               2 |           59.00 |            2.72 |  367,957,571.19 |


