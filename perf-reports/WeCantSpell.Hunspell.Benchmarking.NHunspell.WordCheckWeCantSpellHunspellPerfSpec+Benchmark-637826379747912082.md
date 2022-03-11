# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/11/2022 23:26:14_
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
|TotalBytesAllocated |           bytes |      516,576.00 |      516,576.00 |      516,576.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           67.00 |           67.00 |           67.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          995.00 |          989.33 |          982.00 |            6.66 |
|[Counter] _wordsChecked |      operations |      488,992.00 |      488,992.00 |      488,992.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      525,824.52 |      522,073.64 |      519,034.09 |        3,450.65 |
|TotalCollections [Gen0] |     collections |           68.20 |           67.71 |           67.32 |            0.45 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.18 |          999.83 |          999.58 |            0.31 |
|[Counter] _wordsChecked |      operations |      497,746.67 |      494,196.08 |      491,318.84 |        3,266.39 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      516,576.00 |      525,824.52 |        1,901.78 |
|               2 |      516,576.00 |      521,362.31 |        1,918.05 |
|               3 |      516,576.00 |      519,034.09 |        1,926.66 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           67.00 |           68.20 |   14,662,856.72 |
|               2 |           67.00 |           67.62 |   14,788,352.24 |
|               3 |           67.00 |           67.32 |   14,854,688.06 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  982,411,400.00 |
|               2 |            0.00 |            0.00 |  990,819,600.00 |
|               3 |            0.00 |            0.00 |  995,264,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  982,411,400.00 |
|               2 |            0.00 |            0.00 |  990,819,600.00 |
|               3 |            0.00 |            0.00 |  995,264,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          982.00 |          999.58 |    1,000,418.94 |
|               2 |          991.00 |        1,000.18 |      999,817.96 |
|               3 |          995.00 |          999.73 |    1,000,265.43 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      488,992.00 |      497,746.67 |        2,009.05 |
|               2 |      488,992.00 |      493,522.74 |        2,026.25 |
|               3 |      488,992.00 |      491,318.84 |        2,035.34 |


