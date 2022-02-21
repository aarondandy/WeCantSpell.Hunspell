# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_2/21/2022 1:43:10 PM_
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
|TotalBytesAllocated |           bytes |    5,996,472.00 |    5,996,472.00 |    5,996,472.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           93.00 |           93.00 |           93.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           28.00 |           28.00 |           28.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.00 |            9.00 |            9.00 |            0.00 |
|    Elapsed Time |              ms |        1,207.00 |        1,207.00 |        1,207.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           50.00 |           50.00 |           50.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,970,703.67 |    4,970,703.67 |    4,970,703.67 |            0.00 |
|TotalCollections [Gen0] |     collections |           77.09 |           77.09 |           77.09 |            0.00 |
|TotalCollections [Gen1] |     collections |           23.21 |           23.21 |           23.21 |            0.00 |
|TotalCollections [Gen2] |     collections |            7.46 |            7.46 |            7.46 |            0.00 |
|    Elapsed Time |              ms |        1,000.53 |        1,000.53 |        1,000.53 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           41.45 |           41.45 |           41.45 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,996,472.00 |    4,970,703.67 |          201.18 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           93.00 |           77.09 |   12,971,643.01 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           28.00 |           23.21 |   43,084,385.71 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            9.00 |            7.46 |  134,040,311.11 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,207.00 |        1,000.53 |      999,472.08 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           50.00 |           41.45 |   24,127,256.00 |


