# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/12/2022 03:36:03_
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
|TotalBytesAllocated |           bytes |    2,308,768.00 |    2,308,768.00 |    2,308,768.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           69.00 |           69.00 |           69.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,001.00 |          992.67 |          987.00 |            7.37 |
|[Counter] _wordsChecked |      operations |      605,024.00 |      605,024.00 |      605,024.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,338,917.58 |    2,326,153.63 |    2,306,389.88 |       17,356.86 |
|TotalCollections [Gen0] |     collections |           69.90 |           69.52 |           68.93 |            0.52 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.46 |        1,000.10 |          999.89 |            0.31 |
|[Counter] _wordsChecked |      operations |      612,924.85 |      609,579.99 |      604,400.80 |        4,548.45 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,308,768.00 |    2,333,153.42 |          428.60 |
|               2 |    2,308,768.00 |    2,306,389.88 |          433.58 |
|               3 |    2,308,768.00 |    2,338,917.58 |          427.55 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           69.00 |           69.73 |   14,341,279.71 |
|               2 |           69.00 |           68.93 |   14,507,697.10 |
|               3 |           69.00 |           69.90 |   14,305,936.23 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  989,548,300.00 |
|               2 |            0.00 |            0.00 |1,001,031,100.00 |
|               3 |            0.00 |            0.00 |  987,109,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  989,548,300.00 |
|               2 |            0.00 |            0.00 |1,001,031,100.00 |
|               3 |            0.00 |            0.00 |  987,109,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          990.00 |        1,000.46 |      999,543.74 |
|               2 |        1,001.00 |          999.97 |    1,000,031.07 |
|               3 |          987.00 |          999.89 |    1,000,111.04 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      605,024.00 |      611,414.32 |        1,635.55 |
|               2 |      605,024.00 |      604,400.80 |        1,654.53 |
|               3 |      605,024.00 |      612,924.85 |        1,631.52 |


