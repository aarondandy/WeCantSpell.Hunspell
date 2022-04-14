# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_04/14/2022 13:01:44_
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
|TotalBytesAllocated |           bytes |    5,046,696.00 |    5,046,696.00 |    5,046,696.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           12.00 |           12.00 |           12.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,012.00 |        1,011.00 |        1,010.00 |            1.00 |
|[Counter] _wordsChecked |      operations |      646,464.00 |      646,464.00 |      646,464.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,996,829.14 |    4,990,305.57 |    4,985,388.68 |        5,887.03 |
|TotalCollections [Gen0] |     collections |           11.88 |           11.87 |           11.85 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.02 |          999.70 |          999.38 |            0.32 |
|[Counter] _wordsChecked |      operations |      640,076.23 |      639,240.58 |      638,610.75 |          754.11 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,046,696.00 |    4,988,698.88 |          200.45 |
|               2 |    5,046,696.00 |    4,985,388.68 |          200.59 |
|               3 |    5,046,696.00 |    4,996,829.14 |          200.13 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           12.00 |           11.86 |   84,302,141.67 |
|               2 |           12.00 |           11.85 |   84,358,116.67 |
|               3 |           12.00 |           11.88 |   84,164,975.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,011,625,700.00 |
|               2 |            0.00 |            0.00 |1,012,297,400.00 |
|               3 |            0.00 |            0.00 |1,009,979,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,011,625,700.00 |
|               2 |            0.00 |            0.00 |1,012,297,400.00 |
|               3 |            0.00 |            0.00 |1,009,979,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,011.00 |          999.38 |    1,000,618.89 |
|               2 |        1,012.00 |          999.71 |    1,000,293.87 |
|               3 |        1,010.00 |        1,000.02 |      999,979.90 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      646,464.00 |      639,034.77 |        1,564.86 |
|               2 |      646,464.00 |      638,610.75 |        1,565.90 |
|               3 |      646,464.00 |      640,076.23 |        1,562.31 |


