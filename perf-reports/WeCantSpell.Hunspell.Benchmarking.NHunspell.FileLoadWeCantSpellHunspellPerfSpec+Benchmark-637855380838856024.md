# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_04/14/2022 13:01:23_
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
|TotalBytesAllocated |           bytes |  119,488,856.00 |  119,464,480.00 |  119,440,104.00 |       34,472.87 |
|TotalCollections [Gen0] |     collections |          486.00 |          486.00 |          486.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          190.00 |          190.00 |          190.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           48.00 |           48.00 |           48.00 |            0.00 |
|    Elapsed Time |              ms |       18,605.00 |       18,325.50 |       18,046.00 |          395.27 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,618,488.17 |    6,520,508.88 |    6,422,529.59 |      138,563.64 |
|TotalCollections [Gen0] |     collections |           26.93 |           26.53 |           26.12 |            0.57 |
|TotalCollections [Gen1] |     collections |           10.53 |           10.37 |           10.21 |            0.22 |
|TotalCollections [Gen2] |     collections |            2.66 |            2.62 |            2.58 |            0.06 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.00 |          999.98 |            0.03 |
|[Counter] FilePairsLoaded |      operations |            3.27 |            3.22 |            3.17 |            0.07 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  119,488,856.00 |    6,422,529.59 |          155.70 |
|               2 |  119,440,104.00 |    6,618,488.17 |          151.09 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          486.00 |           26.12 |   38,281,153.91 |
|               2 |          486.00 |           26.93 |   37,132,578.40 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          190.00 |           10.21 |   97,919,162.11 |
|               2 |          190.00 |           10.53 |   94,981,226.84 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           48.00 |            2.58 |  387,596,683.33 |
|               2 |           48.00 |            2.66 |  375,967,356.25 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,605.00 |        1,000.02 |      999,980.69 |
|               2 |       18,046.00 |          999.98 |    1,000,024.00 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.17 |  315,332,894.92 |
|               2 |           59.00 |            3.27 |  305,871,747.46 |


