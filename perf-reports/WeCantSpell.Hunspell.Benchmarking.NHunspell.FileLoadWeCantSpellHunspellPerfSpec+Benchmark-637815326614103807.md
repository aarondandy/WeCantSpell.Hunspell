# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_02/27/2022 04:24:21_
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
|TotalBytesAllocated |           bytes |  127,130,208.00 |   78,973,784.00 |   30,817,360.00 |   68,103,467.94 |
|TotalCollections [Gen0] |     collections |        1,228.00 |        1,227.50 |        1,227.00 |            0.71 |
|TotalCollections [Gen1] |     collections |          398.00 |          397.50 |          397.00 |            0.71 |
|TotalCollections [Gen2] |     collections |          106.00 |          106.00 |          106.00 |            0.00 |
|    Elapsed Time |              ms |       21,729.00 |       21,706.50 |       21,684.00 |           31.82 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,850,631.32 |    3,635,942.09 |    1,421,252.86 |    3,132,043.55 |
|TotalCollections [Gen0] |     collections |           56.59 |           56.55 |           56.51 |            0.05 |
|TotalCollections [Gen1] |     collections |           18.36 |           18.31 |           18.27 |            0.06 |
|TotalCollections [Gen2] |     collections |            4.89 |            4.88 |            4.88 |            0.01 |
|    Elapsed Time |              ms |        1,000.04 |        1,000.01 |          999.99 |            0.04 |
|[Counter] FilePairsLoaded |      operations |            2.72 |            2.72 |            2.72 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,817,360.00 |    1,421,252.86 |          703.60 |
|               2 |  127,130,208.00 |    5,850,631.32 |          170.92 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,227.00 |           56.59 |   17,671,748.33 |
|               2 |        1,228.00 |           56.51 |   17,694,881.51 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          398.00 |           18.36 |   54,480,490.45 |
|               2 |          397.00 |           18.27 |   54,733,789.67 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          106.00 |            4.89 |  204,558,822.64 |
|               2 |          106.00 |            4.88 |  204,993,533.02 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       21,684.00 |        1,000.04 |      999,964.73 |
|               2 |       21,729.00 |          999.99 |    1,000,014.47 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            2.72 |  367,512,461.02 |
|               2 |           59.00 |            2.72 |  368,293,466.10 |


