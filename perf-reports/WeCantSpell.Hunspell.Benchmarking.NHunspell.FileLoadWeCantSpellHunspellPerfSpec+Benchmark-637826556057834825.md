# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/12/2022 04:20:05_
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
|TotalBytesAllocated |           bytes |   30,696,888.00 |   30,696,028.00 |   30,695,168.00 |        1,216.22 |
|TotalCollections [Gen0] |     collections |          505.00 |          501.00 |          497.00 |            5.66 |
|TotalCollections [Gen1] |     collections |          210.00 |          207.00 |          204.00 |            4.24 |
|TotalCollections [Gen2] |     collections |           66.00 |           64.00 |           62.00 |            2.83 |
|    Elapsed Time |              ms |       15,995.00 |       15,575.50 |       15,156.00 |          593.26 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,025,279.28 |    1,972,240.85 |    1,919,202.42 |       75,007.67 |
|TotalCollections [Gen0] |     collections |           33.32 |           32.20 |           31.07 |            1.59 |
|TotalCollections [Gen1] |     collections |           13.86 |           13.31 |           12.75 |            0.78 |
|TotalCollections [Gen2] |     collections |            4.35 |            4.12 |            3.88 |            0.34 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.01 |        1,000.00 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.89 |            3.79 |            3.69 |            0.14 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,695,168.00 |    2,025,279.28 |          493.76 |
|               2 |   30,696,888.00 |    1,919,202.42 |          521.05 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          505.00 |           33.32 |   30,011,915.64 |
|               2 |          497.00 |           31.07 |   32,182,307.44 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          210.00 |           13.86 |   72,171,511.43 |
|               2 |          204.00 |           12.75 |   78,404,935.29 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           66.00 |            4.35 |  229,636,627.27 |
|               2 |           62.00 |            3.88 |  257,977,529.03 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,156.00 |        1,000.00 |    1,000,001.15 |
|               2 |       15,995.00 |        1,000.02 |      999,975.42 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.89 |  256,881,650.85 |
|               2 |           59.00 |            3.69 |  271,095,030.51 |


