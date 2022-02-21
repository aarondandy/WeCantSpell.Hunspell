# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_02/21/2022 18:38:12_
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
|TotalBytesAllocated |           bytes |  127,150,104.00 |   79,071,096.00 |   30,992,088.00 |   67,993,985.18 |
|TotalCollections [Gen0] |     collections |        1,115.00 |        1,114.00 |        1,113.00 |            1.41 |
|TotalCollections [Gen1] |     collections |          377.00 |          374.00 |          371.00 |            4.24 |
|TotalCollections [Gen2] |     collections |          104.00 |          100.50 |           97.00 |            4.95 |
|    Elapsed Time |              ms |       19,243.00 |       19,182.00 |       19,121.00 |           86.27 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,649,602.00 |    4,130,101.12 |    1,610,600.24 |    3,563,112.32 |
|TotalCollections [Gen0] |     collections |           58.21 |           58.08 |           57.94 |            0.19 |
|TotalCollections [Gen1] |     collections |           19.72 |           19.50 |           19.28 |            0.31 |
|TotalCollections [Gen2] |     collections |            5.40 |            5.24 |            5.07 |            0.23 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.00 |          999.98 |            0.03 |
|[Counter] FilePairsLoaded |      operations |            3.09 |            3.08 |            3.07 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,992,088.00 |    1,610,600.24 |          620.89 |
|               2 |  127,150,104.00 |    6,649,602.00 |          150.38 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,115.00 |           57.94 |   17,257,910.40 |
|               2 |        1,113.00 |           58.21 |   17,180,108.54 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          371.00 |           19.28 |   51,866,765.77 |
|               2 |          377.00 |           19.72 |   50,720,055.17 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          104.00 |            5.40 |  185,024,712.50 |
|               2 |           97.00 |            5.07 |  197,128,461.86 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       19,243.00 |        1,000.02 |      999,977.66 |
|               2 |       19,121.00 |          999.98 |    1,000,024.10 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.07 |  326,145,255.93 |
|               2 |           59.00 |            3.09 |  324,092,555.93 |


