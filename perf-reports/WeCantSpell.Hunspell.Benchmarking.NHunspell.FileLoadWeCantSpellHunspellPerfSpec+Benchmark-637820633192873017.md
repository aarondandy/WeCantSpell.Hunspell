# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/05/2022 07:48:39_
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
|TotalBytesAllocated |           bytes |  148,879,456.00 |  148,876,068.00 |  148,872,680.00 |        4,791.36 |
|TotalCollections [Gen0] |     collections |          740.00 |          739.00 |          738.00 |            1.41 |
|TotalCollections [Gen1] |     collections |          299.00 |          298.00 |          297.00 |            1.41 |
|TotalCollections [Gen2] |     collections |           79.00 |           77.50 |           76.00 |            2.12 |
|    Elapsed Time |              ms |       19,882.00 |       19,846.50 |       19,811.00 |           50.20 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,514,857.64 |    7,501,519.08 |    7,488,180.51 |       18,863.58 |
|TotalCollections [Gen0] |     collections |           37.25 |           37.24 |           37.22 |            0.02 |
|TotalCollections [Gen1] |     collections |           15.04 |           15.02 |           14.99 |            0.03 |
|TotalCollections [Gen2] |     collections |            3.97 |            3.90 |            3.84 |            0.10 |
|    Elapsed Time |              ms |        1,000.03 |        1,000.02 |        1,000.00 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            2.98 |            2.97 |            2.97 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  148,872,680.00 |    7,514,857.64 |          133.07 |
|               2 |  148,879,456.00 |    7,488,180.51 |          133.54 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          738.00 |           37.25 |   26,843,422.49 |
|               2 |          740.00 |           37.22 |   26,867,468.51 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          297.00 |           14.99 |   66,701,837.71 |
|               2 |          299.00 |           15.04 |   66,494,738.13 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           76.00 |            3.84 |  260,663,760.53 |
|               2 |           79.00 |            3.97 |  251,669,958.23 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       19,811.00 |        1,000.03 |      999,972.03 |
|               2 |       19,882.00 |        1,000.00 |      999,996.31 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            2.98 |  335,770,267.80 |
|               2 |           59.00 |            2.97 |  336,981,808.47 |


