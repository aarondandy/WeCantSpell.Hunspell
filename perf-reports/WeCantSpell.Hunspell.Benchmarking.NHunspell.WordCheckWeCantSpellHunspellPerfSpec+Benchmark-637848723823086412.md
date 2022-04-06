# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_04/06/2022 20:06:22_
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
|TotalBytesAllocated |           bytes |    5,841,608.00 |    5,841,608.00 |    5,841,608.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           15.00 |           15.00 |           15.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,068.00 |        1,032.00 |        1,008.00 |           31.75 |
|[Counter] _wordsChecked |      operations |      663,040.00 |      663,040.00 |      663,040.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,791,793.94 |    5,661,433.50 |    5,469,938.28 |      169,413.29 |
|TotalCollections [Gen0] |     collections |           14.87 |           14.54 |           14.05 |            0.44 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.05 |          999.56 |          999.21 |            0.44 |
|[Counter] _wordsChecked |      operations |      657,385.95 |      642,589.65 |      620,854.37 |       19,228.92 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,841,608.00 |    5,791,793.94 |          172.66 |
|               2 |    5,841,608.00 |    5,469,938.28 |          182.82 |
|               3 |    5,841,608.00 |    5,722,568.28 |          174.75 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           15.00 |           14.87 |   67,240,053.33 |
|               2 |           15.00 |           14.05 |   71,196,513.33 |
|               3 |           15.00 |           14.69 |   68,053,453.33 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,600,800.00 |
|               2 |            0.00 |            0.00 |1,067,947,700.00 |
|               3 |            0.00 |            0.00 |1,020,801,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,600,800.00 |
|               2 |            0.00 |            0.00 |1,067,947,700.00 |
|               3 |            0.00 |            0.00 |1,020,801,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,008.00 |          999.40 |    1,000,596.03 |
|               2 |        1,068.00 |        1,000.05 |      999,951.03 |
|               3 |        1,020.00 |          999.21 |    1,000,786.08 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      663,040.00 |      657,385.95 |        1,521.18 |
|               2 |      663,040.00 |      620,854.37 |        1,610.68 |
|               3 |      663,040.00 |      649,528.64 |        1,539.58 |


