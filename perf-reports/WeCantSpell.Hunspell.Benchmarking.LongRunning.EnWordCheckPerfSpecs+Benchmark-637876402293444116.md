# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_5/8/2022 8:57:09 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.4,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    5,751,288.00 |    5,751,269.33 |    5,751,232.00 |           32.33 |
|TotalCollections [Gen0] |     collections |           10.00 |           10.00 |           10.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          791.00 |          789.67 |          787.00 |            2.31 |
|[Counter] WordsChecked |      operations |      986,272.00 |      986,272.00 |      986,272.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,305,066.98 |    7,283,628.40 |    7,271,745.20 |       18,602.80 |
|TotalCollections [Gen0] |     collections |           12.70 |           12.66 |           12.64 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.45 |        1,000.06 |          999.62 |            0.42 |
|[Counter] WordsChecked |      operations |    1,252,725.13 |    1,249,052.74 |    1,247,010.88 |        3,187.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,751,232.00 |    7,274,073.00 |          137.47 |
|               2 |    5,751,288.00 |    7,305,066.98 |          136.89 |
|               3 |    5,751,288.00 |    7,271,745.20 |          137.52 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           10.00 |           12.65 |   79,064,810.00 |
|               2 |           10.00 |           12.70 |   78,730,120.00 |
|               3 |           10.00 |           12.64 |   79,090,890.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  790,648,100.00 |
|               2 |            0.00 |            0.00 |  787,301,200.00 |
|               3 |            0.00 |            0.00 |  790,908,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  790,648,100.00 |
|               2 |            0.00 |            0.00 |  787,301,200.00 |
|               3 |            0.00 |            0.00 |  790,908,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          791.00 |        1,000.45 |      999,555.12 |
|               2 |          787.00 |          999.62 |    1,000,382.72 |
|               3 |          791.00 |        1,000.12 |      999,884.83 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      986,272.00 |    1,247,422.21 |          801.65 |
|               2 |      986,272.00 |    1,252,725.13 |          798.26 |
|               3 |      986,272.00 |    1,247,010.88 |          801.92 |


