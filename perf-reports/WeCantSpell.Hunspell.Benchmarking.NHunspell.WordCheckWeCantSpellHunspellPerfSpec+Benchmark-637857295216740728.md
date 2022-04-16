# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_04/16/2022 18:12:01_
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
|TotalBytesAllocated |           bytes |    2,998,696.00 |    2,998,696.00 |    2,998,696.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,012.00 |        1,008.00 |        1,006.00 |            3.46 |
|[Counter] _wordsChecked |      operations |      679,616.00 |      679,616.00 |      679,616.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,981,335.98 |    2,975,056.91 |    2,963,019.18 |       10,428.23 |
|TotalCollections [Gen0] |     collections |           12.92 |           12.90 |           12.85 |            0.05 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.18 |        1,000.05 |          999.96 |            0.11 |
|[Counter] _wordsChecked |      operations |      675,681.57 |      674,258.50 |      671,530.31 |        2,363.42 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,998,696.00 |    2,980,815.58 |          335.48 |
|               2 |    2,998,696.00 |    2,963,019.18 |          337.49 |
|               3 |    2,998,696.00 |    2,981,335.98 |          335.42 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |           12.92 |   77,384,500.00 |
|               2 |           13.00 |           12.85 |   77,849,284.62 |
|               3 |           13.00 |           12.92 |   77,370,992.31 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,005,998,500.00 |
|               2 |            0.00 |            0.00 |1,012,040,700.00 |
|               3 |            0.00 |            0.00 |1,005,822,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,005,998,500.00 |
|               2 |            0.00 |            0.00 |1,012,040,700.00 |
|               3 |            0.00 |            0.00 |1,005,822,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,006.00 |        1,000.00 |      999,998.51 |
|               2 |        1,012.00 |          999.96 |    1,000,040.22 |
|               3 |        1,006.00 |        1,000.18 |      999,823.96 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      679,616.00 |      675,563.63 |        1,480.25 |
|               2 |      679,616.00 |      671,530.31 |        1,489.14 |
|               3 |      679,616.00 |      675,681.57 |        1,479.99 |


