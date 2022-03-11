# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/11/2022 03:59:51_
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
|TotalBytesAllocated |           bytes |    1,404,520.00 |    1,404,520.00 |    1,404,520.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           76.00 |           76.00 |           76.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,031.00 |        1,026.33 |        1,024.00 |            4.04 |
|[Counter] _wordsChecked |      operations |      555,296.00 |      555,296.00 |      555,296.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,371,123.41 |    1,368,340.49 |    1,362,843.30 |        4,760.83 |
|TotalCollections [Gen0] |     collections |           74.19 |           74.04 |           73.74 |            0.26 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.41 |          999.89 |          999.60 |            0.45 |
|[Counter] _wordsChecked |      operations |      542,092.21 |      540,991.94 |      538,818.55 |        1,882.26 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,404,520.00 |    1,371,123.41 |          729.33 |
|               2 |    1,404,520.00 |    1,362,843.30 |          733.76 |
|               3 |    1,404,520.00 |    1,371,054.75 |          729.37 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           76.00 |           74.19 |   13,478,382.89 |
|               2 |           76.00 |           73.74 |   13,560,272.37 |
|               3 |           76.00 |           74.19 |   13,479,057.89 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,024,357,100.00 |
|               2 |            0.00 |            0.00 |1,030,580,700.00 |
|               3 |            0.00 |            0.00 |1,024,408,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,024,357,100.00 |
|               2 |            0.00 |            0.00 |1,030,580,700.00 |
|               3 |            0.00 |            0.00 |1,024,408,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,024.00 |          999.65 |    1,000,348.73 |
|               2 |        1,031.00 |        1,000.41 |      999,593.31 |
|               3 |        1,024.00 |          999.60 |    1,000,398.83 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      555,296.00 |      542,092.21 |        1,844.70 |
|               2 |      555,296.00 |      538,818.55 |        1,855.91 |
|               3 |      555,296.00 |      542,065.06 |        1,844.80 |


