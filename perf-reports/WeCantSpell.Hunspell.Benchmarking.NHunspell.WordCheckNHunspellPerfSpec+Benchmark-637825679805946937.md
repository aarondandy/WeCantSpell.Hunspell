# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/11/2022 03:59:40_
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
|    Elapsed Time |              ms |        1,022.00 |        1,021.67 |        1,021.00 |            0.58 |
|[Counter] _wordsChecked |      operations |    1,259,776.00 |    1,259,776.00 |    1,259,776.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.46 |        1,000.36 |        1,000.25 |            0.10 |
|[Counter] _wordsChecked |      operations |    1,234,311.05 |    1,233,501.75 |    1,232,969.03 |          712.48 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,020,630,900.00 |
|               2 |            0.00 |            0.00 |1,021,529,600.00 |
|               3 |            0.00 |            0.00 |1,021,741,800.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,020,630,900.00 |
|               2 |            0.00 |            0.00 |1,021,529,600.00 |
|               3 |            0.00 |            0.00 |1,021,741,800.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,020,630,900.00 |
|               2 |            0.00 |            0.00 |1,021,529,600.00 |
|               3 |            0.00 |            0.00 |1,021,741,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,020,630,900.00 |
|               2 |            0.00 |            0.00 |1,021,529,600.00 |
|               3 |            0.00 |            0.00 |1,021,741,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,021.00 |        1,000.36 |      999,638.49 |
|               2 |        1,022.00 |        1,000.46 |      999,539.73 |
|               3 |        1,022.00 |        1,000.25 |      999,747.36 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,259,776.00 |    1,234,311.05 |          810.17 |
|               2 |    1,259,776.00 |    1,233,225.16 |          810.88 |
|               3 |    1,259,776.00 |    1,232,969.03 |          811.05 |


