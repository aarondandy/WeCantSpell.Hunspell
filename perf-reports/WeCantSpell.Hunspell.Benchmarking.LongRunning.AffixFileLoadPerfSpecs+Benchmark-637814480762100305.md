# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_2/26/2022 4:54:36 AM_
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
|TotalBytesAllocated |           bytes |   13,259,344.00 |   13,259,344.00 |   13,259,344.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          133.00 |          133.00 |          133.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           32.00 |           32.00 |           32.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.00 |            9.00 |            9.00 |            0.00 |
|    Elapsed Time |              ms |        2,011.00 |        2,011.00 |        2,011.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,595,806.16 |    6,595,806.16 |    6,595,806.16 |            0.00 |
|TotalCollections [Gen0] |     collections |           66.16 |           66.16 |           66.16 |            0.00 |
|TotalCollections [Gen1] |     collections |           15.92 |           15.92 |           15.92 |            0.00 |
|TotalCollections [Gen2] |     collections |            4.48 |            4.48 |            4.48 |            0.00 |
|    Elapsed Time |              ms |        1,000.36 |        1,000.36 |        1,000.36 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           29.35 |           29.35 |           29.35 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   13,259,344.00 |    6,595,806.16 |          151.61 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          133.00 |           66.16 |   15,114,803.76 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           32.00 |           15.92 |   62,820,903.12 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            9.00 |            4.48 |  223,363,211.11 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        2,011.00 |        1,000.36 |      999,636.45 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           29.35 |   34,072,354.24 |


