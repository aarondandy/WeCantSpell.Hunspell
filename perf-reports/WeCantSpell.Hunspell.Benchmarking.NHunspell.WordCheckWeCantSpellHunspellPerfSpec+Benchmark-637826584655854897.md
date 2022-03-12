# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/12/2022 05:07:45_
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
|TotalBytesAllocated |           bytes |    2,875,224.00 |    2,875,224.00 |    2,875,224.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           73.00 |           73.00 |           73.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,048.00 |        1,016.00 |          995.00 |           28.16 |
|[Counter] _wordsChecked |      operations |      629,888.00 |      629,888.00 |      629,888.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,890,583.11 |    2,832,320.35 |    2,744,066.67 |       77,726.17 |
|TotalCollections [Gen0] |     collections |           73.39 |           71.91 |           69.67 |            1.97 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.49 |        1,000.33 |        1,000.19 |            0.15 |
|[Counter] _wordsChecked |      operations |      633,252.79 |      620,488.91 |      601,154.79 |       17,027.82 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,875,224.00 |    2,862,311.26 |          349.37 |
|               2 |    2,875,224.00 |    2,744,066.67 |          364.42 |
|               3 |    2,875,224.00 |    2,890,583.11 |          345.95 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           73.00 |           72.67 |   13,760,428.77 |
|               2 |           73.00 |           69.67 |   14,353,379.45 |
|               3 |           73.00 |           73.39 |   13,625,842.47 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,004,511,300.00 |
|               2 |            0.00 |            0.00 |1,047,796,700.00 |
|               3 |            0.00 |            0.00 |  994,686,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,004,511,300.00 |
|               2 |            0.00 |            0.00 |1,047,796,700.00 |
|               3 |            0.00 |            0.00 |  994,686,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,005.00 |        1,000.49 |      999,513.73 |
|               2 |        1,048.00 |        1,000.19 |      999,806.01 |
|               3 |          995.00 |        1,000.32 |      999,684.92 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      629,888.00 |      627,059.15 |        1,594.75 |
|               2 |      629,888.00 |      601,154.79 |        1,663.47 |
|               3 |      629,888.00 |      633,252.79 |        1,579.15 |


