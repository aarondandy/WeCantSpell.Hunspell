# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/28/2022 22:52:15_
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
|TotalBytesAllocated |           bytes |    3,911,248.00 |    3,911,248.00 |    3,911,248.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           30.00 |           30.00 |           30.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,014.00 |        1,010.33 |        1,006.00 |            4.04 |
|[Counter] _wordsChecked |      operations |      663,040.00 |      663,040.00 |      663,040.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,885,815.72 |    3,870,813.17 |    3,858,989.94 |       13,692.58 |
|TotalCollections [Gen0] |     collections |           29.80 |           29.69 |           29.60 |            0.11 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.45 |          999.88 |          999.46 |            0.51 |
|[Counter] _wordsChecked |      operations |      658,728.69 |      656,185.43 |      654,181.14 |        2,321.18 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,911,248.00 |    3,867,633.85 |          258.56 |
|               2 |    3,911,248.00 |    3,885,815.72 |          257.35 |
|               3 |    3,911,248.00 |    3,858,989.94 |          259.14 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           30.00 |           29.67 |   33,709,223.33 |
|               2 |           30.00 |           29.80 |   33,551,496.67 |
|               3 |           30.00 |           29.60 |   33,784,730.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,011,276,700.00 |
|               2 |            0.00 |            0.00 |1,006,544,900.00 |
|               3 |            0.00 |            0.00 |1,013,541,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,011,276,700.00 |
|               2 |            0.00 |            0.00 |1,006,544,900.00 |
|               3 |            0.00 |            0.00 |1,013,541,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,011.00 |          999.73 |    1,000,273.69 |
|               2 |        1,006.00 |          999.46 |    1,000,541.65 |
|               3 |        1,014.00 |        1,000.45 |      999,548.22 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      663,040.00 |      655,646.47 |        1,525.21 |
|               2 |      663,040.00 |      658,728.69 |        1,518.08 |
|               3 |      663,040.00 |      654,181.14 |        1,528.63 |


