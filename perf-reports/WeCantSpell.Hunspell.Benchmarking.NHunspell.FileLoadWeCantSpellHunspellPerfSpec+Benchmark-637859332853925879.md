# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_04/19/2022 02:48:05_
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
|TotalBytesAllocated |           bytes |   79,677,512.00 |   79,582,748.00 |   79,487,984.00 |      134,016.53 |
|TotalCollections [Gen0] |     collections |          392.00 |          391.00 |          390.00 |            1.41 |
|TotalCollections [Gen1] |     collections |          154.00 |          153.00 |          152.00 |            1.41 |
|TotalCollections [Gen2] |     collections |           39.00 |           39.00 |           39.00 |            0.00 |
|    Elapsed Time |              ms |       17,245.00 |       17,133.00 |       17,021.00 |          158.39 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,681,258.36 |    4,645,325.15 |    4,609,391.94 |       50,817.24 |
|TotalCollections [Gen0] |     collections |           22.91 |           22.82 |           22.73 |            0.13 |
|TotalCollections [Gen1] |     collections |            8.93 |            8.93 |            8.93 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.29 |            2.28 |            2.26 |            0.02 |
|    Elapsed Time |              ms |        1,000.03 |        1,000.02 |        1,000.01 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.47 |            3.44 |            3.42 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   79,487,984.00 |    4,609,391.94 |          216.95 |
|               2 |   79,677,512.00 |    4,681,258.36 |          213.62 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          392.00 |           22.73 |   43,991,804.59 |
|               2 |          390.00 |           22.91 |   43,642,392.05 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          154.00 |            8.93 |  111,979,138.96 |
|               2 |          152.00 |            8.93 |  111,977,190.13 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           39.00 |            2.26 |  442,174,035.90 |
|               2 |           39.00 |            2.29 |  436,423,920.51 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       17,245.00 |        1,000.01 |      999,987.67 |
|               2 |       17,021.00 |        1,000.03 |      999,972.56 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.42 |  292,284,532.20 |
|               2 |           59.00 |            3.47 |  288,483,608.47 |


