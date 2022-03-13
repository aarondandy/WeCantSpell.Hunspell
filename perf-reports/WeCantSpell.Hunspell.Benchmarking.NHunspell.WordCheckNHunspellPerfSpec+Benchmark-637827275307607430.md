# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/13/2022 00:18:50_
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
|    Elapsed Time |              ms |        1,001.00 |          986.33 |          971.00 |           15.01 |
|[Counter] _wordsChecked |      operations |    1,268,064.00 |    1,268,064.00 |    1,268,064.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.25 |        1,000.08 |          999.83 |            0.22 |
|[Counter] _wordsChecked |      operations |    1,305,715.35 |    1,285,930.04 |    1,267,118.73 |       19,316.73 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  971,164,200.00 |
|               2 |            0.00 |            0.00 |  986,854,000.00 |
|               3 |            0.00 |            0.00 |1,000,746,000.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  971,164,200.00 |
|               2 |            0.00 |            0.00 |  986,854,000.00 |
|               3 |            0.00 |            0.00 |1,000,746,000.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  971,164,200.00 |
|               2 |            0.00 |            0.00 |  986,854,000.00 |
|               3 |            0.00 |            0.00 |1,000,746,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  971,164,200.00 |
|               2 |            0.00 |            0.00 |  986,854,000.00 |
|               3 |            0.00 |            0.00 |1,000,746,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          971.00 |          999.83 |    1,000,169.10 |
|               2 |          987.00 |        1,000.15 |      999,852.08 |
|               3 |        1,001.00 |        1,000.25 |      999,746.25 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,268,064.00 |    1,305,715.35 |          765.86 |
|               2 |    1,268,064.00 |    1,284,956.03 |          778.24 |
|               3 |    1,268,064.00 |    1,267,118.73 |          789.19 |


