# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/13/2022 10:15:25 PM_
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
|TotalBytesAllocated |           bytes |    2,490,816.00 |    2,490,789.33 |    2,490,776.00 |           23.09 |
|TotalCollections [Gen0] |     collections |           57.00 |           57.00 |           57.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          837.00 |          831.33 |          826.00 |            5.51 |
|[Counter] WordsChecked |      operations |      803,936.00 |      803,936.00 |      803,936.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,015,570.27 |    2,995,778.79 |    2,975,427.28 |       20,077.35 |
|TotalCollections [Gen0] |     collections |           69.01 |           68.56 |           68.09 |            0.46 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.03 |          999.85 |          999.66 |            0.19 |
|[Counter] WordsChecked |      operations |      973,321.37 |      966,928.19 |      960,364.60 |        6,480.06 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,490,776.00 |    3,015,570.27 |          331.61 |
|               2 |    2,490,776.00 |    2,975,427.28 |          336.09 |
|               3 |    2,490,816.00 |    2,996,338.81 |          333.74 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           57.00 |           69.01 |   14,490,733.33 |
|               2 |           57.00 |           68.09 |   14,686,235.09 |
|               3 |           57.00 |           68.57 |   14,583,973.68 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  825,971,800.00 |
|               2 |            0.00 |            0.00 |  837,115,400.00 |
|               3 |            0.00 |            0.00 |  831,286,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  825,971,800.00 |
|               2 |            0.00 |            0.00 |  837,115,400.00 |
|               3 |            0.00 |            0.00 |  831,286,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          826.00 |        1,000.03 |      999,965.86 |
|               2 |          837.00 |          999.86 |    1,000,137.87 |
|               3 |          831.00 |          999.66 |    1,000,344.77 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      803,936.00 |      973,321.37 |        1,027.41 |
|               2 |      803,936.00 |      960,364.60 |        1,041.27 |
|               3 |      803,936.00 |      967,098.59 |        1,034.02 |


