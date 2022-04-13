# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_04/13/2022 23:10:16_
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
|TotalBytesAllocated |           bytes |    3,138,416.00 |    3,138,416.00 |    3,138,416.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          996.00 |          989.33 |          985.00 |            5.86 |
|[Counter] _wordsChecked |      operations |      563,584.00 |      563,584.00 |      563,584.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,184,822.04 |    3,171,615.96 |    3,151,691.24 |       17,557.55 |
|TotalCollections [Gen0] |     collections |           13.19 |           13.14 |           13.05 |            0.07 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.21 |          999.78 |          999.55 |            0.38 |
|[Counter] _wordsChecked |      operations |      571,917.41 |      569,545.91 |      565,967.91 |        3,152.91 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,138,416.00 |    3,178,334.61 |          314.63 |
|               2 |    3,138,416.00 |    3,151,691.24 |          317.29 |
|               3 |    3,138,416.00 |    3,184,822.04 |          313.99 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |           13.17 |   75,956,953.85 |
|               2 |           13.00 |           13.05 |   76,599,069.23 |
|               3 |           13.00 |           13.19 |   75,802,230.77 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  987,440,400.00 |
|               2 |            0.00 |            0.00 |  995,787,900.00 |
|               3 |            0.00 |            0.00 |  985,429,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  987,440,400.00 |
|               2 |            0.00 |            0.00 |  995,787,900.00 |
|               3 |            0.00 |            0.00 |  985,429,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          987.00 |          999.55 |    1,000,446.20 |
|               2 |          996.00 |        1,000.21 |      999,787.05 |
|               3 |          985.00 |          999.56 |    1,000,435.53 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      563,584.00 |      570,752.42 |        1,752.07 |
|               2 |      563,584.00 |      565,967.91 |        1,766.88 |
|               3 |      563,584.00 |      571,917.41 |        1,748.50 |


