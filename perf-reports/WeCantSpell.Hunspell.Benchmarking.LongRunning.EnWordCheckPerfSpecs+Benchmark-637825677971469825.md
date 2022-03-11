# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/11/2022 3:56:37 AM_
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
|TotalBytesAllocated |           bytes |      590,696.00 |      590,621.33 |      590,472.00 |          129.33 |
|TotalCollections [Gen0] |     collections |           68.00 |           68.00 |           68.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,064.00 |          955.67 |          792.00 |          144.20 |
|[Counter] WordsChecked |      operations |      770,784.00 |      770,784.00 |      770,784.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      745,641.12 |      628,355.80 |      555,096.22 |      102,618.41 |
|TotalCollections [Gen0] |     collections |           85.84 |           72.34 |           63.93 |           11.80 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.25 |        1,000.04 |          999.75 |            0.26 |
|[Counter] WordsChecked |      operations |      972,967.90 |      820,016.90 |      724,605.54 |      133,806.10 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      590,696.00 |      584,330.07 |        1,711.36 |
|               2 |      590,472.00 |      555,096.22 |        1,801.49 |
|               3 |      590,696.00 |      745,641.12 |        1,341.13 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           68.00 |           67.27 |   14,866,094.12 |
|               2 |           68.00 |           63.93 |   15,643,075.00 |
|               3 |           68.00 |           85.84 |   11,649,982.35 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,010,894,400.00 |
|               2 |            0.00 |            0.00 |1,063,729,100.00 |
|               3 |            0.00 |            0.00 |  792,198,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,010,894,400.00 |
|               2 |            0.00 |            0.00 |1,063,729,100.00 |
|               3 |            0.00 |            0.00 |  792,198,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,011.00 |        1,000.10 |      999,895.55 |
|               2 |        1,064.00 |        1,000.25 |      999,745.39 |
|               3 |          792.00 |          999.75 |    1,000,251.01 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      770,784.00 |      762,477.27 |        1,311.51 |
|               2 |      770,784.00 |      724,605.54 |        1,380.06 |
|               3 |      770,784.00 |      972,967.90 |        1,027.78 |


