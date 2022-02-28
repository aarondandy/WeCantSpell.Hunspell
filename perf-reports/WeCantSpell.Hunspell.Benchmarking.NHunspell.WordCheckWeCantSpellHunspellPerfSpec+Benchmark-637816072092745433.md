# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_02/28/2022 01:06:49_
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
|TotalBytesAllocated |           bytes |    5,645,816.00 |    5,645,816.00 |    5,645,816.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           75.00 |           75.00 |           75.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,011.00 |        1,009.33 |        1,007.00 |            2.08 |
|[Counter] _wordsChecked |      operations |      654,752.00 |      654,752.00 |      654,752.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,607,178.61 |    5,593,992.07 |    5,585,397.08 |       11,593.99 |
|TotalCollections [Gen0] |     collections |           74.49 |           74.31 |           74.20 |            0.15 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.18 |        1,000.07 |          999.91 |            0.14 |
|[Counter] _wordsChecked |      operations |      650,271.18 |      648,741.92 |      647,745.15 |        1,344.57 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,645,816.00 |    5,607,178.61 |          178.34 |
|               2 |    5,645,816.00 |    5,589,400.50 |          178.91 |
|               3 |    5,645,816.00 |    5,585,397.08 |          179.04 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           75.00 |           74.49 |   13,425,209.33 |
|               2 |           75.00 |           74.25 |   13,467,910.67 |
|               3 |           75.00 |           74.20 |   13,477,564.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,890,700.00 |
|               2 |            0.00 |            0.00 |1,010,093,300.00 |
|               3 |            0.00 |            0.00 |1,010,817,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,890,700.00 |
|               2 |            0.00 |            0.00 |1,010,093,300.00 |
|               3 |            0.00 |            0.00 |1,010,817,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,007.00 |        1,000.11 |      999,891.46 |
|               2 |        1,010.00 |          999.91 |    1,000,092.38 |
|               3 |        1,011.00 |        1,000.18 |      999,819.29 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      654,752.00 |      650,271.18 |        1,537.82 |
|               2 |      654,752.00 |      648,209.43 |        1,542.71 |
|               3 |      654,752.00 |      647,745.15 |        1,543.82 |


