# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/11/2022 23:26:04_
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
|    Elapsed Time |              ms |          981.00 |          969.00 |          954.00 |           13.75 |
|[Counter] _wordsChecked |      operations |    1,094,016.00 |    1,094,016.00 |    1,094,016.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.13 |          999.79 |          999.12 |            0.58 |
|[Counter] _wordsChecked |      operations |    1,146,912.04 |    1,128,934.25 |    1,114,224.24 |       16,587.10 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  981,863,400.00 |
|               2 |            0.00 |            0.00 |  971,882,900.00 |
|               3 |            0.00 |            0.00 |  953,879,600.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  981,863,400.00 |
|               2 |            0.00 |            0.00 |  971,882,900.00 |
|               3 |            0.00 |            0.00 |  953,879,600.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  981,863,400.00 |
|               2 |            0.00 |            0.00 |  971,882,900.00 |
|               3 |            0.00 |            0.00 |  953,879,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  981,863,400.00 |
|               2 |            0.00 |            0.00 |  971,882,900.00 |
|               3 |            0.00 |            0.00 |  953,879,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          981.00 |          999.12 |    1,000,880.12 |
|               2 |          972.00 |        1,000.12 |      999,879.53 |
|               3 |          954.00 |        1,000.13 |      999,873.79 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,094,016.00 |    1,114,224.24 |          897.49 |
|               2 |    1,094,016.00 |    1,125,666.48 |          888.36 |
|               3 |    1,094,016.00 |    1,146,912.04 |          871.91 |


