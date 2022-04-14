# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_4/14/2022 12:41:03 AM_
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
|TotalBytesAllocated |           bytes |    4,186,952.00 |    4,186,952.00 |    4,186,952.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           15.00 |           15.00 |           15.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          784.00 |          782.33 |          781.00 |            1.53 |
|[Counter] WordsChecked |      operations |      870,240.00 |      870,240.00 |      870,240.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,360,480.78 |    5,352,951.97 |    5,339,944.89 |       11,311.02 |
|TotalCollections [Gen0] |     collections |           19.20 |           19.18 |           19.13 |            0.04 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.80 |        1,000.20 |          999.90 |            0.52 |
|[Counter] WordsChecked |      operations |    1,114,152.92 |    1,112,588.09 |    1,109,884.62 |        2,350.95 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,186,952.00 |    5,360,480.78 |          186.55 |
|               2 |    4,186,952.00 |    5,339,944.89 |          187.27 |
|               3 |    4,186,952.00 |    5,358,430.24 |          186.62 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           15.00 |           19.20 |   52,071,846.67 |
|               2 |           15.00 |           19.13 |   52,272,100.00 |
|               3 |           15.00 |           19.20 |   52,091,773.33 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  781,077,700.00 |
|               2 |            0.00 |            0.00 |  784,081,500.00 |
|               3 |            0.00 |            0.00 |  781,376,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  781,077,700.00 |
|               2 |            0.00 |            0.00 |  784,081,500.00 |
|               3 |            0.00 |            0.00 |  781,376,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          781.00 |          999.90 |    1,000,099.49 |
|               2 |          784.00 |          999.90 |    1,000,103.95 |
|               3 |          782.00 |        1,000.80 |      999,202.81 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      870,240.00 |    1,114,152.92 |          897.54 |
|               2 |      870,240.00 |    1,109,884.62 |          900.99 |
|               3 |      870,240.00 |    1,113,726.72 |          897.89 |


