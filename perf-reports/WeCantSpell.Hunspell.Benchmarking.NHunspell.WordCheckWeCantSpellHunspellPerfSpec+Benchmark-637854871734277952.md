# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_04/13/2022 22:52:53_
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
|TotalBytesAllocated |           bytes |      114,576.00 |      114,576.00 |      114,576.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           59.00 |           59.00 |           59.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,082.00 |        1,077.00 |        1,073.00 |            4.58 |
|[Counter] _wordsChecked |      operations |      580,160.00 |      580,160.00 |      580,160.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      106,782.13 |      106,371.17 |      105,893.52 |          448.05 |
|TotalCollections [Gen0] |     collections |           54.99 |           54.77 |           54.53 |            0.23 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.01 |          999.86 |          999.57 |            0.25 |
|[Counter] _wordsChecked |      operations |      540,695.45 |      538,614.55 |      536,195.90 |        2,268.71 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      114,576.00 |      106,782.13 |        9,364.86 |
|               2 |      114,576.00 |      106,437.88 |        9,395.15 |
|               3 |      114,576.00 |      105,893.52 |        9,443.45 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           54.99 |   18,186,245.76 |
|               2 |           59.00 |           54.81 |   18,245,066.10 |
|               3 |           59.00 |           54.53 |   18,338,857.63 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,072,988,500.00 |
|               2 |            0.00 |            0.00 |1,076,458,900.00 |
|               3 |            0.00 |            0.00 |1,081,992,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,072,988,500.00 |
|               2 |            0.00 |            0.00 |1,076,458,900.00 |
|               3 |            0.00 |            0.00 |1,081,992,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,073.00 |        1,000.01 |      999,989.28 |
|               2 |        1,076.00 |          999.57 |    1,000,426.49 |
|               3 |        1,082.00 |        1,000.01 |      999,993.16 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      580,160.00 |      540,695.45 |        1,849.47 |
|               2 |      580,160.00 |      538,952.30 |        1,855.45 |
|               3 |      580,160.00 |      536,195.90 |        1,864.99 |


