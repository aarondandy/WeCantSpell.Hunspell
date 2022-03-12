# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/12/2022 02:49:48_
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
|    Elapsed Time |              ms |        1,004.00 |          986.67 |          972.00 |           16.17 |
|[Counter] _wordsChecked |      operations |    1,243,200.00 |    1,243,200.00 |    1,243,200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.04 |          999.93 |          999.73 |            0.17 |
|[Counter] _wordsChecked |      operations |    1,278,662.03 |    1,260,129.80 |    1,238,300.91 |       20,381.51 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  983,990,800.00 |
|               2 |            0.00 |            0.00 |1,003,956,300.00 |
|               3 |            0.00 |            0.00 |  972,266,300.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  983,990,800.00 |
|               2 |            0.00 |            0.00 |1,003,956,300.00 |
|               3 |            0.00 |            0.00 |  972,266,300.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  983,990,800.00 |
|               2 |            0.00 |            0.00 |1,003,956,300.00 |
|               3 |            0.00 |            0.00 |  972,266,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  983,990,800.00 |
|               2 |            0.00 |            0.00 |1,003,956,300.00 |
|               3 |            0.00 |            0.00 |  972,266,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          984.00 |        1,000.01 |      999,990.65 |
|               2 |        1,004.00 |        1,000.04 |      999,956.47 |
|               3 |          972.00 |          999.73 |    1,000,273.97 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,243,200.00 |    1,263,426.45 |          791.50 |
|               2 |    1,243,200.00 |    1,238,300.91 |          807.56 |
|               3 |    1,243,200.00 |    1,278,662.03 |          782.07 |


