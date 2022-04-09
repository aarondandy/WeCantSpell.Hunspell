# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_04/09/2022 14:45:09_
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
|    Elapsed Time |              ms |        1,007.00 |        1,004.33 |        1,000.00 |            3.79 |
|[Counter] _wordsChecked |      operations |    1,301,216.00 |    1,301,216.00 |    1,301,216.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.59 |        1,000.12 |          999.49 |            0.56 |
|[Counter] _wordsChecked |      operations |    1,300,555.19 |    1,295,768.10 |    1,292,535.46 |        4,229.80 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,000,508,100.00 |
|               2 |            0.00 |            0.00 |1,006,715,900.00 |
|               3 |            0.00 |            0.00 |1,005,410,500.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,000,508,100.00 |
|               2 |            0.00 |            0.00 |1,006,715,900.00 |
|               3 |            0.00 |            0.00 |1,005,410,500.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,000,508,100.00 |
|               2 |            0.00 |            0.00 |1,006,715,900.00 |
|               3 |            0.00 |            0.00 |1,005,410,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,000,508,100.00 |
|               2 |            0.00 |            0.00 |1,006,715,900.00 |
|               3 |            0.00 |            0.00 |1,005,410,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,000.00 |          999.49 |    1,000,508.10 |
|               2 |        1,007.00 |        1,000.28 |      999,717.87 |
|               3 |        1,006.00 |        1,000.59 |      999,414.02 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,301,216.00 |    1,300,555.19 |          768.90 |
|               2 |    1,301,216.00 |    1,292,535.46 |          773.67 |
|               3 |    1,301,216.00 |    1,294,213.66 |          772.67 |


