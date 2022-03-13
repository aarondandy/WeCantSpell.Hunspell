# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/13/2022 22:18:24_
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
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          992.00 |          987.33 |          982.00 |            5.03 |
|[Counter] _wordsChecked |      operations |    1,284,640.00 |    1,284,640.00 |    1,284,640.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.66 |        1,000.27 |        1,000.04 |            0.33 |
|[Counter] _wordsChecked |      operations |    1,309,044.65 |    1,301,501.22 |    1,295,050.39 |        7,060.82 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  981,356,900.00 |
|               2 |            0.00 |            0.00 |  987,874,100.00 |
|               3 |            0.00 |            0.00 |  991,961,400.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  981,356,900.00 |
|               2 |            0.00 |            0.00 |  987,874,100.00 |
|               3 |            0.00 |            0.00 |  991,961,400.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  981,356,900.00 |
|               2 |            0.00 |            0.00 |  987,874,100.00 |
|               3 |            0.00 |            0.00 |  991,961,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  981,356,900.00 |
|               2 |            0.00 |            0.00 |  987,874,100.00 |
|               3 |            0.00 |            0.00 |  991,961,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          982.00 |        1,000.66 |      999,345.11 |
|               2 |          988.00 |        1,000.13 |      999,872.57 |
|               3 |          992.00 |        1,000.04 |      999,961.09 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,284,640.00 |    1,309,044.65 |          763.92 |
|               2 |    1,284,640.00 |    1,300,408.62 |          768.99 |
|               3 |    1,284,640.00 |    1,295,050.39 |          772.17 |


