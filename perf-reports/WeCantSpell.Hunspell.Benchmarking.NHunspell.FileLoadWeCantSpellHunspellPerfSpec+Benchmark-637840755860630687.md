# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/28/2022 14:46:26_
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
|TotalBytesAllocated |           bytes |   90,610,120.00 |   90,609,764.00 |   90,609,408.00 |          503.46 |
|TotalCollections [Gen0] |     collections |          486.00 |          484.00 |          482.00 |            2.83 |
|TotalCollections [Gen1] |     collections |          190.00 |          188.00 |          186.00 |            2.83 |
|TotalCollections [Gen2] |     collections |           46.00 |           45.00 |           44.00 |            1.41 |
|    Elapsed Time |              ms |       18,240.00 |       18,211.50 |       18,183.00 |           40.31 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,983,031.24 |    4,975,297.65 |    4,967,564.06 |       10,936.95 |
|TotalCollections [Gen0] |     collections |           26.64 |           26.58 |           26.51 |            0.10 |
|TotalCollections [Gen1] |     collections |           10.42 |           10.32 |           10.23 |            0.13 |
|TotalCollections [Gen2] |     collections |            2.52 |            2.47 |            2.42 |            0.07 |
|    Elapsed Time |              ms |          999.98 |          999.97 |          999.97 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.24 |            3.24 |            3.23 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   90,609,408.00 |    4,983,031.24 |          200.68 |
|               2 |   90,610,120.00 |    4,967,564.06 |          201.31 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          482.00 |           26.51 |   37,725,295.02 |
|               2 |          486.00 |           26.64 |   37,531,589.71 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          186.00 |           10.23 |   97,761,248.39 |
|               2 |          190.00 |           10.42 |   96,001,855.79 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           44.00 |            2.42 |  413,263,459.09 |
|               2 |           46.00 |            2.52 |  396,529,404.35 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,183.00 |          999.97 |    1,000,032.57 |
|               2 |       18,240.00 |          999.98 |    1,000,019.33 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.24 |  308,196,477.97 |
|               2 |           59.00 |            3.23 |  309,158,518.64 |


