# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/11/2022 03:59:30_
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
|TotalBytesAllocated |           bytes |   29,500,840.00 |   29,499,228.00 |   29,497,616.00 |        2,279.71 |
|TotalCollections [Gen0] |     collections |          508.00 |          508.00 |          508.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          215.00 |          215.00 |          215.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           70.00 |           70.00 |           70.00 |            0.00 |
|    Elapsed Time |              ms |       15,921.00 |       15,885.50 |       15,850.00 |           50.20 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,861,185.62 |    1,856,958.97 |    1,852,732.32 |        5,977.39 |
|TotalCollections [Gen0] |     collections |           32.05 |           31.98 |           31.91 |            0.10 |
|TotalCollections [Gen1] |     collections |           13.56 |           13.53 |           13.50 |            0.04 |
|TotalCollections [Gen2] |     collections |            4.42 |            4.41 |            4.40 |            0.01 |
|    Elapsed Time |              ms |          999.99 |          999.98 |          999.96 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.72 |            3.71 |            3.71 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   29,500,840.00 |    1,861,185.62 |          537.29 |
|               2 |   29,497,616.00 |    1,852,732.32 |          539.74 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          508.00 |           32.05 |   31,201,895.67 |
|               2 |          508.00 |           31.91 |   31,340,832.48 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          215.00 |           13.56 |   73,723,548.84 |
|               2 |          215.00 |           13.50 |   74,051,827.44 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           70.00 |            4.42 |  226,436,614.29 |
|               2 |           70.00 |            4.40 |  227,444,898.57 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,850.00 |          999.96 |    1,000,035.52 |
|               2 |       15,921.00 |          999.99 |    1,000,008.98 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.72 |  268,653,610.17 |
|               2 |           59.00 |            3.71 |  269,849,879.66 |


