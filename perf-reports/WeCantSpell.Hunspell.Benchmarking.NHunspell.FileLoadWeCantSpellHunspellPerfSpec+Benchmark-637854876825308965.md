# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_04/13/2022 23:01:22_
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
|TotalBytesAllocated |           bytes |  119,470,616.00 |  119,451,576.00 |  119,432,536.00 |       26,926.63 |
|TotalCollections [Gen0] |     collections |          486.00 |          486.00 |          486.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          190.00 |          190.00 |          190.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           48.00 |           48.00 |           48.00 |            0.00 |
|    Elapsed Time |              ms |       19,773.00 |       19,620.00 |       19,467.00 |          216.37 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,135,133.44 |    6,088,483.08 |    6,041,832.72 |       65,973.57 |
|TotalCollections [Gen0] |     collections |           24.97 |           24.77 |           24.58 |            0.27 |
|TotalCollections [Gen1] |     collections |            9.76 |            9.68 |            9.61 |            0.11 |
|TotalCollections [Gen2] |     collections |            2.47 |            2.45 |            2.43 |            0.03 |
|    Elapsed Time |              ms |        1,000.00 |          999.98 |          999.95 |            0.03 |
|[Counter] FilePairsLoaded |      operations |            3.03 |            3.01 |            2.98 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  119,470,616.00 |    6,041,832.72 |          165.51 |
|               2 |  119,432,536.00 |    6,135,133.44 |          163.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          486.00 |           24.58 |   40,687,043.83 |
|               2 |          486.00 |           24.97 |   40,055,519.75 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          190.00 |            9.61 |  104,073,175.26 |
|               2 |          190.00 |            9.76 |  102,457,803.16 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           48.00 |            2.43 |  411,956,318.75 |
|               2 |           48.00 |            2.47 |  405,562,137.50 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       19,773.00 |          999.95 |    1,000,045.68 |
|               2 |       19,467.00 |        1,000.00 |      999,999.11 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            2.98 |  335,150,903.39 |
|               2 |           59.00 |            3.03 |  329,948,857.63 |


