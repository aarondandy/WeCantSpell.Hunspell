# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/13/2022 00:18:40_
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
|TotalBytesAllocated |           bytes |  111,185,816.00 |   70,941,416.00 |   30,697,016.00 |   56,914,176.29 |
|TotalCollections [Gen0] |     collections |          499.00 |          497.50 |          496.00 |            2.12 |
|TotalCollections [Gen1] |     collections |          204.00 |          202.00 |          200.00 |            2.83 |
|TotalCollections [Gen2] |     collections |           62.00 |           60.00 |           58.00 |            2.83 |
|    Elapsed Time |              ms |       15,586.00 |       15,570.00 |       15,554.00 |           22.63 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,133,836.07 |    4,553,716.94 |    1,973,597.81 |    3,648,839.47 |
|TotalCollections [Gen0] |     collections |           32.02 |           31.95 |           31.89 |            0.09 |
|TotalCollections [Gen1] |     collections |           13.09 |           12.97 |           12.86 |            0.16 |
|TotalCollections [Gen2] |     collections |            3.98 |            3.85 |            3.73 |            0.18 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.01 |        1,000.01 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.79 |            3.79 |            3.79 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  111,185,816.00 |    7,133,836.07 |          140.18 |
|               2 |   30,697,016.00 |    1,973,597.81 |          506.69 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          499.00 |           32.02 |   31,233,864.13 |
|               2 |          496.00 |           31.89 |   31,358,539.72 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          204.00 |           13.09 |   76,400,481.37 |
|               2 |          200.00 |           12.86 |   77,769,178.50 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           62.00 |            3.98 |  251,382,229.03 |
|               2 |           58.00 |            3.73 |  268,169,581.03 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,586.00 |        1,000.02 |      999,980.64 |
|               2 |       15,554.00 |        1,000.01 |      999,989.44 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.79 |  264,164,376.27 |
|               2 |           59.00 |            3.79 |  263,624,333.90 |


