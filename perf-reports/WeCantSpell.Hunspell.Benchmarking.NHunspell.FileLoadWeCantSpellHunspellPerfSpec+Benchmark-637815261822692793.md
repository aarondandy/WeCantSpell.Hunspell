# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_02/27/2022 02:36:22_
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
|TotalBytesAllocated |           bytes |   31,031,312.00 |   31,022,004.00 |   31,012,696.00 |       13,163.50 |
|TotalCollections [Gen0] |     collections |        1,439.00 |        1,438.00 |        1,437.00 |            1.41 |
|TotalCollections [Gen1] |     collections |          451.00 |          446.00 |          441.00 |            7.07 |
|TotalCollections [Gen2] |     collections |          128.00 |          128.00 |          128.00 |            0.00 |
|    Elapsed Time |              ms |       22,692.00 |       22,624.00 |       22,556.00 |           96.17 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,375,778.57 |    1,371,217.03 |    1,366,655.49 |        6,451.00 |
|TotalCollections [Gen0] |     collections |           63.80 |           63.56 |           63.33 |            0.33 |
|TotalCollections [Gen1] |     collections |           20.00 |           19.71 |           19.43 |            0.40 |
|TotalCollections [Gen2] |     collections |            5.67 |            5.66 |            5.64 |            0.02 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.00 |          999.98 |            0.03 |
|[Counter] FilePairsLoaded |      operations |            2.62 |            2.61 |            2.60 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   31,031,312.00 |    1,375,778.57 |          726.86 |
|               2 |   31,012,696.00 |    1,366,655.49 |          731.71 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,439.00 |           63.80 |   15,674,395.55 |
|               2 |        1,437.00 |           63.33 |   15,791,511.62 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          451.00 |           20.00 |   50,012,095.79 |
|               2 |          441.00 |           19.43 |   51,456,694.33 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          128.00 |            5.67 |  176,214,493.75 |
|               2 |          128.00 |            5.64 |  177,284,392.19 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       22,556.00 |        1,000.02 |      999,975.85 |
|               2 |       22,692.00 |          999.98 |    1,000,017.72 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            2.62 |  382,295,850.85 |
|               2 |           59.00 |            2.60 |  384,616,986.44 |


