# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/11/2022 01:49:48_
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
NumberOfIterations=2, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   30,698,152.00 |   30,695,092.00 |   30,692,032.00 |        4,327.49 |
|TotalCollections [Gen0] |     collections |          505.00 |          505.00 |          505.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          212.00 |          212.00 |          212.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           69.00 |           69.00 |           69.00 |            0.00 |
|    Elapsed Time |              ms |       15,049.00 |       15,047.00 |       15,045.00 |            2.83 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,040,358.46 |    2,039,933.47 |    2,039,508.48 |          601.03 |
|TotalCollections [Gen0] |     collections |           33.56 |           33.56 |           33.56 |            0.01 |
|TotalCollections [Gen1] |     collections |           14.09 |           14.09 |           14.09 |            0.00 |
|TotalCollections [Gen2] |     collections |            4.59 |            4.59 |            4.59 |            0.00 |
|    Elapsed Time |              ms |        1,000.02 |          999.99 |          999.97 |            0.03 |
|[Counter] FilePairsLoaded |      operations |            3.92 |            3.92 |            3.92 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,698,152.00 |    2,040,358.46 |          490.11 |
|               2 |   30,692,032.00 |    2,039,508.48 |          490.31 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          505.00 |           33.56 |   29,793,009.90 |
|               2 |          505.00 |           33.56 |   29,799,484.36 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          212.00 |           14.09 |   70,969,198.11 |
|               2 |          212.00 |           14.09 |   70,984,620.75 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           69.00 |            4.59 |  218,050,289.86 |
|               2 |           69.00 |            4.59 |  218,097,675.36 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,045.00 |          999.97 |    1,000,031.24 |
|               2 |       15,049.00 |        1,000.02 |      999,982.70 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.92 |  255,007,966.10 |
|               2 |           59.00 |            3.92 |  255,063,383.05 |


