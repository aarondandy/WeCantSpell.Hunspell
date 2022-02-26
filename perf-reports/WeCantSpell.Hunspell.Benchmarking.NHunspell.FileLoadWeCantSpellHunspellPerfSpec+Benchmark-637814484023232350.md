# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_02/26/2022 05:00:02_
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
|TotalBytesAllocated |           bytes |  126,708,256.00 |   64,049,580.00 |    1,390,904.00 |   88,612,749.40 |
|TotalCollections [Gen0] |     collections |        1,234.00 |        1,227.50 |        1,221.00 |            9.19 |
|TotalCollections [Gen1] |     collections |          410.00 |          404.00 |          398.00 |            8.49 |
|TotalCollections [Gen2] |     collections |          124.00 |          114.50 |          105.00 |           13.44 |
|    Elapsed Time |              ms |       20,801.00 |       20,793.50 |       20,786.00 |           10.61 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,091,454.90 |    3,079,185.28 |       66,915.66 |    4,259,992.55 |
|TotalCollections [Gen0] |     collections |           59.32 |           59.03 |           58.74 |            0.41 |
|TotalCollections [Gen1] |     collections |           19.71 |           19.43 |           19.15 |            0.40 |
|TotalCollections [Gen2] |     collections |            5.96 |            5.51 |            5.05 |            0.64 |
|    Elapsed Time |              ms |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] FilePairsLoaded |      operations |            2.84 |            2.84 |            2.84 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,390,904.00 |       66,915.66 |       14,944.19 |
|               2 |  126,708,256.00 |    6,091,454.90 |          164.16 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,221.00 |           58.74 |   17,023,691.32 |
|               2 |        1,234.00 |           59.32 |   16,856,551.05 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          398.00 |           19.15 |   52,225,947.49 |
|               2 |          410.00 |           19.71 |   50,734,107.32 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          105.00 |            5.05 |  197,961,210.48 |
|               2 |          124.00 |            5.96 |  167,749,870.97 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       20,786.00 |        1,000.00 |      999,996.49 |
|               2 |       20,801.00 |        1,000.00 |      999,999.23 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            2.84 |  352,303,849.15 |
|               2 |           59.00 |            2.84 |  352,559,050.85 |


