# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_02/25/2022 03:56:48_
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
|    Elapsed Time |              ms |          996.00 |          991.67 |          987.00 |            4.51 |
|[Counter] _wordsChecked |      operations |    1,292,928.00 |    1,292,928.00 |    1,292,928.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.22 |          999.82 |          999.50 |            0.36 |
|[Counter] _wordsChecked |      operations |    1,309,304.39 |    1,303,578.81 |    1,298,403.37 |        5,471.29 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  992,248,300.00 |
|               2 |            0.00 |            0.00 |  995,783,000.00 |
|               3 |            0.00 |            0.00 |  987,492,300.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  992,248,300.00 |
|               2 |            0.00 |            0.00 |  995,783,000.00 |
|               3 |            0.00 |            0.00 |  987,492,300.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  992,248,300.00 |
|               2 |            0.00 |            0.00 |  995,783,000.00 |
|               3 |            0.00 |            0.00 |  987,492,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  992,248,300.00 |
|               2 |            0.00 |            0.00 |  995,783,000.00 |
|               3 |            0.00 |            0.00 |  987,492,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          992.00 |          999.75 |    1,000,250.30 |
|               2 |          996.00 |        1,000.22 |      999,782.13 |
|               3 |          987.00 |          999.50 |    1,000,498.78 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,292,928.00 |    1,303,028.69 |          767.44 |
|               2 |    1,292,928.00 |    1,298,403.37 |          770.18 |
|               3 |    1,292,928.00 |    1,309,304.39 |          763.76 |


