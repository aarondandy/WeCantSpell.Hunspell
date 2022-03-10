# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/10/2022 01:22:22_
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
|TotalBytesAllocated |           bytes |      525,904.00 |      525,904.00 |      525,904.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           80.00 |           80.00 |           80.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,017.00 |        1,011.00 |        1,007.00 |            5.29 |
|[Counter] _wordsChecked |      operations |      679,616.00 |      679,616.00 |      679,616.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      522,536.15 |      520,181.39 |      517,002.56 |        2,857.35 |
|TotalCollections [Gen0] |     collections |           79.49 |           79.13 |           78.65 |            0.43 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.55 |          999.98 |          999.60 |            0.50 |
|[Counter] _wordsChecked |      operations |      675,263.79 |      672,220.77 |      668,112.83 |        3,692.50 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      525,904.00 |      522,536.15 |        1,913.74 |
|               2 |      525,904.00 |      521,005.45 |        1,919.37 |
|               3 |      525,904.00 |      517,002.56 |        1,934.23 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           80.00 |           79.49 |   12,580,565.00 |
|               2 |           80.00 |           79.25 |   12,617,526.25 |
|               3 |           80.00 |           78.65 |   12,715,217.50 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,445,200.00 |
|               2 |            0.00 |            0.00 |1,009,402,100.00 |
|               3 |            0.00 |            0.00 |1,017,217,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,445,200.00 |
|               2 |            0.00 |            0.00 |1,009,402,100.00 |
|               3 |            0.00 |            0.00 |1,017,217,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,007.00 |        1,000.55 |      999,449.06 |
|               2 |        1,009.00 |          999.60 |    1,000,398.51 |
|               3 |        1,017.00 |          999.79 |    1,000,213.77 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      679,616.00 |      675,263.79 |        1,480.90 |
|               2 |      679,616.00 |      673,285.70 |        1,485.25 |
|               3 |      679,616.00 |      668,112.83 |        1,496.75 |


