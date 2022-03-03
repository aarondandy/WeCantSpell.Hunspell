# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/3/2022 4:07:07 AM_
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
|TotalBytesAllocated |           bytes |   10,579,808.00 |   10,579,808.00 |   10,579,808.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           41.00 |           41.00 |           41.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.00 |           14.00 |           14.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            6.00 |            6.00 |            6.00 |            0.00 |
|    Elapsed Time |              ms |        1,379.00 |        1,379.00 |        1,379.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,671,140.23 |    7,671,140.23 |    7,671,140.23 |            0.00 |
|TotalCollections [Gen0] |     collections |           29.73 |           29.73 |           29.73 |            0.00 |
|TotalCollections [Gen1] |     collections |           10.15 |           10.15 |           10.15 |            0.00 |
|TotalCollections [Gen2] |     collections |            4.35 |            4.35 |            4.35 |            0.00 |
|    Elapsed Time |              ms |          999.88 |          999.88 |          999.88 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           42.78 |           42.78 |           42.78 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   10,579,808.00 |    7,671,140.23 |          130.36 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           41.00 |           29.73 |   33,638,297.56 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           14.00 |           10.15 |   98,512,157.14 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            6.00 |            4.35 |  229,861,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,379.00 |          999.88 |    1,000,123.42 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           42.78 |   23,375,766.10 |


