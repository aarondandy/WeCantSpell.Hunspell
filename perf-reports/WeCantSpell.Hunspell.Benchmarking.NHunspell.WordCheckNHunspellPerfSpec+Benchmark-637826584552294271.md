# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/12/2022 05:07:35_
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
|    Elapsed Time |              ms |        1,056.00 |        1,018.67 |          998.00 |           32.39 |
|[Counter] _wordsChecked |      operations |    1,276,352.00 |    1,276,352.00 |    1,276,352.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.58 |        1,000.28 |          999.74 |            0.47 |
|[Counter] _wordsChecked |      operations |    1,279,657.23 |    1,254,162.27 |    1,208,350.16 |       39,758.82 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,056,276,600.00 |
|               2 |            0.00 |            0.00 |  997,417,100.00 |
|               3 |            0.00 |            0.00 |1,001,469,300.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,056,276,600.00 |
|               2 |            0.00 |            0.00 |  997,417,100.00 |
|               3 |            0.00 |            0.00 |1,001,469,300.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,056,276,600.00 |
|               2 |            0.00 |            0.00 |  997,417,100.00 |
|               3 |            0.00 |            0.00 |1,001,469,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,056,276,600.00 |
|               2 |            0.00 |            0.00 |  997,417,100.00 |
|               3 |            0.00 |            0.00 |1,001,469,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,056.00 |          999.74 |    1,000,261.93 |
|               2 |          998.00 |        1,000.58 |      999,415.93 |
|               3 |        1,002.00 |        1,000.53 |      999,470.36 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,276,352.00 |    1,208,350.16 |          827.57 |
|               2 |    1,276,352.00 |    1,279,657.23 |          781.46 |
|               3 |    1,276,352.00 |    1,274,479.41 |          784.63 |


