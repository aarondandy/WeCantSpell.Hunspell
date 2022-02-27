# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_2/27/2022 10:56:55 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.2,IsMono=False,MaxGcGeneration=2
```

### NBench Settings
```ini
RunMode=Throughput, TestMode=Measurement
NumberOfIterations=1, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,140,296.00 |    6,140,296.00 |    6,140,296.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          129.00 |          129.00 |          129.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           34.00 |           34.00 |           34.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           11.00 |           11.00 |           11.00 |            0.00 |
|    Elapsed Time |              ms |        1,829.00 |        1,829.00 |        1,829.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,357,802.37 |    3,357,802.37 |    3,357,802.37 |            0.00 |
|TotalCollections [Gen0] |     collections |           70.54 |           70.54 |           70.54 |            0.00 |
|TotalCollections [Gen1] |     collections |           18.59 |           18.59 |           18.59 |            0.00 |
|TotalCollections [Gen2] |     collections |            6.02 |            6.02 |            6.02 |            0.00 |
|    Elapsed Time |              ms |        1,000.18 |        1,000.18 |        1,000.18 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           32.26 |           32.26 |           32.26 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,140,296.00 |    3,357,802.37 |          297.81 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          129.00 |           70.54 |   14,175,698.45 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           34.00 |           18.59 |   53,784,267.65 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           11.00 |            6.02 |  166,242,281.82 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,829.00 |        1,000.18 |      999,816.89 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           32.26 |   30,994,323.73 |


