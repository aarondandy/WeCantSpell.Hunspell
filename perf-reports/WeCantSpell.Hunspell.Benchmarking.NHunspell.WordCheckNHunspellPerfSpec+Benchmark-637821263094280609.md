# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/06/2022 01:18:29_
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
|    Elapsed Time |              ms |        1,055.00 |        1,019.67 |          997.00 |           31.01 |
|[Counter] _wordsChecked |      operations |    1,301,216.00 |    1,301,216.00 |    1,301,216.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.36 |          999.88 |          999.25 |            0.57 |
|[Counter] _wordsChecked |      operations |    1,304,149.55 |    1,276,728.85 |    1,233,830.12 |       37,628.21 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  997,750,600.00 |
|               2 |            0.00 |            0.00 |1,006,971,900.00 |
|               3 |            0.00 |            0.00 |1,054,615,200.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  997,750,600.00 |
|               2 |            0.00 |            0.00 |1,006,971,900.00 |
|               3 |            0.00 |            0.00 |1,054,615,200.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  997,750,600.00 |
|               2 |            0.00 |            0.00 |1,006,971,900.00 |
|               3 |            0.00 |            0.00 |1,054,615,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  997,750,600.00 |
|               2 |            0.00 |            0.00 |1,006,971,900.00 |
|               3 |            0.00 |            0.00 |1,054,615,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          997.00 |          999.25 |    1,000,752.86 |
|               2 |        1,007.00 |        1,000.03 |      999,972.10 |
|               3 |        1,055.00 |        1,000.36 |      999,635.26 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,301,216.00 |    1,304,149.55 |          766.78 |
|               2 |    1,301,216.00 |    1,292,206.86 |          773.87 |
|               3 |    1,301,216.00 |    1,233,830.12 |          810.48 |


