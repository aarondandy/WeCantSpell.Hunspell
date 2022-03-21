# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/21/2022 03:21:30_
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
|    Elapsed Time |              ms |        1,012.00 |        1,010.67 |        1,009.00 |            1.53 |
|[Counter] _wordsChecked |      operations |    1,317,792.00 |    1,317,792.00 |    1,317,792.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.81 |          999.62 |          999.53 |            0.16 |
|[Counter] _wordsChecked |      operations |    1,305,786.08 |    1,303,396.37 |    1,301,564.48 |        2,165.37 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,194,400.00 |
|               2 |            0.00 |            0.00 |1,011,477,600.00 |
|               3 |            0.00 |            0.00 |1,012,467,700.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,194,400.00 |
|               2 |            0.00 |            0.00 |1,011,477,600.00 |
|               3 |            0.00 |            0.00 |1,012,467,700.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,194,400.00 |
|               2 |            0.00 |            0.00 |1,011,477,600.00 |
|               3 |            0.00 |            0.00 |1,012,467,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,009,194,400.00 |
|               2 |            0.00 |            0.00 |1,011,477,600.00 |
|               3 |            0.00 |            0.00 |1,012,467,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,009.00 |          999.81 |    1,000,192.67 |
|               2 |        1,011.00 |          999.53 |    1,000,472.40 |
|               3 |        1,012.00 |          999.54 |    1,000,462.15 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,317,792.00 |    1,305,786.08 |          765.82 |
|               2 |    1,317,792.00 |    1,302,838.54 |          767.55 |
|               3 |    1,317,792.00 |    1,301,564.48 |          768.31 |


