# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/08/2022 05:25:57_
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
|    Elapsed Time |              ms |          999.00 |          983.67 |          968.00 |           15.50 |
|[Counter] _wordsChecked |      operations |    1,102,304.00 |    1,102,304.00 |    1,102,304.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.80 |        1,000.13 |          999.25 |            0.79 |
|[Counter] _wordsChecked |      operations |    1,137,895.43 |    1,120,927.50 |    1,103,765.27 |       17,065.91 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  998,676,100.00 |
|               2 |            0.00 |            0.00 |  983,215,200.00 |
|               3 |            0.00 |            0.00 |  968,721,700.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  998,676,100.00 |
|               2 |            0.00 |            0.00 |  983,215,200.00 |
|               3 |            0.00 |            0.00 |  968,721,700.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  998,676,100.00 |
|               2 |            0.00 |            0.00 |  983,215,200.00 |
|               3 |            0.00 |            0.00 |  968,721,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  998,676,100.00 |
|               2 |            0.00 |            0.00 |  983,215,200.00 |
|               3 |            0.00 |            0.00 |  968,721,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          999.00 |        1,000.32 |      999,675.78 |
|               2 |          984.00 |        1,000.80 |      999,202.44 |
|               3 |          968.00 |          999.25 |    1,000,745.56 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,102,304.00 |    1,103,765.27 |          905.99 |
|               2 |    1,102,304.00 |    1,121,121.81 |          891.96 |
|               3 |    1,102,304.00 |    1,137,895.43 |          878.82 |


