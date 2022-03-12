# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/12/2022 05:07:24_
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
|TotalBytesAllocated |           bytes |   30,696,048.00 |   30,693,188.00 |   30,690,328.00 |        4,044.65 |
|TotalCollections [Gen0] |     collections |          498.00 |          498.00 |          498.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          205.00 |          205.00 |          205.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           62.00 |           62.00 |           62.00 |            0.00 |
|    Elapsed Time |              ms |       16,271.00 |       16,070.00 |       15,869.00 |          284.26 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,934,385.09 |    1,910,295.10 |    1,886,205.10 |       34,068.39 |
|TotalCollections [Gen0] |     collections |           31.38 |           30.99 |           30.61 |            0.55 |
|TotalCollections [Gen1] |     collections |           12.92 |           12.76 |           12.60 |            0.23 |
|TotalCollections [Gen2] |     collections |            3.91 |            3.86 |            3.81 |            0.07 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.01 |        1,000.00 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.72 |            3.67 |            3.63 |            0.07 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,690,328.00 |    1,886,205.10 |          530.17 |
|               2 |   30,696,048.00 |    1,934,385.09 |          516.96 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          498.00 |           30.61 |   32,672,568.07 |
|               2 |          498.00 |           31.38 |   31,864,725.90 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          205.00 |           12.60 |   79,370,433.66 |
|               2 |          205.00 |           12.92 |   77,407,968.29 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           62.00 |            3.81 |  262,434,498.39 |
|               2 |           62.00 |            3.91 |  255,945,701.61 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       16,271.00 |        1,000.00 |      999,996.24 |
|               2 |       15,869.00 |        1,000.02 |      999,976.90 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.63 |  275,778,625.42 |
|               2 |           59.00 |            3.72 |  268,959,889.83 |


