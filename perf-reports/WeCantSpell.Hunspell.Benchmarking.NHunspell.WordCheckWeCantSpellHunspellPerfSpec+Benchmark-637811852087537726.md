# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_02/23/2022 03:53:28_
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
|TotalBytesAllocated |           bytes |    4,123,216.00 |    4,123,216.00 |    4,123,216.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           77.00 |           77.00 |           77.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,011.00 |        1,008.67 |        1,007.00 |            2.08 |
|[Counter] _wordsChecked |      operations |      671,328.00 |      671,328.00 |      671,328.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,092,846.26 |    4,086,200.15 |    4,075,980.28 |        8,982.99 |
|TotalCollections [Gen0] |     collections |           76.43 |           76.31 |           76.12 |            0.17 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.82 |          999.61 |          999.42 |            0.20 |
|[Counter] _wordsChecked |      operations |      666,383.30 |      665,301.21 |      663,637.24 |        1,462.58 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,123,216.00 |    4,092,846.26 |          244.33 |
|               2 |    4,123,216.00 |    4,089,773.92 |          244.51 |
|               3 |    4,123,216.00 |    4,075,980.28 |          245.34 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           77.00 |           76.43 |   13,083,379.22 |
|               2 |           77.00 |           76.38 |   13,093,207.79 |
|               3 |           77.00 |           76.12 |   13,137,516.88 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,420,200.00 |
|               2 |            0.00 |            0.00 |1,008,177,000.00 |
|               3 |            0.00 |            0.00 |1,011,588,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,420,200.00 |
|               2 |            0.00 |            0.00 |1,008,177,000.00 |
|               3 |            0.00 |            0.00 |1,011,588,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,007.00 |          999.58 |    1,000,417.28 |
|               2 |        1,008.00 |          999.82 |    1,000,175.60 |
|               3 |        1,011.00 |          999.42 |    1,000,582.39 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      671,328.00 |      666,383.30 |        1,500.64 |
|               2 |      671,328.00 |      665,883.07 |        1,501.77 |
|               3 |      671,328.00 |      663,637.24 |        1,506.85 |


