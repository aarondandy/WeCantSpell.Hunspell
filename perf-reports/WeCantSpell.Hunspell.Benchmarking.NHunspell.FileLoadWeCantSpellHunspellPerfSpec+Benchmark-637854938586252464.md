# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_04/14/2022 00:44:18_
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
|TotalBytesAllocated |           bytes |  119,431,888.00 |  105,023,152.00 |   90,614,416.00 |   20,377,029.87 |
|TotalCollections [Gen0] |     collections |          486.00 |          485.00 |          484.00 |            1.41 |
|TotalCollections [Gen1] |     collections |          190.00 |          189.00 |          188.00 |            1.41 |
|TotalCollections [Gen2] |     collections |           48.00 |           46.00 |           44.00 |            2.83 |
|    Elapsed Time |              ms |       18,138.00 |       18,082.00 |       18,026.00 |           79.20 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,584,856.43 |    5,805,855.54 |    5,026,854.64 |    1,101,673.64 |
|TotalCollections [Gen0] |     collections |           26.85 |           26.82 |           26.80 |            0.04 |
|TotalCollections [Gen1] |     collections |           10.48 |           10.45 |           10.43 |            0.03 |
|TotalCollections [Gen2] |     collections |            2.65 |            2.54 |            2.44 |            0.15 |
|    Elapsed Time |              ms |        1,000.04 |        1,000.02 |        1,000.00 |            0.03 |
|[Counter] FilePairsLoaded |      operations |            3.27 |            3.26 |            3.25 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   90,614,416.00 |    5,026,854.64 |          198.93 |
|               2 |  119,431,888.00 |    6,584,856.43 |          151.86 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          484.00 |           26.85 |   37,243,939.05 |
|               2 |          486.00 |           26.80 |   37,319,663.37 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          188.00 |           10.43 |   95,883,332.45 |
|               2 |          190.00 |           10.48 |   95,459,770.53 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           44.00 |            2.44 |  409,683,329.55 |
|               2 |           48.00 |            2.65 |  377,861,591.67 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,026.00 |        1,000.00 |    1,000,003.69 |
|               2 |       18,138.00 |        1,000.04 |      999,964.52 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.27 |  305,526,550.85 |
|               2 |           59.00 |            3.25 |  307,412,820.34 |


