# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/03/2022 04:12:15_
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
|TotalBytesAllocated |           bytes |  149,065,824.00 |  149,047,052.00 |  149,028,280.00 |       26,547.62 |
|TotalCollections [Gen0] |     collections |          819.00 |          816.50 |          814.00 |            3.54 |
|TotalCollections [Gen1] |     collections |          320.00 |          317.50 |          315.00 |            3.54 |
|TotalCollections [Gen2] |     collections |           85.00 |           81.50 |           78.00 |            4.95 |
|    Elapsed Time |              ms |       18,637.00 |       18,526.50 |       18,416.00 |          156.27 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,092,250.44 |    8,045,368.13 |    7,998,485.83 |       66,301.59 |
|TotalCollections [Gen0] |     collections |           44.47 |           44.07 |           43.68 |            0.56 |
|TotalCollections [Gen1] |     collections |           17.38 |           17.14 |           16.90 |            0.34 |
|TotalCollections [Gen2] |     collections |            4.62 |            4.40 |            4.19 |            0.30 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.00 |          999.99 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.20 |            3.18 |            3.17 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  149,028,280.00 |    8,092,250.44 |          123.58 |
|               2 |  149,065,824.00 |    7,998,485.83 |          125.02 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          819.00 |           44.47 |   22,486,169.11 |
|               2 |          814.00 |           43.68 |   22,895,276.90 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          320.00 |           17.38 |   57,550,539.06 |
|               2 |          315.00 |           16.90 |   59,164,302.86 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           85.00 |            4.62 |  216,660,852.94 |
|               2 |           78.00 |            4.19 |  238,932,761.54 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,416.00 |          999.99 |    1,000,009.37 |
|               2 |       18,637.00 |        1,000.01 |      999,986.88 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.20 |  312,138,516.95 |
|               2 |           59.00 |            3.17 |  315,877,210.17 |


