# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/21/2022 04:26:44_
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
|TotalBytesAllocated |           bytes |  124,196,520.00 |  123,885,480.00 |  123,574,440.00 |      439,876.99 |
|TotalCollections [Gen0] |     collections |          482.00 |          480.00 |          478.00 |            2.83 |
|TotalCollections [Gen1] |     collections |          183.00 |          180.50 |          178.00 |            3.54 |
|TotalCollections [Gen2] |     collections |           41.00 |           39.50 |           38.00 |            2.12 |
|    Elapsed Time |              ms |       17,877.00 |       17,782.00 |       17,687.00 |          134.35 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,021,927.35 |    6,967,241.85 |    6,912,556.36 |       77,336.97 |
|TotalCollections [Gen0] |     collections |           27.25 |           27.00 |           26.74 |            0.36 |
|TotalCollections [Gen1] |     collections |           10.35 |           10.15 |            9.96 |            0.28 |
|TotalCollections [Gen2] |     collections |            2.32 |            2.22 |            2.13 |            0.14 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.01 |        1,000.00 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.34 |            3.32 |            3.30 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  124,196,520.00 |    7,021,927.35 |          142.41 |
|               2 |  123,574,440.00 |    6,912,556.36 |          144.66 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          482.00 |           27.25 |   36,694,929.46 |
|               2 |          478.00 |           26.74 |   37,399,179.08 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          183.00 |           10.35 |   96,650,032.79 |
|               2 |          178.00 |            9.96 |  100,431,503.37 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           41.00 |            2.32 |  431,389,170.73 |
|               2 |           38.00 |            2.13 |  470,442,305.26 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       17,687.00 |        1,000.00 |      999,997.51 |
|               2 |       17,877.00 |        1,000.01 |      999,989.24 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.34 |  299,778,915.25 |
|               2 |           59.00 |            3.30 |  302,996,738.98 |


