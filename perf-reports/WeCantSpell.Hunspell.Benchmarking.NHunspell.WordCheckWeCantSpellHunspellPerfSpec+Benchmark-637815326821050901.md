# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_02/27/2022 04:24:42_
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
|TotalBytesAllocated |           bytes |    5,424,800.00 |    5,424,800.00 |    5,424,800.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           76.00 |           76.00 |           76.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,013.00 |        1,010.00 |        1,008.00 |            2.65 |
|[Counter] _wordsChecked |      operations |      663,040.00 |      663,040.00 |      663,040.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,381,145.46 |    5,372,298.23 |    5,357,098.60 |       13,222.24 |
|TotalCollections [Gen0] |     collections |           75.39 |           75.26 |           75.05 |            0.19 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.88 |        1,000.22 |          999.42 |            0.74 |
|[Counter] _wordsChecked |      operations |      657,704.37 |      656,623.03 |      654,765.27 |        1,616.07 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,424,800.00 |    5,381,145.46 |          185.83 |
|               2 |    5,424,800.00 |    5,378,650.64 |          185.92 |
|               3 |    5,424,800.00 |    5,357,098.60 |          186.67 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           76.00 |           75.39 |   13,264,638.16 |
|               2 |           76.00 |           75.35 |   13,270,790.79 |
|               3 |           76.00 |           75.05 |   13,324,180.26 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,112,500.00 |
|               2 |            0.00 |            0.00 |1,008,580,100.00 |
|               3 |            0.00 |            0.00 |1,012,637,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,112,500.00 |
|               2 |            0.00 |            0.00 |1,008,580,100.00 |
|               3 |            0.00 |            0.00 |1,012,637,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,009.00 |        1,000.88 |      999,120.42 |
|               2 |        1,008.00 |          999.42 |    1,000,575.50 |
|               3 |        1,013.00 |        1,000.36 |      999,642.35 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      663,040.00 |      657,704.37 |        1,520.44 |
|               2 |      663,040.00 |      657,399.45 |        1,521.15 |
|               3 |      663,040.00 |      654,765.27 |        1,527.26 |


