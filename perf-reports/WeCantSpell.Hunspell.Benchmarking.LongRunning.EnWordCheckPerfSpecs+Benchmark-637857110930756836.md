# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_4/16/2022 1:04:53 PM_
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
|TotalBytesAllocated |           bytes |    8,266,288.00 |    8,266,288.00 |    8,266,288.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           12.00 |           12.00 |           12.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          770.00 |          768.67 |          767.00 |            1.53 |
|[Counter] WordsChecked |      operations |      886,816.00 |      886,816.00 |      886,816.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   10,777,607.40 |   10,753,070.51 |   10,732,343.31 |       22,871.26 |
|TotalCollections [Gen0] |     collections |           15.65 |           15.61 |           15.58 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.02 |          999.91 |          999.71 |            0.17 |
|[Counter] WordsChecked |      operations |    1,156,232.97 |    1,153,600.62 |    1,151,376.99 |        2,453.65 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,266,288.00 |   10,777,607.40 |           92.78 |
|               2 |    8,266,288.00 |   10,732,343.31 |           93.18 |
|               3 |    8,266,288.00 |   10,749,260.84 |           93.03 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           12.00 |           15.65 |   63,915,608.33 |
|               2 |           12.00 |           15.58 |   64,185,175.00 |
|               3 |           12.00 |           15.60 |   64,084,158.33 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  766,987,300.00 |
|               2 |            0.00 |            0.00 |  770,222,100.00 |
|               3 |            0.00 |            0.00 |  769,009,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  766,987,300.00 |
|               2 |            0.00 |            0.00 |  770,222,100.00 |
|               3 |            0.00 |            0.00 |  769,009,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          767.00 |        1,000.02 |      999,983.44 |
|               2 |          770.00 |          999.71 |    1,000,288.44 |
|               3 |          769.00 |          999.99 |    1,000,012.87 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      886,816.00 |    1,156,232.97 |          864.88 |
|               2 |      886,816.00 |    1,151,376.99 |          868.53 |
|               3 |      886,816.00 |    1,153,191.92 |          867.16 |


