# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/6/2022 10:59:21 PM_
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
|TotalBytesAllocated |           bytes |    2,848,840.00 |    2,848,418.67 |    2,848,208.00 |          364.89 |
|TotalCollections [Gen0] |     collections |           61.00 |           61.00 |           61.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          730.00 |          723.67 |          716.00 |            7.09 |
|[Counter] WordsChecked |      operations |      745,920.00 |      745,920.00 |      745,920.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,973,279.42 |    3,936,127.52 |    3,903,125.80 |       35,260.47 |
|TotalCollections [Gen0] |     collections |           85.10 |           84.29 |           83.57 |            0.76 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.87 |          999.95 |          998.83 |            1.04 |
|[Counter] WordsChecked |      operations |    1,040,566.06 |    1,030,760.73 |    1,021,966.69 |        9,340.83 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,848,208.00 |    3,973,279.42 |          251.68 |
|               2 |    2,848,208.00 |    3,931,977.34 |          254.32 |
|               3 |    2,848,840.00 |    3,903,125.80 |          256.20 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           61.00 |           85.10 |   11,751,485.25 |
|               2 |           61.00 |           84.21 |   11,874,924.59 |
|               3 |           61.00 |           83.57 |   11,965,357.38 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  716,840,600.00 |
|               2 |            0.00 |            0.00 |  724,370,400.00 |
|               3 |            0.00 |            0.00 |  729,886,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  716,840,600.00 |
|               2 |            0.00 |            0.00 |  724,370,400.00 |
|               3 |            0.00 |            0.00 |  729,886,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          716.00 |          998.83 |    1,001,174.02 |
|               2 |          725.00 |        1,000.87 |      999,131.59 |
|               3 |          730.00 |        1,000.16 |      999,844.93 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      745,920.00 |    1,040,566.06 |          961.02 |
|               2 |      745,920.00 |    1,029,749.42 |          971.11 |
|               3 |      745,920.00 |    1,021,966.69 |          978.51 |


