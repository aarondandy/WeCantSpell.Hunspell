# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/11/2022 1:46:56 AM_
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
|TotalBytesAllocated |           bytes |    2,153,288.00 |    2,152,845.33 |    2,152,624.00 |          383.36 |
|TotalCollections [Gen0] |     collections |           67.00 |           67.00 |           67.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          794.00 |          788.00 |          783.00 |            5.57 |
|[Counter] WordsChecked |      operations |      803,936.00 |      803,936.00 |      803,936.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,751,167.27 |    2,732,789.57 |    2,709,749.60 |       21,098.78 |
|TotalCollections [Gen0] |     collections |           85.60 |           85.05 |           84.34 |            0.65 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.81 |        1,000.24 |          999.50 |            0.67 |
|[Counter] WordsChecked |      operations |    1,027,155.87 |    1,020,503.61 |    1,012,004.54 |        7,742.66 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,152,624.00 |    2,737,451.84 |          365.30 |
|               2 |    2,152,624.00 |    2,709,749.60 |          369.04 |
|               3 |    2,153,288.00 |    2,751,167.27 |          363.48 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           67.00 |           85.20 |   11,736,723.88 |
|               2 |           67.00 |           84.34 |   11,856,710.45 |
|               3 |           67.00 |           85.60 |   11,681,814.93 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  786,360,500.00 |
|               2 |            0.00 |            0.00 |  794,399,600.00 |
|               3 |            0.00 |            0.00 |  782,681,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  786,360,500.00 |
|               2 |            0.00 |            0.00 |  794,399,600.00 |
|               3 |            0.00 |            0.00 |  782,681,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          787.00 |        1,000.81 |      999,187.42 |
|               2 |          794.00 |          999.50 |    1,000,503.27 |
|               3 |          783.00 |        1,000.41 |      999,593.36 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      803,936.00 |    1,022,350.44 |          978.14 |
|               2 |      803,936.00 |    1,012,004.54 |          988.14 |
|               3 |      803,936.00 |    1,027,155.87 |          973.56 |


