# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_02/27/2022 23:02:44_
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
|    Elapsed Time |              ms |        1,010.00 |        1,009.33 |        1,009.00 |            0.58 |
|[Counter] _wordsChecked |      operations |    1,326,080.00 |    1,326,080.00 |    1,326,080.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.59 |        1,000.36 |        1,000.16 |            0.22 |
|[Counter] _wordsChecked |      operations |    1,315,030.46 |    1,314,289.01 |    1,313,161.38 |          992.58 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,675,000.00 |
|               2 |            0.00 |            0.00 |1,008,402,500.00 |
|               3 |            0.00 |            0.00 |1,009,837,800.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,675,000.00 |
|               2 |            0.00 |            0.00 |1,008,402,500.00 |
|               3 |            0.00 |            0.00 |1,009,837,800.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,675,000.00 |
|               2 |            0.00 |            0.00 |1,008,402,500.00 |
|               3 |            0.00 |            0.00 |1,009,837,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,675,000.00 |
|               2 |            0.00 |            0.00 |1,008,402,500.00 |
|               3 |            0.00 |            0.00 |1,009,837,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,009.00 |        1,000.32 |      999,677.90 |
|               2 |        1,009.00 |        1,000.59 |      999,407.83 |
|               3 |        1,010.00 |        1,000.16 |      999,839.41 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,326,080.00 |    1,314,675.19 |          760.64 |
|               2 |    1,326,080.00 |    1,315,030.46 |          760.44 |
|               3 |    1,326,080.00 |    1,313,161.38 |          761.52 |


