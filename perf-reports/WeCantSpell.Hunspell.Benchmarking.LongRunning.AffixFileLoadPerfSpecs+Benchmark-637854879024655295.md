# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_4/13/2022 11:05:02 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.4,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    3,817,496.00 |    3,817,496.00 |    3,817,496.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           95.00 |           95.00 |           95.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           35.00 |           35.00 |           35.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        1,336.00 |        1,336.00 |        1,336.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,857,696.59 |    2,857,696.59 |    2,857,696.59 |            0.00 |
|TotalCollections [Gen0] |     collections |           71.11 |           71.11 |           71.11 |            0.00 |
|TotalCollections [Gen1] |     collections |           26.20 |           26.20 |           26.20 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.73 |            9.73 |            9.73 |            0.00 |
|    Elapsed Time |              ms |        1,000.10 |        1,000.10 |        1,000.10 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          132.50 |          132.50 |          132.50 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,817,496.00 |    2,857,696.59 |          349.93 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           95.00 |           71.11 |   14,061,733.68 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           35.00 |           26.20 |   38,167,562.86 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            9.73 |  102,758,823.08 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,336.00 |        1,000.10 |      999,898.73 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          132.50 |    7,547,258.19 |


