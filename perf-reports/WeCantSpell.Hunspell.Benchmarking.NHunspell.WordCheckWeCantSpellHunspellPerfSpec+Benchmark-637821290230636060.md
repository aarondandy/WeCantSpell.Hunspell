# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/06/2022 02:03:43_
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
|    Elapsed Time |              ms |        1,010.00 |        1,004.67 |        1,002.00 |            4.62 |
|[Counter] _wordsChecked |      operations |      654,752.00 |      654,752.00 |      654,752.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,635,888.38 |    5,621,974.80 |    5,595,126.39 |       23,256.55 |
|TotalCollections [Gen0] |     collections |           74.87 |           74.68 |           74.33 |            0.31 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.93 |        1,000.41 |        1,000.06 |            0.46 |
|[Counter] _wordsChecked |      operations |      653,600.68 |      651,987.11 |      648,873.47 |        2,697.09 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,645,816.00 |    5,635,888.38 |          177.43 |
|               2 |    5,645,816.00 |    5,595,126.39 |          178.73 |
|               3 |    5,645,816.00 |    5,634,909.63 |          177.47 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           75.00 |           74.87 |   13,356,820.00 |
|               2 |           75.00 |           74.33 |   13,454,128.00 |
|               3 |           75.00 |           74.86 |   13,359,140.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,761,500.00 |
|               2 |            0.00 |            0.00 |1,009,059,600.00 |
|               3 |            0.00 |            0.00 |1,001,935,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,761,500.00 |
|               2 |            0.00 |            0.00 |1,009,059,600.00 |
|               3 |            0.00 |            0.00 |1,001,935,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,002.00 |        1,000.24 |      999,761.98 |
|               2 |        1,010.00 |        1,000.93 |      999,068.91 |
|               3 |        1,002.00 |        1,000.06 |      999,935.63 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      654,752.00 |      653,600.68 |        1,529.99 |
|               2 |      654,752.00 |      648,873.47 |        1,541.13 |
|               3 |      654,752.00 |      653,487.18 |        1,530.25 |


