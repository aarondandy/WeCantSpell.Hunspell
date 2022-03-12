# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/12/2022 03:35:43_
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
|TotalBytesAllocated |           bytes |   30,696,808.00 |   30,695,868.00 |   30,694,928.00 |        1,329.36 |
|TotalCollections [Gen0] |     collections |          505.00 |          501.00 |          497.00 |            5.66 |
|TotalCollections [Gen1] |     collections |          210.00 |          207.00 |          204.00 |            4.24 |
|TotalCollections [Gen2] |     collections |           66.00 |           64.00 |           62.00 |            2.83 |
|    Elapsed Time |              ms |       15,621.00 |       15,531.00 |       15,441.00 |          127.28 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,988,038.79 |    1,976,527.46 |    1,965,016.13 |       16,279.48 |
|TotalCollections [Gen0] |     collections |           32.33 |           32.26 |           32.19 |            0.10 |
|TotalCollections [Gen1] |     collections |           13.44 |           13.33 |           13.21 |            0.16 |
|TotalCollections [Gen2] |     collections |            4.23 |            4.12 |            4.02 |            0.15 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.02 |        1,000.02 |            0.00 |
|[Counter] FilePairsLoaded |      operations |            3.82 |            3.80 |            3.78 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,696,808.00 |    1,988,038.79 |          503.01 |
|               2 |   30,694,928.00 |    1,965,016.13 |          508.90 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          497.00 |           32.19 |   31,067,905.43 |
|               2 |          505.00 |           32.33 |   30,932,079.80 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          204.00 |           13.21 |   75,689,946.08 |
|               2 |          210.00 |           13.44 |   74,384,287.14 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           62.00 |            4.02 |  249,044,338.71 |
|               2 |           66.00 |            4.23 |  236,677,277.27 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,441.00 |        1,000.02 |      999,983.74 |
|               2 |       15,621.00 |        1,000.02 |      999,980.81 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.82 |  261,707,610.17 |
|               2 |           59.00 |            3.78 |  264,757,632.20 |


