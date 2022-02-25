# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_02/25/2022 03:56:37_
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
|TotalBytesAllocated |           bytes |  127,179,856.00 |   78,987,716.00 |   30,795,576.00 |   68,153,977.99 |
|TotalCollections [Gen0] |     collections |        1,215.00 |        1,214.50 |        1,214.00 |            0.71 |
|TotalCollections [Gen1] |     collections |          396.00 |          395.50 |          395.00 |            0.71 |
|TotalCollections [Gen2] |     collections |          106.00 |          105.00 |          104.00 |            1.41 |
|    Elapsed Time |              ms |       21,122.00 |       21,060.00 |       20,998.00 |           87.68 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,056,781.05 |    3,757,383.50 |    1,457,985.95 |    3,251,839.20 |
|TotalCollections [Gen0] |     collections |           57.82 |           57.67 |           57.52 |            0.21 |
|TotalCollections [Gen1] |     collections |           18.81 |           18.78 |           18.75 |            0.04 |
|TotalCollections [Gen2] |     collections |            5.05 |            4.99 |            4.92 |            0.09 |
|    Elapsed Time |              ms |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] FilePairsLoaded |      operations |            2.81 |            2.80 |            2.79 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,795,576.00 |    1,457,985.95 |          685.88 |
|               2 |  127,179,856.00 |    6,056,781.05 |          165.10 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,215.00 |           57.52 |   17,384,360.33 |
|               2 |        1,214.00 |           57.82 |   17,296,481.55 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          396.00 |           18.75 |   53,338,378.28 |
|               2 |          395.00 |           18.81 |   53,159,312.91 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          104.00 |            4.92 |  203,096,132.69 |
|               2 |          106.00 |            5.05 |  198,093,666.04 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       21,122.00 |        1,000.00 |      999,999.90 |
|               2 |       20,998.00 |        1,000.00 |      999,996.60 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            2.79 |  357,999,962.71 |
|               2 |           59.00 |            2.81 |  355,897,094.92 |


