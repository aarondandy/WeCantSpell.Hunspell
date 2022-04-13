# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_04/13/2022 23:07:24_
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
|    Elapsed Time |              ms |        3,995.00 |        3,992.50 |        3,990.00 |            3.54 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,464,828.46 |   22,449,742.04 |   22,434,655.63 |       21,335.41 |
|TotalCollections [Gen0] |     collections |            3.26 |            3.26 |            3.25 |            0.00 |
|TotalCollections [Gen1] |     collections |            3.26 |            3.26 |            3.25 |            0.00 |
|TotalCollections [Gen2] |     collections |            3.26 |            3.26 |            3.25 |            0.00 |
|    Elapsed Time |              ms |        1,000.12 |        1,000.07 |        1,000.03 |            0.07 |
|[Counter] FilePairsLoaded |      operations |           14.79 |           14.78 |           14.77 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,464,828.46 |           44.51 |
|               2 |   89,624,208.00 |   22,434,655.63 |           44.57 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,887,076.92 |
|               2 |           13.00 |            3.25 |  307,300,007.69 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,887,076.92 |
|               2 |           13.00 |            3.25 |  307,300,007.69 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,887,076.92 |
|               2 |           13.00 |            3.25 |  307,300,007.69 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,990.00 |        1,000.12 |      999,882.71 |
|               2 |        3,995.00 |        1,000.03 |      999,974.99 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.79 |   67,619,186.44 |
|               2 |           59.00 |           14.77 |   67,710,171.19 |


