# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/08/2022 05:25:47_
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
|TotalBytesAllocated |           bytes |   35,040,336.00 |   35,028,284.00 |   35,016,232.00 |       17,044.10 |
|TotalCollections [Gen0] |     collections |          619.00 |          617.00 |          615.00 |            2.83 |
|TotalCollections [Gen1] |     collections |          251.00 |          250.50 |          250.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           72.00 |           71.50 |           71.00 |            0.71 |
|    Elapsed Time |              ms |       19,102.00 |       19,083.50 |       19,065.00 |           26.16 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,837,886.80 |    1,835,502.75 |    1,833,118.69 |        3,371.56 |
|TotalCollections [Gen0] |     collections |           32.40 |           32.33 |           32.26 |            0.10 |
|TotalCollections [Gen1] |     collections |           13.14 |           13.13 |           13.11 |            0.02 |
|TotalCollections [Gen2] |     collections |            3.78 |            3.75 |            3.72 |            0.04 |
|    Elapsed Time |              ms |        1,000.00 |          999.99 |          999.97 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.09 |            3.09 |            3.09 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   35,040,336.00 |    1,837,886.80 |          544.10 |
|               2 |   35,016,232.00 |    1,833,118.69 |          545.52 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          615.00 |           32.26 |   31,000,906.02 |
|               2 |          619.00 |           32.40 |   30,859,449.60 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          250.00 |           13.11 |   76,262,228.80 |
|               2 |          251.00 |           13.14 |   76,103,582.87 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           72.00 |            3.78 |  264,799,405.56 |
|               2 |           71.00 |            3.72 |  269,042,243.66 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       19,065.00 |          999.97 |    1,000,029.23 |
|               2 |       19,102.00 |        1,000.00 |      999,999.96 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.09 |  323,145,037.29 |
|               2 |           59.00 |            3.09 |  323,762,700.00 |


