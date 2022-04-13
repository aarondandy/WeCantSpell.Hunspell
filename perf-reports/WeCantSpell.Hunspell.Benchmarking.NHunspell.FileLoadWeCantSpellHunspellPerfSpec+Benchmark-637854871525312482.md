# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_04/13/2022 22:52:32_
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
|TotalBytesAllocated |           bytes |  111,148,208.00 |  100,577,620.00 |   90,007,032.00 |   14,949,068.91 |
|TotalCollections [Gen0] |     collections |          489.00 |          488.50 |          488.00 |            0.71 |
|TotalCollections [Gen1] |     collections |          192.00 |          191.50 |          191.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           47.00 |           47.00 |           47.00 |            0.00 |
|    Elapsed Time |              ms |       15,434.00 |       15,332.50 |       15,231.00 |          143.54 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,201,480.40 |    6,555,402.13 |    5,909,323.86 |      913,692.65 |
|TotalCollections [Gen0] |     collections |           32.04 |           31.86 |           31.68 |            0.25 |
|TotalCollections [Gen1] |     collections |           12.54 |           12.49 |           12.44 |            0.07 |
|TotalCollections [Gen2] |     collections |            3.09 |            3.07 |            3.05 |            0.03 |
|    Elapsed Time |              ms |          999.99 |          999.99 |          999.98 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.87 |            3.85 |            3.82 |            0.04 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  111,148,208.00 |    7,201,480.40 |          138.86 |
|               2 |   90,007,032.00 |    5,909,323.86 |          169.22 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          489.00 |           31.68 |   31,562,531.08 |
|               2 |          488.00 |           32.04 |   31,211,800.82 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          192.00 |           12.44 |   80,385,821.35 |
|               2 |          191.00 |           12.54 |   79,745,334.03 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           47.00 |            3.05 |  328,384,631.91 |
|               2 |           47.00 |            3.09 |  324,071,463.83 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,434.00 |          999.99 |    1,000,005.03 |
|               2 |       15,231.00 |          999.98 |    1,000,023.56 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.82 |  261,594,537.29 |
|               2 |           59.00 |            3.87 |  258,158,623.73 |


