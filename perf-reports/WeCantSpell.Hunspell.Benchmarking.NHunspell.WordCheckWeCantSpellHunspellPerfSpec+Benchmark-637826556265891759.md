# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/12/2022 04:20:26_
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
|TotalBytesAllocated |           bytes |    4,841,520.00 |    4,841,520.00 |    4,841,520.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           83.00 |           83.00 |           83.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,110.00 |        1,042.67 |        1,006.00 |           58.39 |
|[Counter] _wordsChecked |      operations |      638,176.00 |      638,176.00 |      638,176.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,815,372.05 |    4,653,678.43 |    4,364,050.15 |      251,392.76 |
|TotalCollections [Gen0] |     collections |           82.55 |           79.78 |           74.81 |            4.31 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.57 |        1,000.19 |          999.48 |            0.62 |
|[Counter] _wordsChecked |      operations |      634,729.36 |      613,416.01 |      575,239.19 |       33,136.87 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,841,520.00 |    4,364,050.15 |          229.14 |
|               2 |    4,841,520.00 |    4,781,613.08 |          209.13 |
|               3 |    4,841,520.00 |    4,815,372.05 |          207.67 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           83.00 |           74.81 |   13,366,383.13 |
|               2 |           83.00 |           81.97 |   12,199,139.76 |
|               3 |           83.00 |           82.55 |   12,113,615.66 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,109,409,800.00 |
|               2 |            0.00 |            0.00 |1,012,528,600.00 |
|               3 |            0.00 |            0.00 |1,005,430,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,109,409,800.00 |
|               2 |            0.00 |            0.00 |1,012,528,600.00 |
|               3 |            0.00 |            0.00 |1,005,430,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,110.00 |        1,000.53 |      999,468.29 |
|               2 |        1,012.00 |          999.48 |    1,000,522.33 |
|               3 |        1,006.00 |        1,000.57 |      999,433.50 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      638,176.00 |      575,239.19 |        1,738.41 |
|               2 |      638,176.00 |      630,279.48 |        1,586.60 |
|               3 |      638,176.00 |      634,729.36 |        1,575.47 |


