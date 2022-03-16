# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/17/2022 00:56:11_
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
|TotalBytesAllocated |           bytes |    3,931,424.00 |    3,931,424.00 |    3,931,424.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           60.00 |           60.00 |           60.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          990.00 |          981.00 |          973.00 |            8.54 |
|[Counter] _wordsChecked |      operations |      621,600.00 |      621,600.00 |      621,600.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,039,871.94 |    4,006,973.93 |    3,970,019.34 |       35,102.54 |
|TotalCollections [Gen0] |     collections |           61.66 |           61.15 |           60.59 |            0.54 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.84 |          999.80 |          999.72 |            0.07 |
|[Counter] _wordsChecked |      operations |      638,746.77 |      633,545.25 |      627,702.33 |        5,550.09 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,931,424.00 |    4,039,871.94 |          247.53 |
|               2 |    3,931,424.00 |    3,970,019.34 |          251.89 |
|               3 |    3,931,424.00 |    4,011,030.52 |          249.31 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           60.00 |           61.66 |   16,219,260.00 |
|               2 |           60.00 |           60.59 |   16,504,638.33 |
|               3 |           60.00 |           61.21 |   16,335,885.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  973,155,600.00 |
|               2 |            0.00 |            0.00 |  990,278,300.00 |
|               3 |            0.00 |            0.00 |  980,153,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  973,155,600.00 |
|               2 |            0.00 |            0.00 |  990,278,300.00 |
|               3 |            0.00 |            0.00 |  980,153,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          973.00 |          999.84 |    1,000,159.92 |
|               2 |          990.00 |          999.72 |    1,000,281.11 |
|               3 |          980.00 |          999.84 |    1,000,156.22 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      621,600.00 |      638,746.77 |        1,565.57 |
|               2 |      621,600.00 |      627,702.33 |        1,593.11 |
|               3 |      621,600.00 |      634,186.64 |        1,576.82 |


