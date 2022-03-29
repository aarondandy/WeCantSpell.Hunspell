# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/29/2022 11:43:05 PM_
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
|TotalBytesAllocated |           bytes |    6,717,928.00 |    6,717,928.00 |    6,717,928.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           11.00 |           11.00 |           11.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          605.00 |          600.67 |          598.00 |            3.79 |
|[Counter] WordsChecked |      operations |      663,040.00 |      663,040.00 |      663,040.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   11,226,056.23 |   11,184,020.61 |   11,110,227.60 |       64,112.91 |
|TotalCollections [Gen0] |     collections |           18.38 |           18.31 |           18.19 |            0.10 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.56 |          999.97 |          999.29 |            0.64 |
|[Counter] WordsChecked |      operations |    1,107,979.17 |    1,103,830.38 |    1,096,547.23 |        6,327.76 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,717,928.00 |   11,226,056.23 |           89.08 |
|               2 |    6,717,928.00 |   11,215,777.99 |           89.16 |
|               3 |    6,717,928.00 |   11,110,227.60 |           90.01 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           11.00 |           18.38 |   54,402,072.73 |
|               2 |           11.00 |           18.36 |   54,451,927.27 |
|               3 |           11.00 |           18.19 |   54,969,236.36 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  598,422,800.00 |
|               2 |            0.00 |            0.00 |  598,971,200.00 |
|               3 |            0.00 |            0.00 |  604,661,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  598,422,800.00 |
|               2 |            0.00 |            0.00 |  598,971,200.00 |
|               3 |            0.00 |            0.00 |  604,661,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          598.00 |          999.29 |    1,000,707.02 |
|               2 |          599.00 |        1,000.05 |      999,951.92 |
|               3 |          605.00 |        1,000.56 |      999,440.66 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      663,040.00 |    1,107,979.17 |          902.54 |
|               2 |      663,040.00 |    1,106,964.74 |          903.37 |
|               3 |      663,040.00 |    1,096,547.23 |          911.95 |


