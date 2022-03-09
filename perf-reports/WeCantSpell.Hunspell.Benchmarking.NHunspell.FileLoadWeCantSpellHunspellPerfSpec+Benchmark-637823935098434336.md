# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/09/2022 03:31:49_
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
|TotalBytesAllocated |           bytes |  122,061,200.00 |   77,975,040.00 |   33,888,880.00 |   62,347,245.38 |
|TotalCollections [Gen0] |     collections |          556.00 |          552.00 |          548.00 |            5.66 |
|TotalCollections [Gen1] |     collections |          232.00 |          226.50 |          221.00 |            7.78 |
|TotalCollections [Gen2] |     collections |           72.00 |           67.50 |           63.00 |            6.36 |
|    Elapsed Time |              ms |       16,271.00 |       16,193.00 |       16,115.00 |          110.31 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,501,548.20 |    4,802,270.10 |    2,102,991.99 |    3,817,355.70 |
|TotalCollections [Gen0] |     collections |           34.50 |           34.09 |           33.68 |            0.58 |
|TotalCollections [Gen1] |     collections |           14.40 |           13.99 |           13.58 |            0.58 |
|TotalCollections [Gen2] |     collections |            4.47 |            4.17 |            3.87 |            0.42 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.00 |          999.97 |            0.04 |
|[Counter] FilePairsLoaded |      operations |            3.66 |            3.64 |            3.63 |            0.02 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   33,888,880.00 |    2,102,991.99 |          475.51 |
|               2 |  122,061,200.00 |    7,501,548.20 |          133.31 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          556.00 |           34.50 |   28,983,098.02 |
|               2 |          548.00 |           33.68 |   29,692,459.49 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          232.00 |           14.40 |   69,459,493.53 |
|               2 |          221.00 |           13.58 |   73,626,551.13 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           72.00 |            4.47 |  223,813,923.61 |
|               2 |           63.00 |            3.87 |  258,277,266.67 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       16,115.00 |        1,000.02 |      999,975.33 |
|               2 |       16,271.00 |          999.97 |    1,000,028.75 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.66 |  273,128,855.93 |
|               2 |           59.00 |            3.63 |  275,787,589.83 |


