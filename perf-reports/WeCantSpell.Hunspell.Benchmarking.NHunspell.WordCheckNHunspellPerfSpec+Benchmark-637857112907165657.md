# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_04/16/2022 13:08:10_
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
|    Elapsed Time |              ms |        1,003.00 |        1,002.33 |        1,002.00 |            0.58 |
|[Counter] _wordsChecked |      operations |    1,326,080.00 |    1,326,080.00 |    1,326,080.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.80 |          999.64 |          999.31 |            0.28 |
|[Counter] _wordsChecked |      operations |    1,323,171.67 |    1,322,512.22 |    1,321,840.99 |          665.42 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,002,198,000.00 |
|               2 |            0.00 |            0.00 |1,002,688,800.00 |
|               3 |            0.00 |            0.00 |1,003,206,900.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,002,198,000.00 |
|               2 |            0.00 |            0.00 |1,002,688,800.00 |
|               3 |            0.00 |            0.00 |1,003,206,900.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,002,198,000.00 |
|               2 |            0.00 |            0.00 |1,002,688,800.00 |
|               3 |            0.00 |            0.00 |1,003,206,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,002,198,000.00 |
|               2 |            0.00 |            0.00 |1,002,688,800.00 |
|               3 |            0.00 |            0.00 |1,003,206,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,002.00 |          999.80 |    1,000,197.60 |
|               2 |        1,002.00 |          999.31 |    1,000,687.43 |
|               3 |        1,003.00 |          999.79 |    1,000,206.28 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,326,080.00 |    1,323,171.67 |          755.76 |
|               2 |    1,326,080.00 |    1,322,524.00 |          756.13 |
|               3 |    1,326,080.00 |    1,321,840.99 |          756.52 |


