# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_04/17/2022 17:06:19_
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
|TotalBytesAllocated |           bytes |  120,543,912.00 |  120,007,660.00 |  119,471,408.00 |      758,374.85 |
|TotalCollections [Gen0] |     collections |          485.00 |          484.50 |          484.00 |            0.71 |
|TotalCollections [Gen1] |     collections |          189.00 |          188.50 |          188.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           46.00 |           45.50 |           45.00 |            0.71 |
|    Elapsed Time |              ms |       17,720.00 |       17,683.50 |       17,647.00 |           51.62 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,802,745.87 |    6,786,470.40 |    6,770,194.93 |       23,016.99 |
|TotalCollections [Gen0] |     collections |           27.43 |           27.40 |           27.37 |            0.04 |
|TotalCollections [Gen1] |     collections |           10.67 |           10.66 |           10.65 |            0.01 |
|TotalCollections [Gen2] |     collections |            2.60 |            2.57 |            2.55 |            0.03 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.01 |        1,000.01 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.34 |            3.34 |            3.33 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  120,543,912.00 |    6,802,745.87 |          147.00 |
|               2 |  119,471,408.00 |    6,770,194.93 |          147.71 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          485.00 |           27.37 |   36,535,856.70 |
|               2 |          484.00 |           27.43 |   36,460,065.70 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          189.00 |           10.67 |   93,756,034.39 |
|               2 |          188.00 |           10.65 |   93,865,275.53 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           46.00 |            2.60 |  385,215,010.87 |
|               2 |           45.00 |            2.55 |  392,148,262.22 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       17,720.00 |        1,000.01 |      999,993.82 |
|               2 |       17,647.00 |        1,000.02 |      999,981.40 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.33 |  300,337,127.12 |
|               2 |           59.00 |            3.34 |  299,096,132.20 |


