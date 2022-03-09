# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/09/2022 02:46:00_
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
|    Elapsed Time |              ms |        1,030.00 |        1,005.67 |          975.00 |           28.04 |
|[Counter] _wordsChecked |      operations |    1,243,200.00 |    1,243,200.00 |    1,243,200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.62 |        1,000.38 |        1,000.02 |            0.31 |
|[Counter] _wordsChecked |      operations |    1,275,724.47 |    1,237,319.28 |    1,207,019.24 |       35,062.41 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,029,975,300.00 |
|               2 |            0.00 |            0.00 |1,011,377,900.00 |
|               3 |            0.00 |            0.00 |  974,505,100.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,029,975,300.00 |
|               2 |            0.00 |            0.00 |1,011,377,900.00 |
|               3 |            0.00 |            0.00 |  974,505,100.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,029,975,300.00 |
|               2 |            0.00 |            0.00 |1,011,377,900.00 |
|               3 |            0.00 |            0.00 |  974,505,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,029,975,300.00 |
|               2 |            0.00 |            0.00 |1,011,377,900.00 |
|               3 |            0.00 |            0.00 |  974,505,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,030.00 |        1,000.02 |      999,976.02 |
|               2 |        1,012.00 |        1,000.62 |      999,385.28 |
|               3 |          975.00 |        1,000.51 |      999,492.41 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,243,200.00 |    1,207,019.24 |          828.49 |
|               2 |    1,243,200.00 |    1,229,214.12 |          813.53 |
|               3 |    1,243,200.00 |    1,275,724.47 |          783.87 |


