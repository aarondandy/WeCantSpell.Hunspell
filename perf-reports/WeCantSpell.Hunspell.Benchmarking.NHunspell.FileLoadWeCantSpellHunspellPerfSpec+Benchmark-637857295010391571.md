# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_04/16/2022 18:11:41_
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
|TotalBytesAllocated |           bytes |  120,544,664.00 |  119,987,380.00 |  119,430,096.00 |      788,118.59 |
|TotalCollections [Gen0] |     collections |          487.00 |          485.50 |          484.00 |            2.12 |
|TotalCollections [Gen1] |     collections |          191.00 |          189.50 |          188.00 |            2.12 |
|TotalCollections [Gen2] |     collections |           48.00 |           47.00 |           46.00 |            1.41 |
|    Elapsed Time |              ms |       17,718.00 |       17,712.50 |       17,707.00 |            7.78 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,808,064.10 |    6,774,285.60 |    6,740,507.11 |       47,770.00 |
|TotalCollections [Gen0] |     collections |           27.50 |           27.41 |           27.32 |            0.13 |
|TotalCollections [Gen1] |     collections |           10.79 |           10.70 |           10.61 |            0.12 |
|TotalCollections [Gen2] |     collections |            2.71 |            2.65 |            2.60 |            0.08 |
|    Elapsed Time |              ms |        1,000.05 |        1,000.02 |          999.99 |            0.04 |
|[Counter] FilePairsLoaded |      operations |            3.33 |            3.33 |            3.33 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  120,544,664.00 |    6,808,064.10 |          146.88 |
|               2 |  119,430,096.00 |    6,740,507.11 |          148.36 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          487.00 |           27.50 |   36,357,615.61 |
|               2 |          484.00 |           27.32 |   36,607,986.98 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          191.00 |           10.79 |   92,702,402.09 |
|               2 |          188.00 |           10.61 |   94,246,094.15 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           48.00 |            2.71 |  368,878,308.33 |
|               2 |           46.00 |            2.60 |  385,179,689.13 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       17,707.00 |        1,000.05 |      999,952.49 |
|               2 |       17,718.00 |          999.99 |    1,000,015.00 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.33 |  300,104,386.44 |
|               2 |           59.00 |            3.33 |  300,309,588.14 |


