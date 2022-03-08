# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/08/2022 04:57:54_
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
|TotalBytesAllocated |           bytes |    4,540,216.00 |    4,540,216.00 |    4,540,216.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           80.00 |           80.00 |           80.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          998.00 |          997.00 |          996.00 |            1.00 |
|[Counter] _wordsChecked |      operations |      696,192.00 |      696,192.00 |      696,192.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,556,379.76 |    4,552,652.56 |    4,550,070.09 |        3,306.93 |
|TotalCollections [Gen0] |     collections |           80.28 |           80.22 |           80.17 |            0.06 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.17 |          999.73 |          999.48 |            0.38 |
|[Counter] _wordsChecked |      operations |      698,670.53 |      698,099.01 |      697,703.02 |          507.08 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,540,216.00 |    4,551,507.84 |          219.71 |
|               2 |    4,540,216.00 |    4,550,070.09 |          219.78 |
|               3 |    4,540,216.00 |    4,556,379.76 |          219.47 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           80.00 |           80.20 |   12,468,988.75 |
|               2 |           80.00 |           80.17 |   12,472,928.75 |
|               3 |           80.00 |           80.28 |   12,455,656.25 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  997,519,100.00 |
|               2 |            0.00 |            0.00 |  997,834,300.00 |
|               3 |            0.00 |            0.00 |  996,452,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  997,519,100.00 |
|               2 |            0.00 |            0.00 |  997,834,300.00 |
|               3 |            0.00 |            0.00 |  996,452,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          997.00 |          999.48 |    1,000,520.66 |
|               2 |          998.00 |        1,000.17 |      999,833.97 |
|               3 |          996.00 |          999.55 |    1,000,454.32 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      696,192.00 |      697,923.48 |        1,432.82 |
|               2 |      696,192.00 |      697,703.02 |        1,433.27 |
|               3 |      696,192.00 |      698,670.53 |        1,431.29 |


