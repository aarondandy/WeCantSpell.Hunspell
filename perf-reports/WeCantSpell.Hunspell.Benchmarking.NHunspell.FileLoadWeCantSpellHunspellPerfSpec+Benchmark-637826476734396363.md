# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/12/2022 02:07:53_
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
|TotalBytesAllocated |           bytes |   30,692,272.00 |   30,094,376.00 |   29,496,480.00 |      845,552.63 |
|TotalCollections [Gen0] |     collections |          505.00 |          501.50 |          498.00 |            4.95 |
|TotalCollections [Gen1] |     collections |          212.00 |          208.50 |          205.00 |            4.95 |
|TotalCollections [Gen2] |     collections |           65.00 |           63.00 |           61.00 |            2.83 |
|    Elapsed Time |              ms |       16,529.00 |       16,401.50 |       16,274.00 |          180.31 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,856,952.10 |    1,834,714.52 |    1,812,476.93 |       31,448.69 |
|TotalCollections [Gen0] |     collections |           31.03 |           30.58 |           30.13 |            0.64 |
|TotalCollections [Gen1] |     collections |           13.03 |           12.71 |           12.40 |            0.44 |
|TotalCollections [Gen2] |     collections |            3.99 |            3.84 |            3.69 |            0.21 |
|    Elapsed Time |              ms |        1,000.04 |        1,000.02 |          999.99 |            0.04 |
|[Counter] FilePairsLoaded |      operations |            3.63 |            3.60 |            3.57 |            0.04 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   29,496,480.00 |    1,812,476.93 |          551.73 |
|               2 |   30,692,272.00 |    1,856,952.10 |          538.52 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          505.00 |           31.03 |   32,225,994.26 |
|               2 |          498.00 |           30.13 |   33,189,368.88 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          212.00 |           13.03 |   76,764,750.47 |
|               2 |          205.00 |           12.40 |   80,625,881.46 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           65.00 |            3.99 |  250,371,186.15 |
|               2 |           61.00 |            3.69 |  270,955,831.15 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       16,274.00 |          999.99 |    1,000,007.81 |
|               2 |       16,529.00 |        1,000.04 |      999,958.00 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.63 |  275,832,662.71 |
|               2 |           59.00 |            3.57 |  280,140,774.58 |


