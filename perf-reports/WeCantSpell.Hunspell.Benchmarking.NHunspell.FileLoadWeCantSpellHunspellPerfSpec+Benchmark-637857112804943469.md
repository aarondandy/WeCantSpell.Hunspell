# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_04/16/2022 13:08:00_
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
|TotalBytesAllocated |           bytes |  120,551,992.00 |  119,996,204.00 |  119,440,416.00 |      786,002.93 |
|TotalCollections [Gen0] |     collections |          487.00 |          485.50 |          484.00 |            2.12 |
|TotalCollections [Gen1] |     collections |          191.00 |          189.50 |          188.00 |            2.12 |
|TotalCollections [Gen2] |     collections |           48.00 |           47.00 |           46.00 |            1.41 |
|    Elapsed Time |              ms |       17,631.00 |       17,608.00 |       17,585.00 |           32.53 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,837,515.71 |    6,814,842.76 |    6,792,169.80 |       32,064.40 |
|TotalCollections [Gen0] |     collections |           27.62 |           27.57 |           27.52 |            0.07 |
|TotalCollections [Gen1] |     collections |           10.83 |           10.76 |           10.69 |            0.10 |
|TotalCollections [Gen2] |     collections |            2.72 |            2.67 |            2.62 |            0.08 |
|    Elapsed Time |              ms |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] FilePairsLoaded |      operations |            3.36 |            3.35 |            3.35 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  120,551,992.00 |    6,837,515.71 |          146.25 |
|               2 |  119,440,416.00 |    6,792,169.80 |          147.23 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          487.00 |           27.62 |   36,203,211.09 |
|               2 |          484.00 |           27.52 |   36,332,678.10 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          191.00 |           10.83 |   92,308,710.99 |
|               2 |          188.00 |           10.69 |   93,537,320.21 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           48.00 |            2.72 |  367,311,745.83 |
|               2 |           46.00 |            2.62 |  382,282,960.87 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       17,631.00 |        1,000.00 |      999,997.95 |
|               2 |       17,585.00 |        1,000.00 |    1,000,000.92 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.35 |  298,829,894.92 |
|               2 |           59.00 |            3.36 |  298,051,122.03 |


