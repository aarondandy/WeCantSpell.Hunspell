# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/01/2022 05:26:31_
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
|    Elapsed Time |              ms |        1,010.00 |        1,008.00 |        1,007.00 |            1.73 |
|[Counter] _wordsChecked |      operations |    1,309,504.00 |    1,309,504.00 |    1,309,504.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.30 |        1,000.10 |          999.98 |            0.17 |
|[Counter] _wordsChecked |      operations |    1,300,786.00 |    1,299,245.95 |    1,296,511.14 |        2,374.70 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,969,400.00 |
|               2 |            0.00 |            0.00 |1,010,021,400.00 |
|               3 |            0.00 |            0.00 |1,006,702,100.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,969,400.00 |
|               2 |            0.00 |            0.00 |1,010,021,400.00 |
|               3 |            0.00 |            0.00 |1,006,702,100.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,969,400.00 |
|               2 |            0.00 |            0.00 |1,010,021,400.00 |
|               3 |            0.00 |            0.00 |1,006,702,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,969,400.00 |
|               2 |            0.00 |            0.00 |1,010,021,400.00 |
|               3 |            0.00 |            0.00 |1,006,702,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,007.00 |        1,000.03 |      999,969.61 |
|               2 |        1,010.00 |          999.98 |    1,000,021.19 |
|               3 |        1,007.00 |        1,000.30 |      999,704.17 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,309,504.00 |    1,300,440.71 |          768.97 |
|               2 |    1,309,504.00 |    1,296,511.14 |          771.30 |
|               3 |    1,309,504.00 |    1,300,786.00 |          768.77 |


