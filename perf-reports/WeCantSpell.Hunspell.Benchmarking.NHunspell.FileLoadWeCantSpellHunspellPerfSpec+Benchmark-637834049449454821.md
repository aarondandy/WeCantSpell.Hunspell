# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/20/2022 20:29:04_
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
|TotalBytesAllocated |           bytes |  203,532,336.00 |  156,624,304.00 |  109,716,272.00 |   66,337,975.04 |
|TotalCollections [Gen0] |     collections |          504.00 |          502.50 |          501.00 |            2.12 |
|TotalCollections [Gen1] |     collections |          203.00 |          203.00 |          203.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           55.00 |           55.00 |           55.00 |            0.00 |
|    Elapsed Time |              ms |       22,932.00 |       22,900.00 |       22,868.00 |           45.25 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,875,233.97 |    6,836,551.90 |    4,797,869.83 |    2,883,131.83 |
|TotalCollections [Gen0] |     collections |           21.98 |           21.94 |           21.91 |            0.05 |
|TotalCollections [Gen1] |     collections |            8.88 |            8.86 |            8.85 |            0.02 |
|TotalCollections [Gen2] |     collections |            2.41 |            2.40 |            2.40 |            0.00 |
|    Elapsed Time |              ms |        1,000.01 |          999.99 |          999.97 |            0.03 |
|[Counter] FilePairsLoaded |      operations |            2.58 |            2.58 |            2.57 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  203,532,336.00 |    8,875,233.97 |          112.67 |
|               2 |  109,716,272.00 |    4,797,869.83 |          208.43 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          504.00 |           21.98 |   45,501,223.02 |
|               2 |          501.00 |           21.91 |   45,644,121.76 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          203.00 |            8.85 |  112,968,553.69 |
|               2 |          203.00 |            8.88 |  112,648,793.10 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           55.00 |            2.40 |  416,956,661.82 |
|               2 |           55.00 |            2.41 |  415,776,454.55 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       22,932.00 |          999.97 |    1,000,026.88 |
|               2 |       22,868.00 |        1,000.01 |      999,987.10 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            2.57 |  388,688,413.56 |
|               2 |           59.00 |            2.58 |  387,588,220.34 |


