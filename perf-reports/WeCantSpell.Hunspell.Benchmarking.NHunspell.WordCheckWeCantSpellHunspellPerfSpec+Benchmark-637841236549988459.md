# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/29/2022 04:07:34_
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
NumberOfIterations=3, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      462,352.00 |      462,352.00 |      462,352.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           29.00 |           29.00 |           29.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,025.00 |        1,009.00 |        1,001.00 |           13.86 |
|[Counter] _wordsChecked |      operations |      629,888.00 |      629,888.00 |      629,888.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      461,768.19 |      458,120.42 |      451,064.12 |        6,112.11 |
|TotalCollections [Gen0] |     collections |           28.96 |           28.73 |           28.29 |            0.38 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.98 |          999.64 |          999.22 |            0.39 |
|[Counter] _wordsChecked |      operations |      629,092.64 |      624,123.08 |      614,509.89 |        8,326.86 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      462,352.00 |      461,768.19 |        2,165.59 |
|               2 |      462,352.00 |      451,064.12 |        2,216.98 |
|               3 |      462,352.00 |      461,528.96 |        2,166.71 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           29.00 |           28.96 |   34,526,355.17 |
|               2 |           29.00 |           28.29 |   35,345,689.66 |
|               3 |           29.00 |           28.95 |   34,544,251.72 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,264,300.00 |
|               2 |            0.00 |            0.00 |1,025,025,000.00 |
|               3 |            0.00 |            0.00 |1,001,783,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,264,300.00 |
|               2 |            0.00 |            0.00 |1,025,025,000.00 |
|               3 |            0.00 |            0.00 |1,001,783,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,001.00 |          999.74 |    1,000,264.04 |
|               2 |        1,025.00 |          999.98 |    1,000,024.39 |
|               3 |        1,001.00 |          999.22 |    1,000,782.52 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      629,888.00 |      629,092.64 |        1,589.59 |
|               2 |      629,888.00 |      614,509.89 |        1,627.31 |
|               3 |      629,888.00 |      628,766.72 |        1,590.41 |


