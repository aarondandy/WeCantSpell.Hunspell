# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_02/22/2022 02:21:31_
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
|TotalBytesAllocated |           bytes |   31,035,552.00 |   31,011,628.00 |   30,987,704.00 |       33,833.65 |
|TotalCollections [Gen0] |     collections |        1,116.00 |        1,113.50 |        1,111.00 |            3.54 |
|TotalCollections [Gen1] |     collections |          377.00 |          374.50 |          372.00 |            3.54 |
|TotalCollections [Gen2] |     collections |          103.00 |          100.00 |           97.00 |            4.24 |
|    Elapsed Time |              ms |       19,048.00 |       18,975.50 |       18,903.00 |          102.53 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,639,252.85 |    1,634,289.80 |    1,629,326.75 |        7,018.81 |
|TotalCollections [Gen0] |     collections |           58.77 |           58.68 |           58.59 |            0.13 |
|TotalCollections [Gen1] |     collections |           19.79 |           19.74 |           19.68 |            0.08 |
|TotalCollections [Gen2] |     collections |            5.41 |            5.27 |            5.13 |            0.20 |
|    Elapsed Time |              ms |        1,000.00 |          999.98 |          999.97 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.12 |            3.11 |            3.10 |            0.02 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,987,704.00 |    1,639,252.85 |          610.03 |
|               2 |   31,035,552.00 |    1,629,326.75 |          613.75 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,111.00 |           58.77 |   17,014,899.64 |
|               2 |        1,116.00 |           58.59 |   17,068,175.18 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          372.00 |           19.68 |   50,816,004.03 |
|               2 |          377.00 |           19.79 |   50,525,420.42 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           97.00 |            5.13 |  194,881,994.85 |
|               2 |          103.00 |            5.41 |  184,932,849.51 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,903.00 |          999.97 |    1,000,029.28 |
|               2 |       19,048.00 |        1,000.00 |    1,000,004.38 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.12 |  320,399,211.86 |
|               2 |           59.00 |            3.10 |  322,848,872.88 |


