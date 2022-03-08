# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/08/2022 05:26:08_
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
|TotalBytesAllocated |           bytes |    1,333,936.00 |    1,333,936.00 |    1,333,936.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           67.00 |           67.00 |           67.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.00 |          987.67 |          981.00 |            9.87 |
|[Counter] _wordsChecked |      operations |      580,160.00 |      580,160.00 |      580,160.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,359,263.84 |    1,350,197.55 |    1,335,121.45 |       13,145.42 |
|TotalCollections [Gen0] |     collections |           68.27 |           67.82 |           67.06 |            0.66 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.89 |          999.64 |          999.41 |            0.24 |
|[Counter] _wordsChecked |      operations |      591,175.67 |      587,232.53 |      580,675.58 |        5,717.25 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,333,936.00 |    1,335,121.45 |          749.00 |
|               2 |    1,333,936.00 |    1,356,207.37 |          737.35 |
|               3 |    1,333,936.00 |    1,359,263.84 |          735.69 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           67.00 |           67.06 |   14,912,120.90 |
|               2 |           67.00 |           68.12 |   14,680,271.64 |
|               3 |           67.00 |           68.27 |   14,647,261.19 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  999,112,100.00 |
|               2 |            0.00 |            0.00 |  983,578,200.00 |
|               3 |            0.00 |            0.00 |  981,366,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  999,112,100.00 |
|               2 |            0.00 |            0.00 |  983,578,200.00 |
|               3 |            0.00 |            0.00 |  981,366,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          999.00 |          999.89 |    1,000,112.21 |
|               2 |          983.00 |          999.41 |    1,000,588.20 |
|               3 |          981.00 |          999.63 |    1,000,373.60 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      580,160.00 |      580,675.58 |        1,722.13 |
|               2 |      580,160.00 |      589,846.34 |        1,695.36 |
|               3 |      580,160.00 |      591,175.67 |        1,691.54 |


