# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_02/26/2022 05:00:23_
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
|TotalBytesAllocated |           bytes |    4,099,016.00 |    4,099,016.00 |    4,099,016.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           82.00 |           82.00 |           82.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,016.00 |        1,015.67 |        1,015.00 |            0.58 |
|[Counter] _wordsChecked |      operations |      712,768.00 |      712,768.00 |      712,768.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,038,540.47 |    4,035,427.63 |    4,032,588.78 |        2,985.29 |
|TotalCollections [Gen0] |     collections |           80.79 |           80.73 |           80.67 |            0.06 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.17 |          999.91 |          999.54 |            0.33 |
|[Counter] _wordsChecked |      operations |      702,252.06 |      701,710.77 |      701,217.13 |          519.11 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,099,016.00 |    4,038,540.47 |          247.61 |
|               2 |    4,099,016.00 |    4,032,588.78 |          247.98 |
|               3 |    4,099,016.00 |    4,035,153.64 |          247.82 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           82.00 |           80.79 |   12,377,739.02 |
|               2 |           82.00 |           80.67 |   12,396,007.32 |
|               3 |           82.00 |           80.72 |   12,388,128.05 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,014,974,600.00 |
|               2 |            0.00 |            0.00 |1,016,472,600.00 |
|               3 |            0.00 |            0.00 |1,015,826,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,014,974,600.00 |
|               2 |            0.00 |            0.00 |1,016,472,600.00 |
|               3 |            0.00 |            0.00 |1,015,826,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,015.00 |        1,000.03 |      999,974.98 |
|               2 |        1,016.00 |          999.54 |    1,000,465.16 |
|               3 |        1,016.00 |        1,000.17 |      999,829.23 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      712,768.00 |      702,252.06 |        1,423.99 |
|               2 |      712,768.00 |      701,217.13 |        1,426.09 |
|               3 |      712,768.00 |      701,663.13 |        1,425.19 |


