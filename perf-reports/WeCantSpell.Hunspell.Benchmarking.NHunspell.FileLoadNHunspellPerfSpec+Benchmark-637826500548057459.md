# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/12/2022 02:47:34_
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
|TotalBytesAllocated |           bytes |   89,624,176.00 |   46,748,788.00 |    3,873,400.00 |   60,634,955.20 |
|TotalCollections [Gen0] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen1] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen2] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|    Elapsed Time |              ms |        3,999.00 |        3,996.50 |        3,994.00 |            3.54 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,410,908.85 |   11,690,270.19 |      969,631.54 |   15,161,272.58 |
|TotalCollections [Gen0] |     collections |            4.01 |            3.63 |            3.25 |            0.53 |
|TotalCollections [Gen1] |     collections |            4.01 |            3.63 |            3.25 |            0.53 |
|TotalCollections [Gen2] |     collections |            4.01 |            3.63 |            3.25 |            0.53 |
|    Elapsed Time |              ms |          999.97 |          999.89 |          999.82 |            0.10 |
|[Counter] FilePairsLoaded |      operations |           14.77 |           14.76 |           14.75 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,873,400.00 |      969,631.54 |        1,031.32 |
|               2 |   89,624,176.00 |   22,410,908.85 |           44.62 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           16.00 |            4.01 |  249,669,581.25 |
|               2 |           13.00 |            3.25 |  307,625,515.38 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           16.00 |            4.01 |  249,669,581.25 |
|               2 |           13.00 |            3.25 |  307,625,515.38 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           16.00 |            4.01 |  249,669,581.25 |
|               2 |           13.00 |            3.25 |  307,625,515.38 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,994.00 |          999.82 |    1,000,178.59 |
|               2 |        3,999.00 |          999.97 |    1,000,032.93 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.77 |   67,707,005.08 |
|               2 |           59.00 |           14.75 |   67,781,893.22 |


