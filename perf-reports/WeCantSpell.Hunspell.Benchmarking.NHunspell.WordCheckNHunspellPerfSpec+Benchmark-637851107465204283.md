# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_04/09/2022 14:19:06_
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
|    Elapsed Time |              ms |        1,132.00 |        1,047.67 |        1,003.00 |           73.08 |
|[Counter] _wordsChecked |      operations |    1,309,504.00 |    1,309,504.00 |    1,309,504.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.71 |        1,000.25 |          999.92 |            0.41 |
|[Counter] _wordsChecked |      operations |    1,305,741.38 |    1,254,122.54 |    1,157,621.20 |       83,640.46 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,002,881,600.00 |
|               2 |            0.00 |            0.00 |1,008,082,300.00 |
|               3 |            0.00 |            0.00 |1,131,202,500.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,002,881,600.00 |
|               2 |            0.00 |            0.00 |1,008,082,300.00 |
|               3 |            0.00 |            0.00 |1,131,202,500.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,002,881,600.00 |
|               2 |            0.00 |            0.00 |1,008,082,300.00 |
|               3 |            0.00 |            0.00 |1,131,202,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,002,881,600.00 |
|               2 |            0.00 |            0.00 |1,008,082,300.00 |
|               3 |            0.00 |            0.00 |1,131,202,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,003.00 |        1,000.12 |      999,881.95 |
|               2 |        1,008.00 |          999.92 |    1,000,081.65 |
|               3 |        1,132.00 |        1,000.71 |      999,295.49 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,309,504.00 |    1,305,741.38 |          765.85 |
|               2 |    1,309,504.00 |    1,299,005.05 |          769.82 |
|               3 |    1,309,504.00 |    1,157,621.20 |          863.84 |


