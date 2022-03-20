# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/20/2022 20:29:25_
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
|TotalBytesAllocated |           bytes |    1,622,160.00 |    1,622,160.00 |    1,622,160.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           63.00 |           63.00 |           63.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,042.00 |        1,022.00 |        1,011.00 |           17.35 |
|[Counter] _wordsChecked |      operations |      621,600.00 |      621,600.00 |      621,600.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,604,433.42 |    1,587,490.89 |    1,556,022.36 |       27,279.32 |
|TotalCollections [Gen0] |     collections |           62.31 |           61.65 |           60.43 |            1.06 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.42 |          999.96 |          999.52 |            0.45 |
|[Counter] _wordsChecked |      operations |      614,807.30 |      608,315.05 |      596,256.53 |       10,453.24 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,622,160.00 |    1,602,016.88 |          624.21 |
|               2 |    1,622,160.00 |    1,604,433.42 |          623.27 |
|               3 |    1,622,160.00 |    1,556,022.36 |          642.66 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           63.00 |           62.22 |   16,072,596.83 |
|               2 |           63.00 |           62.31 |   16,048,388.89 |
|               3 |           63.00 |           60.43 |   16,547,687.30 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,012,573,600.00 |
|               2 |            0.00 |            0.00 |1,011,048,500.00 |
|               3 |            0.00 |            0.00 |1,042,504,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,012,573,600.00 |
|               2 |            0.00 |            0.00 |1,011,048,500.00 |
|               3 |            0.00 |            0.00 |1,042,504,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,013.00 |        1,000.42 |      999,579.07 |
|               2 |        1,011.00 |          999.95 |    1,000,047.97 |
|               3 |        1,042.00 |          999.52 |    1,000,483.97 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      621,600.00 |      613,881.30 |        1,628.98 |
|               2 |      621,600.00 |      614,807.30 |        1,626.53 |
|               3 |      621,600.00 |      596,256.53 |        1,677.13 |


