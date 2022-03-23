# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/23/2022 00:48:21_
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
|TotalBytesAllocated |           bytes |    4,080,432.00 |    4,080,432.00 |    4,080,432.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           66.00 |           66.00 |           66.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,014.00 |        1,012.33 |        1,009.00 |            2.89 |
|[Counter] _wordsChecked |      operations |      654,752.00 |      654,752.00 |      654,752.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,046,350.80 |    4,032,535.55 |    4,023,605.02 |       12,134.17 |
|TotalCollections [Gen0] |     collections |           65.45 |           65.23 |           65.08 |            0.20 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.88 |        1,000.44 |          999.88 |            0.51 |
|[Counter] _wordsChecked |      operations |      649,283.28 |      647,066.47 |      645,633.46 |        1,947.07 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,080,432.00 |    4,027,650.84 |          248.28 |
|               2 |    4,080,432.00 |    4,046,350.80 |          247.14 |
|               3 |    4,080,432.00 |    4,023,605.02 |          248.53 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           66.00 |           65.15 |   15,350,071.21 |
|               2 |           66.00 |           65.45 |   15,279,131.82 |
|               3 |           66.00 |           65.08 |   15,365,506.06 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,013,104,700.00 |
|               2 |            0.00 |            0.00 |1,008,422,700.00 |
|               3 |            0.00 |            0.00 |1,014,123,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,013,104,700.00 |
|               2 |            0.00 |            0.00 |1,008,422,700.00 |
|               3 |            0.00 |            0.00 |1,014,123,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,014.00 |        1,000.88 |      999,117.06 |
|               2 |        1,009.00 |        1,000.57 |      999,427.85 |
|               3 |        1,014.00 |          999.88 |    1,000,121.70 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      654,752.00 |      646,282.66 |        1,547.31 |
|               2 |      654,752.00 |      649,283.28 |        1,540.16 |
|               3 |      654,752.00 |      645,633.46 |        1,548.87 |


