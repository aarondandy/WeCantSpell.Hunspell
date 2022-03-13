# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/13/2022 00:19:00_
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
|TotalBytesAllocated |           bytes |    2,875,224.00 |    2,875,224.00 |    2,875,224.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           73.00 |           73.00 |           73.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          996.00 |          982.33 |          971.00 |           12.66 |
|[Counter] _wordsChecked |      operations |      629,888.00 |      629,888.00 |      629,888.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,959,883.78 |    2,926,410.71 |    2,885,720.81 |       37,604.50 |
|TotalCollections [Gen0] |     collections |           75.15 |           74.30 |           73.27 |            0.95 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.91 |          999.71 |          999.59 |            0.17 |
|[Counter] _wordsChecked |      operations |      648,434.79 |      641,101.70 |      632,187.58 |        8,238.18 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,875,224.00 |    2,933,627.54 |          340.87 |
|               2 |    2,875,224.00 |    2,959,883.78 |          337.85 |
|               3 |    2,875,224.00 |    2,885,720.81 |          346.53 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           73.00 |           74.48 |   13,425,913.70 |
|               2 |           73.00 |           75.15 |   13,306,816.44 |
|               3 |           73.00 |           73.27 |   13,648,801.37 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  980,091,700.00 |
|               2 |            0.00 |            0.00 |  971,397,600.00 |
|               3 |            0.00 |            0.00 |  996,362,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  980,091,700.00 |
|               2 |            0.00 |            0.00 |  971,397,600.00 |
|               3 |            0.00 |            0.00 |  996,362,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          980.00 |          999.91 |    1,000,093.57 |
|               2 |          971.00 |          999.59 |    1,000,409.47 |
|               3 |          996.00 |          999.64 |    1,000,363.96 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      629,888.00 |      642,682.72 |        1,555.98 |
|               2 |      629,888.00 |      648,434.79 |        1,542.18 |
|               3 |      629,888.00 |      632,187.58 |        1,581.81 |


