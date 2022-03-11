# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/11/2022 01:49:58_
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
|    Elapsed Time |              ms |        1,005.00 |        1,003.00 |        1,001.00 |            2.00 |
|[Counter] _wordsChecked |      operations |    1,326,080.00 |    1,326,080.00 |    1,326,080.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.44 |        1,000.22 |          999.85 |            0.32 |
|[Counter] _wordsChecked |      operations |    1,325,337.28 |    1,322,405.11 |    1,319,968.94 |        2,718.32 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,000,560,400.00 |
|               2 |            0.00 |            0.00 |1,003,155,200.00 |
|               3 |            0.00 |            0.00 |1,004,629,700.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,000,560,400.00 |
|               2 |            0.00 |            0.00 |1,003,155,200.00 |
|               3 |            0.00 |            0.00 |1,004,629,700.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,000,560,400.00 |
|               2 |            0.00 |            0.00 |1,003,155,200.00 |
|               3 |            0.00 |            0.00 |1,004,629,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,000,560,400.00 |
|               2 |            0.00 |            0.00 |1,003,155,200.00 |
|               3 |            0.00 |            0.00 |1,004,629,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,001.00 |        1,000.44 |      999,560.84 |
|               2 |        1,003.00 |          999.85 |    1,000,154.74 |
|               3 |        1,005.00 |        1,000.37 |      999,631.54 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,326,080.00 |    1,325,337.28 |          754.52 |
|               2 |    1,326,080.00 |    1,321,909.11 |          756.48 |
|               3 |    1,326,080.00 |    1,319,968.94 |          757.59 |


