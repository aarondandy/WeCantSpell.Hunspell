# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/28/2022 22:51:55_
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
|TotalBytesAllocated |           bytes |  119,429,856.00 |  105,018,084.00 |   90,606,312.00 |   20,381,323.42 |
|TotalCollections [Gen0] |     collections |          483.00 |          482.50 |          482.00 |            0.71 |
|TotalCollections [Gen1] |     collections |          187.00 |          186.50 |          186.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           46.00 |           45.00 |           44.00 |            1.41 |
|    Elapsed Time |              ms |       18,702.00 |       18,327.00 |       17,952.00 |          530.33 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,385,804.57 |    5,716,335.83 |    5,046,867.09 |      946,771.77 |
|TotalCollections [Gen0] |     collections |           26.85 |           26.34 |           25.83 |            0.72 |
|TotalCollections [Gen1] |     collections |           10.36 |           10.18 |           10.00 |            0.26 |
|TotalCollections [Gen2] |     collections |            2.46 |            2.46 |            2.45 |            0.01 |
|    Elapsed Time |              ms |          999.98 |          999.96 |          999.95 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.29 |            3.22 |            3.15 |            0.09 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  119,429,856.00 |    6,385,804.57 |          156.60 |
|               2 |   90,606,312.00 |    5,046,867.09 |          198.14 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          483.00 |           25.83 |   38,721,320.08 |
|               2 |          482.00 |           26.85 |   37,246,849.79 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          187.00 |           10.00 |  100,012,821.39 |
|               2 |          186.00 |           10.36 |   96,521,406.45 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           46.00 |            2.46 |  406,573,860.87 |
|               2 |           44.00 |            2.45 |  408,022,309.09 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,702.00 |          999.98 |    1,000,021.26 |
|               2 |       17,952.00 |          999.95 |    1,000,054.68 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.15 |  316,989,789.83 |
|               2 |           59.00 |            3.29 |  304,287,823.73 |


