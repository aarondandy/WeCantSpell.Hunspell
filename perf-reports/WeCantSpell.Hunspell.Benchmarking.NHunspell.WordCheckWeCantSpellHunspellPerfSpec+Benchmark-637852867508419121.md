# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_04/11/2022 15:12:30_
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
|TotalBytesAllocated |           bytes |    3,285,704.00 |    3,285,704.00 |    3,285,704.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           15.00 |           15.00 |           15.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,003.00 |        1,002.00 |        1,000.00 |            1.73 |
|[Counter] _wordsChecked |      operations |      646,464.00 |      646,464.00 |      646,464.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,284,558.35 |    3,279,503.92 |    3,276,015.19 |        4,481.62 |
|TotalCollections [Gen0] |     collections |           14.99 |           14.97 |           14.96 |            0.02 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.63 |        1,000.11 |          999.65 |            0.49 |
|[Counter] _wordsChecked |      operations |      646,238.59 |      645,244.13 |      644,557.72 |          881.76 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,285,704.00 |    3,284,558.35 |          304.45 |
|               2 |    3,285,704.00 |    3,276,015.19 |          305.25 |
|               3 |    3,285,704.00 |    3,277,938.24 |          305.07 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           15.00 |           14.99 |   66,689,920.00 |
|               2 |           15.00 |           14.96 |   66,863,833.33 |
|               3 |           15.00 |           14.96 |   66,824,606.67 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,000,348,800.00 |
|               2 |            0.00 |            0.00 |1,002,957,500.00 |
|               3 |            0.00 |            0.00 |1,002,369,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,000,348,800.00 |
|               2 |            0.00 |            0.00 |1,002,957,500.00 |
|               3 |            0.00 |            0.00 |1,002,369,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,000.00 |          999.65 |    1,000,348.80 |
|               2 |        1,003.00 |        1,000.04 |      999,957.63 |
|               3 |        1,003.00 |        1,000.63 |      999,370.99 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      646,464.00 |      646,238.59 |        1,547.42 |
|               2 |      646,464.00 |      644,557.72 |        1,551.45 |
|               3 |      646,464.00 |      644,936.08 |        1,550.54 |


