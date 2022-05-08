# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_05/08/2022 21:00:22_
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
|TotalBytesAllocated |           bytes |   79,528,512.00 |   79,454,352.00 |   79,380,192.00 |      104,878.08 |
|TotalCollections [Gen0] |     collections |          360.00 |          355.50 |          351.00 |            6.36 |
|TotalCollections [Gen1] |     collections |          143.00 |          139.00 |          135.00 |            5.66 |
|TotalCollections [Gen2] |     collections |           40.00 |           36.00 |           32.00 |            5.66 |
|    Elapsed Time |              ms |       18,083.00 |       17,904.00 |       17,725.00 |          253.14 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,486,562.42 |    4,438,221.36 |    4,389,880.31 |       68,364.57 |
|TotalCollections [Gen0] |     collections |           19.91 |           19.86 |           19.80 |            0.08 |
|TotalCollections [Gen1] |     collections |            7.91 |            7.76 |            7.62 |            0.21 |
|TotalCollections [Gen2] |     collections |            2.21 |            2.01 |            1.81 |            0.29 |
|    Elapsed Time |              ms |        1,000.03 |          999.99 |          999.95 |            0.06 |
|[Counter] FilePairsLoaded |      operations |            3.33 |            3.30 |            3.26 |            0.05 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   79,528,512.00 |    4,486,562.42 |          222.89 |
|               2 |   79,380,192.00 |    4,389,880.31 |          227.80 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          351.00 |           19.80 |   50,501,238.18 |
|               2 |          360.00 |           19.91 |   50,229,281.39 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          135.00 |            7.62 |  131,303,219.26 |
|               2 |          143.00 |            7.91 |  126,451,337.76 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           32.00 |            1.81 |  553,935,456.25 |
|               2 |           40.00 |            2.21 |  452,063,532.50 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       17,725.00 |          999.95 |    1,000,052.73 |
|               2 |       18,083.00 |        1,000.03 |      999,974.63 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.33 |  300,439,569.49 |
|               2 |           59.00 |            3.26 |  306,483,750.85 |


