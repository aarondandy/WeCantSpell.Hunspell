# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/05/2022 07:45:58_
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
|    Elapsed Time |              ms |        3,975.00 |        3,962.50 |        3,950.00 |           17.68 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,685,270.43 |   22,613,864.78 |   22,542,459.13 |      100,982.84 |
|TotalCollections [Gen0] |     collections |            3.29 |            3.28 |            3.27 |            0.01 |
|TotalCollections [Gen1] |     collections |            3.29 |            3.28 |            3.27 |            0.01 |
|TotalCollections [Gen2] |     collections |            3.29 |            3.28 |            3.27 |            0.01 |
|    Elapsed Time |              ms |          999.81 |          999.80 |          999.80 |            0.00 |
|[Counter] FilePairsLoaded |      operations |           14.93 |           14.89 |           14.84 |            0.07 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,685,270.43 |           44.08 |
|               2 |   89,624,176.00 |   22,542,459.13 |           44.36 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.29 |  303,904,930.77 |
|               2 |           13.00 |            3.27 |  305,830,315.38 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.29 |  303,904,930.77 |
|               2 |           13.00 |            3.27 |  305,830,315.38 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.29 |  303,904,930.77 |
|               2 |           13.00 |            3.27 |  305,830,315.38 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,950.00 |          999.81 |    1,000,193.44 |
|               2 |        3,975.00 |          999.80 |    1,000,199.77 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.93 |   66,962,103.39 |
|               2 |           59.00 |           14.84 |   67,386,340.68 |


