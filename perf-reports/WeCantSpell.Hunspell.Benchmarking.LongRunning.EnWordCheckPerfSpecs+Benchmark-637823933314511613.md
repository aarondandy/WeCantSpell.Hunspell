# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/9/2022 3:28:51 AM_
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
|TotalBytesAllocated |           bytes |      547,280.00 |      547,053.33 |      546,856.00 |          213.52 |
|TotalCollections [Gen0] |     collections |           64.00 |           64.00 |           64.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,045.00 |          848.00 |          744.00 |          170.70 |
|[Counter] WordsChecked |      operations |      779,072.00 |      779,072.00 |      779,072.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      735,085.24 |      661,141.76 |      523,318.64 |      119,464.34 |
|TotalCollections [Gen0] |     collections |           85.96 |           77.34 |           61.25 |           13.95 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.67 |        1,000.00 |          999.31 |            0.68 |
|[Counter] WordsChecked |      operations |    1,046,419.25 |      941,511.68 |      745,539.78 |      169,857.73 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      546,856.00 |      523,318.64 |        1,910.88 |
|               2 |      547,280.00 |      735,085.24 |        1,360.39 |
|               3 |      547,024.00 |      725,021.39 |        1,379.27 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           64.00 |           61.25 |   16,327,767.19 |
|               2 |           64.00 |           85.96 |   11,633,004.69 |
|               3 |           64.00 |           84.83 |   11,788,962.50 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,044,977,100.00 |
|               2 |            0.00 |            0.00 |  744,512,300.00 |
|               3 |            0.00 |            0.00 |  754,493,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,044,977,100.00 |
|               2 |            0.00 |            0.00 |  744,512,300.00 |
|               3 |            0.00 |            0.00 |  754,493,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,045.00 |        1,000.02 |      999,978.09 |
|               2 |          744.00 |          999.31 |    1,000,688.58 |
|               3 |          755.00 |        1,000.67 |      999,329.27 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      779,072.00 |      745,539.78 |        1,341.31 |
|               2 |      779,072.00 |    1,046,419.25 |          955.64 |
|               3 |      779,072.00 |    1,032,576.02 |          968.45 |


