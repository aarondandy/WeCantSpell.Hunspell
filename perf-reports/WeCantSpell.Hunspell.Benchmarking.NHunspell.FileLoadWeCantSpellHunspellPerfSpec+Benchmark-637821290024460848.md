# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/06/2022 02:03:22_
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
|TotalBytesAllocated |           bytes |  149,194,640.00 |   94,732,600.00 |   40,270,560.00 |   77,020,955.60 |
|TotalCollections [Gen0] |     collections |          740.00 |          739.00 |          738.00 |            1.41 |
|TotalCollections [Gen1] |     collections |          296.00 |          294.00 |          292.00 |            2.83 |
|TotalCollections [Gen2] |     collections |           80.00 |           79.00 |           78.00 |            1.41 |
|    Elapsed Time |              ms |       18,848.00 |       18,829.50 |       18,811.00 |           26.16 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,915,777.43 |    5,028,321.06 |    2,140,864.68 |    4,083,479.97 |
|TotalCollections [Gen0] |     collections |           39.26 |           39.25 |           39.23 |            0.02 |
|TotalCollections [Gen1] |     collections |           15.74 |           15.61 |           15.49 |            0.17 |
|TotalCollections [Gen2] |     collections |            4.25 |            4.20 |            4.14 |            0.08 |
|    Elapsed Time |              ms |        1,000.03 |        1,000.02 |        1,000.01 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.14 |            3.13 |            3.13 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   40,270,560.00 |    2,140,864.68 |          467.10 |
|               2 |  149,194,640.00 |    7,915,777.43 |          126.33 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          738.00 |           39.23 |   25,488,371.54 |
|               2 |          740.00 |           39.26 |   25,469,940.27 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          296.00 |           15.74 |   63,548,710.14 |
|               2 |          292.00 |           15.49 |   64,547,108.90 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           80.00 |            4.25 |  235,130,227.50 |
|               2 |           78.00 |            4.14 |  241,637,894.87 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,811.00 |        1,000.03 |      999,969.07 |
|               2 |       18,848.00 |        1,000.01 |      999,987.04 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.14 |  318,820,647.46 |
|               2 |           59.00 |            3.13 |  319,453,488.14 |


