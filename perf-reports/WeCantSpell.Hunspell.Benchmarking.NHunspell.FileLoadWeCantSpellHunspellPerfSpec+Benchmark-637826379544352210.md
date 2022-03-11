# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/11/2022 23:25:54_
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
|TotalBytesAllocated |           bytes |   30,693,944.00 |   30,097,596.00 |   29,501,248.00 |      843,363.43 |
|TotalCollections [Gen0] |     collections |          508.00 |          506.50 |          505.00 |            2.12 |
|TotalCollections [Gen1] |     collections |          215.00 |          214.00 |          213.00 |            1.41 |
|TotalCollections [Gen2] |     collections |           70.00 |           69.00 |           68.00 |            1.41 |
|    Elapsed Time |              ms |       16,952.00 |       16,852.00 |       16,752.00 |          141.42 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,832,283.55 |    1,786,263.84 |    1,740,244.13 |       65,081.69 |
|TotalCollections [Gen0] |     collections |           30.15 |           30.06 |           29.97 |            0.13 |
|TotalCollections [Gen1] |     collections |           12.72 |           12.70 |           12.68 |            0.02 |
|TotalCollections [Gen2] |     collections |            4.13 |            4.09 |            4.06 |            0.05 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.00 |          999.98 |            0.03 |
|[Counter] FilePairsLoaded |      operations |            3.52 |            3.50 |            3.48 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,693,944.00 |    1,832,283.55 |          545.77 |
|               2 |   29,501,248.00 |    1,740,244.13 |          574.63 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          505.00 |           30.15 |   33,171,769.31 |
|               2 |          508.00 |           29.97 |   33,370,790.75 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          213.00 |           12.72 |   78,646,683.10 |
|               2 |          215.00 |           12.68 |   78,848,193.95 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           68.00 |            4.06 |  246,349,169.12 |
|               2 |           70.00 |            4.13 |  242,176,595.71 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       16,752.00 |        1,000.02 |      999,984.69 |
|               2 |       16,952.00 |          999.98 |    1,000,021.34 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.52 |  283,927,855.93 |
|               2 |           59.00 |            3.48 |  287,328,164.41 |


