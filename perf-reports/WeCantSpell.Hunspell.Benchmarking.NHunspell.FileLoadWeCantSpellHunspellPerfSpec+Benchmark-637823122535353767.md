# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/08/2022 04:57:33_
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
|TotalBytesAllocated |           bytes |   35,021,624.00 |   35,019,328.00 |   35,017,032.00 |        3,247.03 |
|TotalCollections [Gen0] |     collections |          614.00 |          613.50 |          613.00 |            0.71 |
|TotalCollections [Gen1] |     collections |          249.00 |          248.50 |          248.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           72.00 |           71.00 |           70.00 |            1.41 |
|    Elapsed Time |              ms |       16,460.00 |       16,443.50 |       16,427.00 |           23.33 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,131,629.18 |    2,129,605.77 |    2,127,582.35 |        2,861.54 |
|TotalCollections [Gen0] |     collections |           37.38 |           37.31 |           37.24 |            0.10 |
|TotalCollections [Gen1] |     collections |           15.16 |           15.11 |           15.07 |            0.06 |
|TotalCollections [Gen2] |     collections |            4.38 |            4.32 |            4.25 |            0.09 |
|    Elapsed Time |              ms |          999.98 |          999.97 |          999.95 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.59 |            3.59 |            3.58 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   35,017,032.00 |    2,131,629.18 |          469.12 |
|               2 |   35,021,624.00 |    2,127,582.35 |          470.02 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          614.00 |           37.38 |   26,754,651.95 |
|               2 |          613.00 |           37.24 |   26,852,790.70 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          249.00 |           15.16 |   65,973,318.47 |
|               2 |          248.00 |           15.07 |   66,374,035.08 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           72.00 |            4.38 |  228,157,726.39 |
|               2 |           70.00 |            4.25 |  235,153,724.29 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       16,427.00 |          999.98 |    1,000,021.69 |
|               2 |       16,460.00 |          999.95 |    1,000,046.22 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.59 |  278,429,767.80 |
|               2 |           59.00 |            3.58 |  278,995,944.07 |


