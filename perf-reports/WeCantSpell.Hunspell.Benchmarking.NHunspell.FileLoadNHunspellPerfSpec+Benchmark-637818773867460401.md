# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/03/2022 04:09:46_
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
|    Elapsed Time |              ms |        3,971.00 |        3,963.50 |        3,956.00 |           10.61 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,651,858.80 |   22,608,359.68 |   22,564,860.57 |       61,517.04 |
|TotalCollections [Gen0] |     collections |            3.29 |            3.28 |            3.27 |            0.01 |
|TotalCollections [Gen1] |     collections |            3.29 |            3.28 |            3.27 |            0.01 |
|TotalCollections [Gen2] |     collections |            3.29 |            3.28 |            3.27 |            0.01 |
|    Elapsed Time |              ms |          999.85 |          999.82 |          999.79 |            0.05 |
|[Counter] FilePairsLoaded |      operations |           14.91 |           14.88 |           14.85 |            0.04 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,651,858.80 |           44.15 |
|               2 |   89,624,176.00 |   22,564,860.57 |           44.32 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.29 |  304,353,192.31 |
|               2 |           13.00 |            3.27 |  305,526,700.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.29 |  304,353,192.31 |
|               2 |           13.00 |            3.27 |  305,526,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.29 |  304,353,192.31 |
|               2 |           13.00 |            3.27 |  305,526,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,956.00 |          999.85 |    1,000,149.52 |
|               2 |        3,971.00 |          999.79 |    1,000,213.32 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.91 |   67,060,872.88 |
|               2 |           59.00 |           14.85 |   67,319,442.37 |


