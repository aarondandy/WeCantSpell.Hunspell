# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/17/2022 00:56:01_
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
|    Elapsed Time |              ms |        1,012.00 |        1,008.67 |        1,003.00 |            4.93 |
|[Counter] _wordsChecked |      operations |    1,309,504.00 |    1,309,504.00 |    1,309,504.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.37 |        1,000.30 |        1,000.22 |            0.08 |
|[Counter] _wordsChecked |      operations |    1,305,873.41 |    1,298,659.97 |    1,294,368.69 |        6,284.42 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,010,624,200.00 |
|               2 |            0.00 |            0.00 |1,011,693,200.00 |
|               3 |            0.00 |            0.00 |1,002,780,200.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,010,624,200.00 |
|               2 |            0.00 |            0.00 |1,011,693,200.00 |
|               3 |            0.00 |            0.00 |1,002,780,200.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,010,624,200.00 |
|               2 |            0.00 |            0.00 |1,011,693,200.00 |
|               3 |            0.00 |            0.00 |1,002,780,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,010,624,200.00 |
|               2 |            0.00 |            0.00 |1,011,693,200.00 |
|               3 |            0.00 |            0.00 |1,002,780,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,011.00 |        1,000.37 |      999,628.29 |
|               2 |        1,012.00 |        1,000.30 |      999,696.84 |
|               3 |        1,003.00 |        1,000.22 |      999,780.86 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,309,504.00 |    1,295,737.82 |          771.76 |
|               2 |    1,309,504.00 |    1,294,368.69 |          772.58 |
|               3 |    1,309,504.00 |    1,305,873.41 |          765.77 |


