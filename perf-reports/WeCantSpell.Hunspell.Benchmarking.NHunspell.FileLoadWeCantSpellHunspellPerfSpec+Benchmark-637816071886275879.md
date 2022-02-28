# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_02/28/2022 01:06:28_
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
|TotalBytesAllocated |           bytes |   40,860,504.00 |   40,840,188.00 |   40,819,872.00 |       28,731.16 |
|TotalCollections [Gen0] |     collections |        1,029.00 |        1,027.50 |        1,026.00 |            2.12 |
|TotalCollections [Gen1] |     collections |          365.00 |          364.00 |          363.00 |            1.41 |
|TotalCollections [Gen2] |     collections |           96.00 |           94.00 |           92.00 |            2.83 |
|    Elapsed Time |              ms |       21,266.00 |       20,517.50 |       19,769.00 |        1,058.54 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,064,778.29 |    1,993,080.63 |    1,921,382.96 |      101,395.81 |
|TotalCollections [Gen0] |     collections |           52.05 |           50.15 |           48.25 |            2.69 |
|TotalCollections [Gen1] |     collections |           18.46 |           17.77 |           17.07 |            0.99 |
|TotalCollections [Gen2] |     collections |            4.86 |            4.59 |            4.33 |            0.37 |
|    Elapsed Time |              ms |          999.99 |          999.98 |          999.97 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            2.98 |            2.88 |            2.77 |            0.15 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   40,860,504.00 |    1,921,382.96 |          520.46 |
|               2 |   40,819,872.00 |    2,064,778.29 |          484.31 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,026.00 |           48.25 |   20,727,285.19 |
|               2 |        1,029.00 |           52.05 |   19,212,453.94 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          363.00 |           17.07 |   58,584,558.13 |
|               2 |          365.00 |           18.46 |   54,163,329.04 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           92.00 |            4.33 |  231,154,289.13 |
|               2 |           96.00 |            4.86 |  205,933,490.63 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       21,266.00 |          999.99 |    1,000,009.15 |
|               2 |       19,769.00 |          999.97 |    1,000,031.11 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            2.77 |  360,443,976.27 |
|               2 |           59.00 |            2.98 |  335,078,222.03 |


