# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/10/2022 04:43:41_
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
|TotalBytesAllocated |           bytes |   30,698,832.00 |   30,696,792.00 |   30,694,752.00 |        2,885.00 |
|TotalCollections [Gen0] |     collections |          505.00 |          505.00 |          505.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          213.00 |          212.50 |          212.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           69.00 |           69.00 |           69.00 |            0.00 |
|    Elapsed Time |              ms |       15,295.00 |       15,288.00 |       15,281.00 |            9.90 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,008,632.20 |    2,007,923.57 |    2,007,214.95 |        1,002.14 |
|TotalCollections [Gen0] |     collections |           33.05 |           33.03 |           33.02 |            0.02 |
|TotalCollections [Gen1] |     collections |           13.93 |           13.90 |           13.87 |            0.04 |
|TotalCollections [Gen2] |     collections |            4.52 |            4.51 |            4.51 |            0.00 |
|    Elapsed Time |              ms |        1,000.05 |        1,000.01 |          999.97 |            0.05 |
|[Counter] FilePairsLoaded |      operations |            3.86 |            3.86 |            3.86 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,694,752.00 |    2,008,632.20 |          497.85 |
|               2 |   30,698,832.00 |    2,007,214.95 |          498.20 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          505.00 |           33.05 |   30,260,237.43 |
|               2 |          505.00 |           33.02 |   30,285,628.51 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          212.00 |           13.87 |   72,082,169.34 |
|               2 |          213.00 |           13.93 |   71,803,954.93 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           69.00 |            4.52 |  221,469,853.62 |
|               2 |           69.00 |            4.51 |  221,655,686.96 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,281.00 |          999.97 |    1,000,027.48 |
|               2 |       15,295.00 |        1,000.05 |      999,950.47 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.86 |  259,007,116.95 |
|               2 |           59.00 |            3.86 |  259,224,447.46 |


