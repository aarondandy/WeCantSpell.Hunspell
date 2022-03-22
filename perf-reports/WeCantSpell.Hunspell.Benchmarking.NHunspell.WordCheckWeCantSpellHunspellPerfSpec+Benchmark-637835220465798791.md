# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/22/2022 05:00:46_
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
|TotalBytesAllocated |           bytes |    4,489,240.00 |    4,489,240.00 |    4,489,240.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           60.00 |           60.00 |           60.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,035.00 |        1,019.33 |        1,007.00 |           14.29 |
|[Counter] _wordsChecked |      operations |      596,736.00 |      596,736.00 |      596,736.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,460,382.66 |    4,404,676.61 |    4,337,151.70 |       62,459.84 |
|TotalCollections [Gen0] |     collections |           59.61 |           58.87 |           57.97 |            0.83 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.53 |        1,000.00 |          999.54 |            0.50 |
|[Counter] _wordsChecked |      operations |      592,900.11 |      585,495.34 |      576,519.54 |        8,302.53 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,489,240.00 |    4,337,151.70 |          230.57 |
|               2 |    4,489,240.00 |    4,416,495.46 |          226.42 |
|               3 |    4,489,240.00 |    4,460,382.66 |          224.20 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           60.00 |           57.97 |   17,251,106.67 |
|               2 |           60.00 |           59.03 |   16,941,185.00 |
|               3 |           60.00 |           59.61 |   16,774,495.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,035,066,400.00 |
|               2 |            0.00 |            0.00 |1,016,471,100.00 |
|               3 |            0.00 |            0.00 |1,006,469,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,035,066,400.00 |
|               2 |            0.00 |            0.00 |1,016,471,100.00 |
|               3 |            0.00 |            0.00 |1,006,469,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,035.00 |          999.94 |    1,000,064.15 |
|               2 |        1,016.00 |          999.54 |    1,000,463.68 |
|               3 |        1,007.00 |        1,000.53 |      999,473.39 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      596,736.00 |      576,519.54 |        1,734.55 |
|               2 |      596,736.00 |      587,066.37 |        1,703.38 |
|               3 |      596,736.00 |      592,900.11 |        1,686.62 |


