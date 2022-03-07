# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/07/2022 04:57:44_
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
|TotalBytesAllocated |           bytes |  149,161,608.00 |  149,160,336.00 |  149,159,064.00 |        1,798.88 |
|TotalCollections [Gen0] |     collections |          721.00 |          716.50 |          712.00 |            6.36 |
|TotalCollections [Gen1] |     collections |          286.00 |          281.50 |          277.00 |            6.36 |
|TotalCollections [Gen2] |     collections |           79.00 |           74.50 |           70.00 |            6.36 |
|    Elapsed Time |              ms |       18,016.00 |       17,924.00 |       17,832.00 |          130.11 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,365,061.84 |    8,322,237.32 |    8,279,412.80 |       60,563.02 |
|TotalCollections [Gen0] |     collections |           40.43 |           39.98 |           39.52 |            0.65 |
|TotalCollections [Gen1] |     collections |           16.04 |           15.71 |           15.38 |            0.47 |
|TotalCollections [Gen2] |     collections |            4.43 |            4.16 |            3.89 |            0.39 |
|    Elapsed Time |              ms |        1,000.05 |        1,000.02 |        1,000.00 |            0.03 |
|[Counter] FilePairsLoaded |      operations |            3.31 |            3.29 |            3.27 |            0.02 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  149,159,064.00 |    8,365,061.84 |          119.54 |
|               2 |  149,161,608.00 |    8,279,412.80 |          120.78 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          721.00 |           40.43 |   24,731,202.22 |
|               2 |          712.00 |           39.52 |   25,303,321.07 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          286.00 |           16.04 |   62,346,841.96 |
|               2 |          277.00 |           15.38 |   65,039,583.39 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           79.00 |            4.43 |  225,711,351.90 |
|               2 |           70.00 |            3.89 |  257,370,922.86 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       17,832.00 |        1,000.05 |      999,954.96 |
|               2 |       18,016.00 |        1,000.00 |      999,998.04 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.31 |  302,223,674.58 |
|               2 |           59.00 |            3.27 |  305,355,332.20 |


