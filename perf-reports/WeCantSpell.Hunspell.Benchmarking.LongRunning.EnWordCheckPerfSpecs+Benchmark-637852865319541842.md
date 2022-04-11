# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_4/11/2022 3:08:51 PM_
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
|TotalBytesAllocated |           bytes |    7,954,856.00 |    7,954,856.00 |    7,954,856.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           11.00 |           11.00 |           11.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          603.00 |          602.33 |          602.00 |            0.58 |
|[Counter] WordsChecked |      operations |      671,328.00 |      671,328.00 |      671,328.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   13,214,896.04 |   13,206,859.56 |   13,193,760.56 |       11,441.11 |
|TotalCollections [Gen0] |     collections |           18.27 |           18.26 |           18.24 |            0.02 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.12 |        1,000.01 |          999.84 |            0.15 |
|[Counter] WordsChecked |      operations |    1,115,234.48 |    1,114,556.27 |    1,113,450.81 |          965.54 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,954,856.00 |   13,214,896.04 |           75.67 |
|               2 |    7,954,856.00 |   13,193,760.56 |           75.79 |
|               3 |    7,954,856.00 |   13,211,922.07 |           75.69 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           11.00 |           18.27 |   54,723,754.55 |
|               2 |           11.00 |           18.24 |   54,811,418.18 |
|               3 |           11.00 |           18.27 |   54,736,072.73 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  601,961,300.00 |
|               2 |            0.00 |            0.00 |  602,925,600.00 |
|               3 |            0.00 |            0.00 |  602,096,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  601,961,300.00 |
|               2 |            0.00 |            0.00 |  602,925,600.00 |
|               3 |            0.00 |            0.00 |  602,096,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          602.00 |        1,000.06 |      999,935.71 |
|               2 |          603.00 |        1,000.12 |      999,876.62 |
|               3 |          602.00 |          999.84 |    1,000,160.80 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      671,328.00 |    1,115,234.48 |          896.67 |
|               2 |      671,328.00 |    1,113,450.81 |          898.11 |
|               3 |      671,328.00 |    1,114,983.50 |          896.87 |


