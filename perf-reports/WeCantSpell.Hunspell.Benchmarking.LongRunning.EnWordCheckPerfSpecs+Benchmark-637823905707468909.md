# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/9/2022 2:42:50 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.3,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |      547,024.00 |      547,024.00 |      547,024.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           64.00 |           64.00 |           64.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,046.00 |          853.67 |          757.00 |          166.57 |
|[Counter] WordsChecked |      operations |      779,072.00 |      779,072.00 |      779,072.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      722,245.63 |      655,641.81 |      522,793.31 |      115,050.31 |
|TotalCollections [Gen0] |     collections |           84.50 |           76.71 |           61.17 |           13.46 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.30 |          999.82 |          999.48 |            0.43 |
|[Counter] WordsChecked |      operations |    1,028,622.78 |      933,765.57 |      744,562.64 |      163,854.75 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      547,024.00 |      522,793.31 |        1,912.80 |
|               2 |      547,024.00 |      722,245.63 |        1,384.57 |
|               3 |      547,024.00 |      721,886.49 |        1,385.26 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           64.00 |           61.17 |   16,349,195.31 |
|               2 |           64.00 |           84.50 |   11,834,270.31 |
|               3 |           64.00 |           84.46 |   11,840,157.81 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,046,348,500.00 |
|               2 |            0.00 |            0.00 |  757,393,300.00 |
|               3 |            0.00 |            0.00 |  757,770,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,046,348,500.00 |
|               2 |            0.00 |            0.00 |  757,393,300.00 |
|               3 |            0.00 |            0.00 |  757,770,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,046.00 |          999.67 |    1,000,333.17 |
|               2 |          757.00 |          999.48 |    1,000,519.55 |
|               3 |          758.00 |        1,000.30 |      999,696.70 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      779,072.00 |      744,562.64 |        1,343.07 |
|               2 |      779,072.00 |    1,028,622.78 |          972.17 |
|               3 |      779,072.00 |    1,028,111.30 |          972.66 |


