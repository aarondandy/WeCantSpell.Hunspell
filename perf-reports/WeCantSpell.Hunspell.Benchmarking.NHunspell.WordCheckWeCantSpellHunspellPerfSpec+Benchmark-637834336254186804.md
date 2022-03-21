# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/21/2022 04:27:05_
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
NumberOfIterations=3, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,622,160.00 |    1,622,160.00 |    1,622,160.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           63.00 |           63.00 |           63.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,028.00 |        1,023.33 |        1,019.00 |            4.51 |
|[Counter] _wordsChecked |      operations |      621,600.00 |      621,600.00 |      621,600.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,591,043.33 |    1,585,155.36 |    1,578,545.88 |        6,279.89 |
|TotalCollections [Gen0] |     collections |           61.79 |           61.56 |           61.31 |            0.24 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.36 |          999.98 |          999.45 |            0.47 |
|[Counter] _wordsChecked |      operations |      609,676.32 |      607,420.09 |      604,887.38 |        2,406.41 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,622,160.00 |    1,585,876.88 |          630.57 |
|               2 |    1,622,160.00 |    1,591,043.33 |          628.52 |
|               3 |    1,622,160.00 |    1,578,545.88 |          633.49 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           63.00 |           61.59 |   16,236,173.02 |
|               2 |           63.00 |           61.79 |   16,183,450.79 |
|               3 |           63.00 |           61.31 |   16,311,576.19 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,022,878,900.00 |
|               2 |            0.00 |            0.00 |1,019,557,400.00 |
|               3 |            0.00 |            0.00 |1,027,629,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,022,878,900.00 |
|               2 |            0.00 |            0.00 |1,019,557,400.00 |
|               3 |            0.00 |            0.00 |1,027,629,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,023.00 |        1,000.12 |      999,881.62 |
|               2 |        1,019.00 |          999.45 |    1,000,547.01 |
|               3 |        1,028.00 |        1,000.36 |      999,639.40 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      621,600.00 |      607,696.57 |        1,645.56 |
|               2 |      621,600.00 |      609,676.32 |        1,640.21 |
|               3 |      621,600.00 |      604,887.38 |        1,653.20 |


