# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/21/2022 3:16:44 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.3,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |   19,335,984.00 |   19,335,984.00 |   19,335,984.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          101.00 |          101.00 |          101.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           40.00 |           40.00 |           40.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        1,448.00 |        1,448.00 |        1,448.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   13,359,072.28 |   13,359,072.28 |   13,359,072.28 |            0.00 |
|TotalCollections [Gen0] |     collections |           69.78 |           69.78 |           69.78 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.64 |           27.64 |           27.64 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.98 |            8.98 |            8.98 |            0.00 |
|    Elapsed Time |              ms |        1,000.41 |        1,000.41 |        1,000.41 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          122.29 |          122.29 |          122.29 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   19,335,984.00 |   13,359,072.28 |           74.86 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          101.00 |           69.78 |   14,330,739.60 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           40.00 |           27.64 |   36,185,117.50 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            8.98 |  111,338,823.08 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,448.00 |        1,000.41 |      999,588.88 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          122.29 |    8,177,427.68 |


