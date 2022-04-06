# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_04/06/2022 20:06:11_
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
|    Elapsed Time |              ms |        1,011.00 |        1,009.67 |        1,009.00 |            1.15 |
|[Counter] _wordsChecked |      operations |    1,309,504.00 |    1,309,504.00 |    1,309,504.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.83 |        1,000.15 |          999.79 |            0.59 |
|[Counter] _wordsChecked |      operations |    1,297,593.13 |    1,297,156.29 |    1,296,326.32 |          719.11 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,179,200.00 |
|               2 |            0.00 |            0.00 |1,009,213,200.00 |
|               3 |            0.00 |            0.00 |1,010,165,400.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,179,200.00 |
|               2 |            0.00 |            0.00 |1,009,213,200.00 |
|               3 |            0.00 |            0.00 |1,010,165,400.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,179,200.00 |
|               2 |            0.00 |            0.00 |1,009,213,200.00 |
|               3 |            0.00 |            0.00 |1,010,165,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,179,200.00 |
|               2 |            0.00 |            0.00 |1,009,213,200.00 |
|               3 |            0.00 |            0.00 |1,010,165,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,009.00 |          999.82 |    1,000,177.60 |
|               2 |        1,009.00 |          999.79 |    1,000,211.30 |
|               3 |        1,011.00 |        1,000.83 |      999,174.48 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,309,504.00 |    1,297,593.13 |          770.66 |
|               2 |    1,309,504.00 |    1,297,549.42 |          770.68 |
|               3 |    1,309,504.00 |    1,296,326.32 |          771.41 |


