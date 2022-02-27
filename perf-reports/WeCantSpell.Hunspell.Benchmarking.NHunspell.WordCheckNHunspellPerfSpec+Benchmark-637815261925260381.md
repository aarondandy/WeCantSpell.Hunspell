# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_02/27/2022 02:36:32_
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
|    Elapsed Time |              ms |        1,009.00 |        1,007.67 |        1,006.00 |            1.53 |
|[Counter] _wordsChecked |      operations |    1,317,792.00 |    1,317,792.00 |    1,317,792.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.78 |          999.63 |          999.45 |            0.17 |
|[Counter] _wordsChecked |      operations |    1,309,642.36 |    1,307,280.31 |    1,305,589.44 |        2,108.18 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,346,400.00 |
|               2 |            0.00 |            0.00 |1,008,558,700.00 |
|               3 |            0.00 |            0.00 |1,006,222,800.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,346,400.00 |
|               2 |            0.00 |            0.00 |1,008,558,700.00 |
|               3 |            0.00 |            0.00 |1,006,222,800.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,346,400.00 |
|               2 |            0.00 |            0.00 |1,008,558,700.00 |
|               3 |            0.00 |            0.00 |1,006,222,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,346,400.00 |
|               2 |            0.00 |            0.00 |1,008,558,700.00 |
|               3 |            0.00 |            0.00 |1,006,222,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,009.00 |          999.66 |    1,000,343.31 |
|               2 |        1,008.00 |          999.45 |    1,000,554.27 |
|               3 |        1,006.00 |          999.78 |    1,000,221.47 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,317,792.00 |    1,305,589.44 |          765.94 |
|               2 |    1,317,792.00 |    1,306,609.12 |          765.34 |
|               3 |    1,317,792.00 |    1,309,642.36 |          763.57 |


