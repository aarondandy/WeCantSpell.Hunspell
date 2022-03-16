# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/17/2022 00:55:50_
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
|TotalBytesAllocated |           bytes |  111,098,416.00 |  111,043,164.00 |  110,987,912.00 |       78,138.13 |
|TotalCollections [Gen0] |     collections |          487.00 |          484.00 |          481.00 |            4.24 |
|TotalCollections [Gen1] |     collections |          189.00 |          186.00 |          183.00 |            4.24 |
|TotalCollections [Gen2] |     collections |           45.00 |           43.00 |           41.00 |            2.83 |
|    Elapsed Time |              ms |       15,500.00 |       15,302.50 |       15,105.00 |          279.31 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,355,209.93 |    7,257,884.38 |    7,160,558.82 |      137,639.12 |
|TotalCollections [Gen0] |     collections |           32.24 |           31.64 |           31.03 |            0.85 |
|TotalCollections [Gen1] |     collections |           12.51 |           12.16 |           11.81 |            0.50 |
|TotalCollections [Gen2] |     collections |            2.98 |            2.81 |            2.65 |            0.24 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.01 |        1,000.01 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.91 |            3.86 |            3.81 |            0.07 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  110,987,912.00 |    7,160,558.82 |          139.65 |
|               2 |  111,098,416.00 |    7,355,209.93 |          135.96 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          481.00 |           31.03 |   32,224,314.55 |
|               2 |          487.00 |           32.24 |   31,015,860.37 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          183.00 |           11.81 |   84,698,881.42 |
|               2 |          189.00 |           12.51 |   79,919,174.60 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           41.00 |            2.65 |  378,046,226.83 |
|               2 |           45.00 |            2.98 |  335,660,533.33 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,500.00 |        1,000.01 |      999,993.25 |
|               2 |       15,105.00 |        1,000.02 |      999,981.73 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.81 |  262,710,089.83 |
|               2 |           59.00 |            3.91 |  256,012,271.19 |


