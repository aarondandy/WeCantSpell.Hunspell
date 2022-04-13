# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_04/13/2022 23:01:43_
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
|TotalBytesAllocated |           bytes |    5,636,520.00 |    5,636,520.00 |    5,636,520.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,015.00 |        1,006.67 |          997.00 |            9.07 |
|[Counter] _wordsChecked |      operations |      580,160.00 |      580,160.00 |      580,160.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,652,245.11 |    5,598,761.94 |    5,554,440.92 |       49,541.64 |
|TotalCollections [Gen0] |     collections |           13.04 |           12.91 |           12.81 |            0.11 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.22 |          999.87 |          999.61 |            0.31 |
|[Counter] _wordsChecked |      operations |      581,778.57 |      576,273.61 |      571,711.70 |        5,099.26 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,636,520.00 |    5,589,599.78 |          178.90 |
|               2 |    5,636,520.00 |    5,554,440.92 |          180.04 |
|               3 |    5,636,520.00 |    5,652,245.11 |          176.92 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |           12.89 |   77,568,784.62 |
|               2 |           13.00 |           12.81 |   78,059,784.62 |
|               3 |           13.00 |           13.04 |   76,709,069.23 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,394,200.00 |
|               2 |            0.00 |            0.00 |1,014,777,200.00 |
|               3 |            0.00 |            0.00 |  997,217,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,394,200.00 |
|               2 |            0.00 |            0.00 |1,014,777,200.00 |
|               3 |            0.00 |            0.00 |  997,217,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,008.00 |          999.61 |    1,000,391.07 |
|               2 |        1,015.00 |        1,000.22 |      999,780.49 |
|               3 |          997.00 |          999.78 |    1,000,218.56 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      580,160.00 |      575,330.56 |        1,738.13 |
|               2 |      580,160.00 |      571,711.70 |        1,749.13 |
|               3 |      580,160.00 |      581,778.57 |        1,718.87 |


