# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/24/2022 04:42:39_
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
|TotalBytesAllocated |           bytes |  123,921,568.00 |  123,912,940.00 |  123,904,312.00 |       12,201.83 |
|TotalCollections [Gen0] |     collections |          478.00 |          478.00 |          478.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          181.00 |          181.00 |          181.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           40.00 |           40.00 |           40.00 |            0.00 |
|    Elapsed Time |              ms |       18,099.00 |       18,087.00 |       18,075.00 |           16.97 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,855,821.43 |    6,850,885.76 |    6,845,950.10 |        6,980.09 |
|TotalCollections [Gen0] |     collections |           26.44 |           26.43 |           26.41 |            0.02 |
|TotalCollections [Gen1] |     collections |           10.01 |           10.01 |           10.00 |            0.01 |
|TotalCollections [Gen2] |     collections |            2.21 |            2.21 |            2.21 |            0.00 |
|    Elapsed Time |              ms |        1,000.00 |          999.99 |          999.98 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.26 |            3.26 |            3.26 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  123,904,312.00 |    6,845,950.10 |          146.07 |
|               2 |  123,921,568.00 |    6,855,821.43 |          145.86 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          478.00 |           26.41 |   37,863,852.09 |
|               2 |          478.00 |           26.44 |   37,814,599.58 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          181.00 |           10.00 |   99,994,040.33 |
|               2 |          181.00 |           10.01 |   99,863,970.17 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           40.00 |            2.21 |  452,473,032.50 |
|               2 |           40.00 |            2.21 |  451,884,465.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,099.00 |        1,000.00 |      999,995.65 |
|               2 |       18,075.00 |          999.98 |    1,000,020.95 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.26 |  306,761,377.97 |
|               2 |           59.00 |            3.26 |  306,362,349.15 |


