# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_02/21/2022 13:46:43_
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
|TotalBytesAllocated |           bytes |  127,026,984.00 |   78,854,576.00 |   30,682,168.00 |   68,126,072.73 |
|TotalCollections [Gen0] |     collections |          761.00 |          760.50 |          760.00 |            0.71 |
|TotalCollections [Gen1] |     collections |          260.00 |          259.00 |          258.00 |            1.41 |
|TotalCollections [Gen2] |     collections |           78.00 |           77.00 |           76.00 |            1.41 |
|    Elapsed Time |              ms |       12,599.00 |       12,566.00 |       12,533.00 |           46.67 |
|[Counter] FilePairsLoaded |      operations |           50.00 |           50.00 |           50.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   10,082,251.23 |    6,265,197.33 |    2,448,143.42 |    5,398,129.40 |
|TotalCollections [Gen0] |     collections |           60.72 |           60.52 |           60.32 |            0.28 |
|TotalCollections [Gen1] |     collections |           20.75 |           20.61 |           20.48 |            0.19 |
|TotalCollections [Gen2] |     collections |            6.22 |            6.13 |            6.03 |            0.14 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.00 |          999.99 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.99 |            3.98 |            3.97 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  127,026,984.00 |   10,082,251.23 |           99.18 |
|               2 |   30,682,168.00 |    2,448,143.42 |          408.47 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          760.00 |           60.32 |   16,577,723.03 |
|               2 |          761.00 |           60.72 |   16,468,897.63 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          258.00 |           20.48 |   48,833,602.71 |
|               2 |          260.00 |           20.75 |   48,203,196.54 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           76.00 |            6.03 |  165,777,230.26 |
|               2 |           78.00 |            6.22 |  160,677,321.79 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       12,599.00 |          999.99 |    1,000,005.52 |
|               2 |       12,533.00 |        1,000.01 |      999,986.52 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           50.00 |            3.97 |  251,981,390.00 |
|               2 |           50.00 |            3.99 |  250,656,622.00 |


