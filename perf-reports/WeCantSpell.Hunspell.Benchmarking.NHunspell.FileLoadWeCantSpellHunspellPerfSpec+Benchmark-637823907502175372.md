# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/09/2022 02:45:50_
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
|TotalBytesAllocated |           bytes |  122,063,288.00 |  122,062,748.00 |  122,062,208.00 |          763.68 |
|TotalCollections [Gen0] |     collections |          548.00 |          548.00 |          548.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          221.00 |          220.50 |          220.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           63.00 |           62.00 |           61.00 |            1.41 |
|    Elapsed Time |              ms |       16,380.00 |       16,364.00 |       16,348.00 |           22.63 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,466,525.12 |    7,459,105.21 |    7,451,685.29 |       10,493.34 |
|TotalCollections [Gen0] |     collections |           33.52 |           33.49 |           33.45 |            0.05 |
|TotalCollections [Gen1] |     collections |           13.49 |           13.47 |           13.46 |            0.02 |
|TotalCollections [Gen2] |     collections |            3.85 |            3.79 |            3.73 |            0.08 |
|    Elapsed Time |              ms |        1,000.00 |          999.98 |          999.97 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.61 |            3.61 |            3.60 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  122,063,288.00 |    7,466,525.12 |          133.93 |
|               2 |  122,062,208.00 |    7,451,685.29 |          134.20 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          548.00 |           33.52 |   29,832,247.63 |
|               2 |          548.00 |           33.45 |   29,891,393.25 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          220.00 |           13.46 |   74,309,416.82 |
|               2 |          221.00 |           13.49 |   74,119,834.84 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           61.00 |            3.73 |  268,001,175.41 |
|               2 |           63.00 |            3.85 |  260,007,674.60 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       16,348.00 |        1,000.00 |    1,000,004.39 |
|               2 |       16,380.00 |          999.97 |    1,000,029.52 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.61 |  277,085,961.02 |
|               2 |           59.00 |            3.60 |  277,635,313.56 |


