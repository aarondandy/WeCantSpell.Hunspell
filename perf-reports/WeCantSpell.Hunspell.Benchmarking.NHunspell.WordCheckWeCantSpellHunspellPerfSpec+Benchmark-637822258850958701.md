# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/07/2022 04:58:05_
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
|TotalBytesAllocated |           bytes |    4,761,320.00 |    4,761,320.00 |    4,761,320.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           79.00 |           79.00 |           79.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,019.00 |        1,016.67 |        1,013.00 |            3.21 |
|[Counter] _wordsChecked |      operations |      687,904.00 |      687,904.00 |      687,904.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,700,689.57 |    4,684,433.24 |    4,672,821.89 |       14,502.88 |
|TotalCollections [Gen0] |     collections |           77.99 |           77.72 |           77.53 |            0.24 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.57 |        1,000.24 |        1,000.06 |            0.28 |
|[Counter] _wordsChecked |      operations |      679,144.26 |      676,795.59 |      675,118.01 |        2,095.34 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,761,320.00 |    4,672,821.89 |          214.00 |
|               2 |    4,761,320.00 |    4,700,689.57 |          212.73 |
|               3 |    4,761,320.00 |    4,679,788.26 |          213.68 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           79.00 |           77.53 |   12,897,960.76 |
|               2 |           79.00 |           77.99 |   12,821,496.20 |
|               3 |           79.00 |           77.65 |   12,878,760.76 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,018,938,900.00 |
|               2 |            0.00 |            0.00 |1,012,898,200.00 |
|               3 |            0.00 |            0.00 |1,017,422,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,018,938,900.00 |
|               2 |            0.00 |            0.00 |1,012,898,200.00 |
|               3 |            0.00 |            0.00 |1,017,422,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,019.00 |        1,000.06 |      999,940.04 |
|               2 |        1,013.00 |        1,000.10 |      999,899.51 |
|               3 |        1,018.00 |        1,000.57 |      999,432.32 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      687,904.00 |      675,118.01 |        1,481.22 |
|               2 |      687,904.00 |      679,144.26 |        1,472.44 |
|               3 |      687,904.00 |      676,124.49 |        1,479.02 |


