# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/17/2022 00:53:48_
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
|TotalBytesAllocated |           bytes |   89,624,152.00 |   46,747,280.00 |    3,870,408.00 |   60,637,053.89 |
|TotalCollections [Gen0] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen1] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen2] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|    Elapsed Time |              ms |        3,989.00 |        3,983.50 |        3,978.00 |            7.78 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,467,108.66 |   11,719,940.14 |      972,771.62 |   15,198,791.47 |
|TotalCollections [Gen0] |     collections |            4.02 |            3.64 |            3.26 |            0.54 |
|TotalCollections [Gen1] |     collections |            4.02 |            3.64 |            3.26 |            0.54 |
|TotalCollections [Gen2] |     collections |            4.02 |            3.64 |            3.26 |            0.54 |
|    Elapsed Time |              ms |          999.97 |          999.89 |          999.81 |            0.11 |
|[Counter] FilePairsLoaded |      operations |           14.83 |           14.81 |           14.79 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,467,108.66 |           44.51 |
|               2 |    3,870,408.00 |      972,771.62 |        1,027.99 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,855,930.77 |
|               2 |           16.00 |            4.02 |  248,671,418.75 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,855,930.77 |
|               2 |           16.00 |            4.02 |  248,671,418.75 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,855,930.77 |
|               2 |           16.00 |            4.02 |  248,671,418.75 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,989.00 |          999.97 |    1,000,031.86 |
|               2 |        3,978.00 |          999.81 |    1,000,186.70 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.79 |   67,612,323.73 |
|               2 |           59.00 |           14.83 |   67,436,316.95 |


