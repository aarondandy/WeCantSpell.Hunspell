# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/10/2022 01:22:12_
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
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,009.00 |        1,004.67 |        1,001.00 |            4.04 |
|[Counter] _wordsChecked |      operations |    1,334,368.00 |    1,334,368.00 |    1,334,368.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.06 |          999.82 |          999.63 |            0.22 |
|[Counter] _wordsChecked |      operations |    1,332,702.92 |    1,327,939.19 |    1,321,980.64 |        5,460.08 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,937,900.00 |
|               2 |            0.00 |            0.00 |1,009,370,300.00 |
|               3 |            0.00 |            0.00 |1,001,249,400.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,937,900.00 |
|               2 |            0.00 |            0.00 |1,009,370,300.00 |
|               3 |            0.00 |            0.00 |1,001,249,400.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,937,900.00 |
|               2 |            0.00 |            0.00 |1,009,370,300.00 |
|               3 |            0.00 |            0.00 |1,001,249,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,937,900.00 |
|               2 |            0.00 |            0.00 |1,009,370,300.00 |
|               3 |            0.00 |            0.00 |1,001,249,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,004.00 |        1,000.06 |      999,938.15 |
|               2 |        1,009.00 |          999.63 |    1,000,367.00 |
|               3 |        1,001.00 |          999.75 |    1,000,249.15 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,334,368.00 |    1,329,134.00 |          752.37 |
|               2 |    1,334,368.00 |    1,321,980.64 |          756.44 |
|               3 |    1,334,368.00 |    1,332,702.92 |          750.35 |


