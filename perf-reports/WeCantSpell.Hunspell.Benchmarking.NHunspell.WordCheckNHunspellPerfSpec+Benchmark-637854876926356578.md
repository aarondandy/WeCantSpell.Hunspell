# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_04/13/2022 23:01:32_
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
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          995.00 |          991.00 |          985.00 |            5.29 |
|[Counter] _wordsChecked |      operations |    1,193,472.00 |    1,193,472.00 |    1,193,472.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.03 |          999.87 |          999.74 |            0.15 |
|[Counter] _wordsChecked |      operations |    1,211,439.96 |    1,204,174.58 |    1,199,157.32 |        6,442.54 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  985,168,100.00 |
|               2 |            0.00 |            0.00 |  995,258,900.00 |
|               3 |            0.00 |            0.00 |  992,965,900.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  985,168,100.00 |
|               2 |            0.00 |            0.00 |  995,258,900.00 |
|               3 |            0.00 |            0.00 |  992,965,900.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  985,168,100.00 |
|               2 |            0.00 |            0.00 |  995,258,900.00 |
|               3 |            0.00 |            0.00 |  992,965,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  985,168,100.00 |
|               2 |            0.00 |            0.00 |  995,258,900.00 |
|               3 |            0.00 |            0.00 |  992,965,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          985.00 |          999.83 |    1,000,170.66 |
|               2 |          995.00 |          999.74 |    1,000,260.20 |
|               3 |          993.00 |        1,000.03 |      999,965.66 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,193,472.00 |    1,211,439.96 |          825.46 |
|               2 |    1,193,472.00 |    1,199,157.32 |          833.92 |
|               3 |    1,193,472.00 |    1,201,926.47 |          832.00 |


