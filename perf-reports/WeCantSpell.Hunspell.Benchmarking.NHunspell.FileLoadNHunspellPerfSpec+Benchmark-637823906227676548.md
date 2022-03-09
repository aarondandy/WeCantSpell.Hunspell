# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/09/2022 02:43:42_
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
|TotalBytesAllocated |           bytes |   13,280,128.00 |    8,570,748.00 |    3,861,368.00 |    6,660,069.07 |
|TotalCollections [Gen0] |     collections |           17.00 |           16.50 |           16.00 |            0.71 |
|TotalCollections [Gen1] |     collections |           17.00 |           16.50 |           16.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           17.00 |           16.50 |           16.00 |            0.71 |
|    Elapsed Time |              ms |        4,039.00 |        4,026.00 |        4,013.00 |           18.38 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,308,955.11 |    2,132,468.70 |      955,982.29 |    1,663,803.04 |
|TotalCollections [Gen0] |     collections |            4.21 |            4.10 |            3.99 |            0.16 |
|TotalCollections [Gen1] |     collections |            4.21 |            4.10 |            3.99 |            0.16 |
|TotalCollections [Gen2] |     collections |            4.21 |            4.10 |            3.99 |            0.16 |
|    Elapsed Time |              ms |          999.96 |          999.93 |          999.90 |            0.04 |
|[Counter] FilePairsLoaded |      operations |           14.70 |           14.65 |           14.61 |            0.07 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   13,280,128.00 |    3,308,955.11 |          302.21 |
|               2 |    3,861,368.00 |      955,982.29 |        1,046.04 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           16.00 |            3.99 |  250,836,887.50 |
|               2 |           17.00 |            4.21 |  237,597,805.88 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           16.00 |            3.99 |  250,836,887.50 |
|               2 |           17.00 |            4.21 |  237,597,805.88 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           16.00 |            3.99 |  250,836,887.50 |
|               2 |           17.00 |            4.21 |  237,597,805.88 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,013.00 |          999.90 |    1,000,097.23 |
|               2 |        4,039.00 |          999.96 |    1,000,040.28 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.70 |   68,023,562.71 |
|               2 |           59.00 |           14.61 |   68,460,384.75 |


