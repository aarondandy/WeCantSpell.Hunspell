# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/09/2022 04:01:47_
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
|    Elapsed Time |              ms |        1,004.00 |          988.67 |          979.00 |           13.43 |
|[Counter] _wordsChecked |      operations |    1,243,200.00 |    1,243,200.00 |    1,243,200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.90 |          999.75 |          999.56 |            0.18 |
|[Counter] _wordsChecked |      operations |    1,269,305.81 |    1,257,287.20 |    1,237,977.96 |       16,888.56 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,004,218,200.00 |
|               2 |            0.00 |            0.00 |  983,094,900.00 |
|               3 |            0.00 |            0.00 |  979,433,000.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,004,218,200.00 |
|               2 |            0.00 |            0.00 |  983,094,900.00 |
|               3 |            0.00 |            0.00 |  979,433,000.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,004,218,200.00 |
|               2 |            0.00 |            0.00 |  983,094,900.00 |
|               3 |            0.00 |            0.00 |  979,433,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,004,218,200.00 |
|               2 |            0.00 |            0.00 |  983,094,900.00 |
|               3 |            0.00 |            0.00 |  979,433,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,004.00 |          999.78 |    1,000,217.33 |
|               2 |          983.00 |          999.90 |    1,000,096.54 |
|               3 |          979.00 |          999.56 |    1,000,442.29 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,243,200.00 |    1,237,977.96 |          807.77 |
|               2 |    1,243,200.00 |    1,264,577.81 |          790.78 |
|               3 |    1,243,200.00 |    1,269,305.81 |          787.83 |


