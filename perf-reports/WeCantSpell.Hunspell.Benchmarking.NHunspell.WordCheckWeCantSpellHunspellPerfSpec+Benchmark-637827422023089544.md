# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/13/2022 04:23:22_
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
|TotalBytesAllocated |           bytes |    2,687,056.00 |    2,687,056.00 |    2,687,056.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           74.00 |           74.00 |           74.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,056.00 |        1,036.00 |        1,016.00 |           20.00 |
|[Counter] _wordsChecked |      operations |      638,176.00 |      638,176.00 |      638,176.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,643,597.37 |    2,594,519.72 |    2,544,443.99 |       49,584.23 |
|TotalCollections [Gen0] |     collections |           72.80 |           71.45 |           70.07 |            1.37 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.71 |        1,000.08 |          999.57 |            0.58 |
|[Counter] _wordsChecked |      operations |      627,854.57 |      616,198.63 |      604,305.64 |       11,776.26 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,687,056.00 |    2,595,517.80 |          385.28 |
|               2 |    2,687,056.00 |    2,643,597.37 |          378.27 |
|               3 |    2,687,056.00 |    2,544,443.99 |          393.01 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           74.00 |           71.48 |   13,990,105.41 |
|               2 |           74.00 |           72.80 |   13,735,664.86 |
|               3 |           74.00 |           70.07 |   14,270,924.32 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,035,267,800.00 |
|               2 |            0.00 |            0.00 |1,016,439,200.00 |
|               3 |            0.00 |            0.00 |1,056,048,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,035,267,800.00 |
|               2 |            0.00 |            0.00 |1,016,439,200.00 |
|               3 |            0.00 |            0.00 |1,056,048,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,036.00 |        1,000.71 |      999,293.24 |
|               2 |        1,016.00 |          999.57 |    1,000,432.28 |
|               3 |        1,056.00 |          999.95 |    1,000,045.83 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      638,176.00 |      616,435.67 |        1,622.23 |
|               2 |      638,176.00 |      627,854.57 |        1,592.73 |
|               3 |      638,176.00 |      604,305.64 |        1,654.79 |


