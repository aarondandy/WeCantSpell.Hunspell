# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/12/2022 5:04:29 AM_
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
|TotalBytesAllocated |           bytes |    4,085,560.00 |    4,085,501.33 |    4,085,472.00 |           50.81 |
|TotalCollections [Gen0] |     collections |           64.00 |           64.00 |           64.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          791.00 |          789.67 |          788.00 |            1.53 |
|[Counter] WordsChecked |      operations |      779,072.00 |      779,072.00 |      779,072.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,179,967.54 |    5,172,797.65 |    5,165,557.54 |        7,205.26 |
|TotalCollections [Gen0] |     collections |           81.15 |           81.03 |           80.92 |            0.11 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.25 |          999.82 |          999.10 |            0.63 |
|[Counter] WordsChecked |      operations |      987,784.93 |      986,410.60 |      985,037.04 |        1,373.95 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,085,472.00 |    5,179,967.54 |          193.05 |
|               2 |    4,085,560.00 |    5,172,867.86 |          193.32 |
|               3 |    4,085,472.00 |    5,165,557.54 |          193.59 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           64.00 |           81.15 |   12,323,532.81 |
|               2 |           64.00 |           81.03 |   12,340,712.50 |
|               3 |           64.00 |           80.92 |   12,357,910.94 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  788,706,100.00 |
|               2 |            0.00 |            0.00 |  789,805,600.00 |
|               3 |            0.00 |            0.00 |  790,906,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  788,706,100.00 |
|               2 |            0.00 |            0.00 |  789,805,600.00 |
|               3 |            0.00 |            0.00 |  790,906,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          788.00 |          999.10 |    1,000,896.07 |
|               2 |          790.00 |        1,000.25 |      999,753.92 |
|               3 |          791.00 |        1,000.12 |      999,881.54 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      779,072.00 |      987,784.93 |        1,012.37 |
|               2 |      779,072.00 |      986,409.82 |        1,013.78 |
|               3 |      779,072.00 |      985,037.04 |        1,015.19 |


