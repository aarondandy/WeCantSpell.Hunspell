# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/06/2022 23:02:57_
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
|TotalBytesAllocated |           bytes |    5,636,896.00 |    5,636,896.00 |    5,636,896.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           75.00 |           75.00 |           75.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,001.00 |          999.33 |          996.00 |            2.89 |
|[Counter] _wordsChecked |      operations |      654,752.00 |      654,752.00 |      654,752.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,658,205.37 |    5,640,341.99 |    5,628,204.36 |       15,798.84 |
|TotalCollections [Gen0] |     collections |           75.28 |           75.05 |           74.88 |            0.21 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.60 |          999.94 |          999.46 |            0.59 |
|[Counter] _wordsChecked |      operations |      657,227.18 |      655,152.27 |      653,742.43 |        1,835.11 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,636,896.00 |    5,628,204.36 |          177.68 |
|               2 |    5,636,896.00 |    5,658,205.37 |          176.73 |
|               3 |    5,636,896.00 |    5,634,616.23 |          177.47 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           75.00 |           74.88 |   13,353,924.00 |
|               2 |           75.00 |           75.28 |   13,283,118.67 |
|               3 |           75.00 |           74.97 |   13,338,728.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,544,300.00 |
|               2 |            0.00 |            0.00 |  996,233,900.00 |
|               3 |            0.00 |            0.00 |1,000,404,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,544,300.00 |
|               2 |            0.00 |            0.00 |  996,233,900.00 |
|               3 |            0.00 |            0.00 |1,000,404,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,001.00 |          999.46 |    1,000,543.76 |
|               2 |          996.00 |          999.77 |    1,000,234.84 |
|               3 |        1,001.00 |        1,000.60 |      999,405.19 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      654,752.00 |      653,742.43 |        1,529.65 |
|               2 |      654,752.00 |      657,227.18 |        1,521.54 |
|               3 |      654,752.00 |      654,487.19 |        1,527.91 |


