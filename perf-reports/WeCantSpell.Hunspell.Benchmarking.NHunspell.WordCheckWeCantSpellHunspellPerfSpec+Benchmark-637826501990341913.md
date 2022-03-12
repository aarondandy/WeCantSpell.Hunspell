# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/12/2022 02:49:59_
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
|TotalBytesAllocated |           bytes |    2,297,976.00 |    2,297,976.00 |    2,297,976.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           77.00 |           77.00 |           77.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,079.00 |        1,043.67 |          984.00 |           51.96 |
|[Counter] _wordsChecked |      operations |      563,584.00 |      563,584.00 |      563,584.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,334,798.81 |    2,205,533.52 |    2,131,387.00 |      112,350.57 |
|TotalCollections [Gen0] |     collections |           78.23 |           73.90 |           71.42 |            3.76 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.78 |          999.99 |          999.42 |            0.71 |
|[Counter] _wordsChecked |      operations |      572,614.88 |      540,912.27 |      522,727.66 |       27,554.24 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,297,976.00 |    2,334,798.81 |          428.30 |
|               2 |    2,297,976.00 |    2,131,387.00 |          469.18 |
|               3 |    2,297,976.00 |    2,150,414.75 |          465.03 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           77.00 |           78.23 |   12,782,190.91 |
|               2 |           77.00 |           71.42 |   14,002,076.62 |
|               3 |           77.00 |           72.06 |   13,878,180.52 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  984,228,700.00 |
|               2 |            0.00 |            0.00 |1,078,159,900.00 |
|               3 |            0.00 |            0.00 |1,068,619,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  984,228,700.00 |
|               2 |            0.00 |            0.00 |1,078,159,900.00 |
|               3 |            0.00 |            0.00 |1,068,619,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          984.00 |          999.77 |    1,000,232.42 |
|               2 |        1,079.00 |        1,000.78 |      999,221.41 |
|               3 |        1,068.00 |          999.42 |    1,000,580.43 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      563,584.00 |      572,614.88 |        1,746.37 |
|               2 |      563,584.00 |      522,727.66 |        1,913.04 |
|               3 |      563,584.00 |      527,394.26 |        1,896.11 |


