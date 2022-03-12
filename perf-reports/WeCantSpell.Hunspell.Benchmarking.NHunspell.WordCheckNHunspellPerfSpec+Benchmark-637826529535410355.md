# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/12/2022 03:35:53_
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
|    Elapsed Time |              ms |        1,019.00 |        1,010.33 |        1,003.00 |            8.08 |
|[Counter] _wordsChecked |      operations |    1,292,928.00 |    1,292,928.00 |    1,292,928.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.46 |          999.98 |          999.66 |            0.43 |
|[Counter] _wordsChecked |      operations |    1,288,623.61 |    1,279,725.53 |    1,269,406.78 |        9,686.87 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,018,529,300.00 |
|               2 |            0.00 |            0.00 |1,003,340,300.00 |
|               3 |            0.00 |            0.00 |1,009,196,300.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,018,529,300.00 |
|               2 |            0.00 |            0.00 |1,003,340,300.00 |
|               3 |            0.00 |            0.00 |1,009,196,300.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,018,529,300.00 |
|               2 |            0.00 |            0.00 |1,003,340,300.00 |
|               3 |            0.00 |            0.00 |1,009,196,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,018,529,300.00 |
|               2 |            0.00 |            0.00 |1,003,340,300.00 |
|               3 |            0.00 |            0.00 |1,009,196,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,019.00 |        1,000.46 |      999,538.08 |
|               2 |        1,003.00 |          999.66 |    1,000,339.28 |
|               3 |        1,009.00 |          999.81 |    1,000,194.55 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,292,928.00 |    1,269,406.78 |          787.77 |
|               2 |    1,292,928.00 |    1,288,623.61 |          776.02 |
|               3 |    1,292,928.00 |    1,281,146.20 |          780.55 |


