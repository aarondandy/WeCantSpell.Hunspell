# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_04/06/2022 20:03:33_
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
|TotalBytesAllocated |           bytes |   89,624,176.00 |   89,624,164.00 |   89,624,152.00 |           16.97 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        3,985.00 |        3,981.00 |        3,977.00 |            5.66 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,530,876.32 |   22,510,147.48 |   22,489,418.64 |       29,315.00 |
|TotalCollections [Gen0] |     collections |            3.27 |            3.27 |            3.26 |            0.00 |
|TotalCollections [Gen1] |     collections |            3.27 |            3.27 |            3.26 |            0.00 |
|TotalCollections [Gen2] |     collections |            3.27 |            3.27 |            3.26 |            0.00 |
|    Elapsed Time |              ms |          999.96 |          999.87 |          999.79 |            0.12 |
|[Counter] FilePairsLoaded |      operations |           14.83 |           14.82 |           14.80 |            0.02 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,489,418.64 |           44.47 |
|               2 |   89,624,176.00 |   22,530,876.32 |           44.38 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,551,523.08 |
|               2 |           13.00 |            3.27 |  305,987,538.46 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,551,523.08 |
|               2 |           13.00 |            3.27 |  305,987,538.46 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,551,523.08 |
|               2 |           13.00 |            3.27 |  305,987,538.46 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,985.00 |          999.96 |    1,000,042.61 |
|               2 |        3,977.00 |          999.79 |    1,000,210.71 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.80 |   67,545,250.85 |
|               2 |           59.00 |           14.83 |   67,420,983.05 |


