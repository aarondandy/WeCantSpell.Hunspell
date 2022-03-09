# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/09/2022 04:01:57_
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
|TotalBytesAllocated |           bytes |    1,621,504.00 |    1,621,504.00 |    1,621,504.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           71.00 |           71.00 |           71.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,004.00 |          989.00 |          970.00 |           17.35 |
|[Counter] _wordsChecked |      operations |      605,024.00 |      605,024.00 |      605,024.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,671,400.31 |    1,639,835.14 |    1,614,306.29 |       29,021.71 |
|TotalCollections [Gen0] |     collections |           73.18 |           71.80 |           70.68 |            1.27 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.53 |          999.97 |          999.54 |            0.50 |
|[Counter] _wordsChecked |      operations |      623,641.57 |      611,863.81 |      602,338.35 |       10,828.73 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,621,504.00 |    1,671,400.31 |          598.30 |
|               2 |    1,621,504.00 |    1,614,306.29 |          619.46 |
|               3 |    1,621,504.00 |    1,633,798.83 |          612.07 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           71.00 |           73.18 |   13,664,042.25 |
|               2 |           71.00 |           70.68 |   14,147,305.63 |
|               3 |           71.00 |           71.54 |   13,978,516.90 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  970,147,000.00 |
|               2 |            0.00 |            0.00 |1,004,458,700.00 |
|               3 |            0.00 |            0.00 |  992,474,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  970,147,000.00 |
|               2 |            0.00 |            0.00 |1,004,458,700.00 |
|               3 |            0.00 |            0.00 |  992,474,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          970.00 |          999.85 |    1,000,151.55 |
|               2 |        1,004.00 |          999.54 |    1,000,456.87 |
|               3 |          993.00 |        1,000.53 |      999,471.00 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      605,024.00 |      623,641.57 |        1,603.49 |
|               2 |      605,024.00 |      602,338.35 |        1,660.20 |
|               3 |      605,024.00 |      609,611.51 |        1,640.39 |


