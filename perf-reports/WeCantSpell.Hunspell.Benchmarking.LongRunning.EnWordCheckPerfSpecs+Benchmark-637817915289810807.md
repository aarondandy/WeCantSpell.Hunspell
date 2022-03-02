# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/2/2022 4:18:48 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.2,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |      547,376.00 |      547,320.00 |      547,208.00 |           96.99 |
|TotalCollections [Gen0] |     collections |           64.00 |           64.00 |           64.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          770.00 |          768.33 |          766.00 |            2.08 |
|[Counter] WordsChecked |      operations |      779,072.00 |      779,072.00 |      779,072.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      715,320.25 |      712,633.58 |      710,861.83 |        2,365.83 |
|TotalCollections [Gen0] |     collections |           83.64 |           83.33 |           83.14 |            0.27 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,001.02 |        1,000.40 |          999.88 |            0.58 |
|[Counter] WordsChecked |      operations |    1,018,104.51 |    1,014,384.17 |    1,012,069.54 |        3,253.81 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      547,208.00 |      710,861.83 |        1,406.74 |
|               2 |      547,376.00 |      711,718.67 |        1,405.05 |
|               3 |      547,376.00 |      715,320.25 |        1,397.98 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           64.00 |           83.14 |   12,027,829.69 |
|               2 |           64.00 |           83.22 |   12,017,037.50 |
|               3 |           64.00 |           83.64 |   11,956,532.81 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  769,781,100.00 |
|               2 |            0.00 |            0.00 |  769,090,400.00 |
|               3 |            0.00 |            0.00 |  765,218,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  769,781,100.00 |
|               2 |            0.00 |            0.00 |  769,090,400.00 |
|               3 |            0.00 |            0.00 |  765,218,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          770.00 |        1,000.28 |      999,715.71 |
|               2 |          769.00 |          999.88 |    1,000,117.56 |
|               3 |          766.00 |        1,001.02 |      998,979.24 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      779,072.00 |    1,012,069.54 |          988.07 |
|               2 |      779,072.00 |    1,012,978.45 |          987.19 |
|               3 |      779,072.00 |    1,018,104.51 |          982.22 |


