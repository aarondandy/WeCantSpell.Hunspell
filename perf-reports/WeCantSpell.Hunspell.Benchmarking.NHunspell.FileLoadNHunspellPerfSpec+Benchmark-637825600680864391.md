# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/11/2022 01:47:48_
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
|TotalBytesAllocated |           bytes |   89,624,208.00 |   89,624,180.00 |   89,624,152.00 |           39.60 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        4,005.00 |        3,986.50 |        3,968.00 |           26.16 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,588,463.44 |   22,483,116.64 |   22,377,769.84 |      148,982.87 |
|TotalCollections [Gen0] |     collections |            3.28 |            3.26 |            3.25 |            0.02 |
|TotalCollections [Gen1] |     collections |            3.28 |            3.26 |            3.25 |            0.02 |
|TotalCollections [Gen2] |     collections |            3.28 |            3.26 |            3.25 |            0.02 |
|    Elapsed Time |              ms |        1,000.08 |        1,000.03 |          999.99 |            0.06 |
|[Counter] FilePairsLoaded |      operations |           14.87 |           14.80 |           14.73 |            0.10 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,377,769.84 |           44.69 |
|               2 |   89,624,208.00 |   22,588,463.44 |           44.27 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  308,080,992.31 |
|               2 |           13.00 |            3.28 |  305,207,561.54 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  308,080,992.31 |
|               2 |           13.00 |            3.28 |  305,207,561.54 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  308,080,992.31 |
|               2 |           13.00 |            3.28 |  305,207,561.54 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,005.00 |          999.99 |    1,000,013.21 |
|               2 |        3,968.00 |        1,000.08 |      999,923.97 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.73 |   67,882,252.54 |
|               2 |           59.00 |           14.87 |   67,249,123.73 |


