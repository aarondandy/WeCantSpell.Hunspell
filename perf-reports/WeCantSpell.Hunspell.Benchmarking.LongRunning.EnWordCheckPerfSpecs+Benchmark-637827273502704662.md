# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/13/2022 12:15:50 AM_
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
|TotalBytesAllocated |           bytes |    1,456,344.00 |    1,456,221.33 |    1,456,160.00 |          106.23 |
|TotalCollections [Gen0] |     collections |           65.00 |           65.00 |           65.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,054.00 |          883.00 |          794.00 |          148.13 |
|[Counter] WordsChecked |      operations |      787,360.00 |      787,360.00 |      787,360.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,833,809.47 |    1,677,364.92 |    1,380,504.75 |      257,213.34 |
|TotalCollections [Gen0] |     collections |           81.85 |           74.87 |           61.62 |           11.48 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.92 |          999.65 |          999.24 |            0.36 |
|[Counter] WordsChecked |      operations |      991,433.50 |      906,925.91 |      746,452.46 |      139,039.69 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,456,160.00 |    1,380,504.75 |          724.37 |
|               2 |    1,456,160.00 |    1,817,780.53 |          550.12 |
|               3 |    1,456,344.00 |    1,833,809.47 |          545.31 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           65.00 |           61.62 |   16,227,732.31 |
|               2 |           65.00 |           81.14 |   12,324,073.85 |
|               3 |           65.00 |           81.85 |   12,217,895.38 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,054,802,600.00 |
|               2 |            0.00 |            0.00 |  801,064,800.00 |
|               3 |            0.00 |            0.00 |  794,163,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,054,802,600.00 |
|               2 |            0.00 |            0.00 |  801,064,800.00 |
|               3 |            0.00 |            0.00 |  794,163,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,054.00 |          999.24 |    1,000,761.48 |
|               2 |          801.00 |          999.92 |    1,000,080.90 |
|               3 |          794.00 |          999.79 |    1,000,205.54 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      787,360.00 |      746,452.46 |        1,339.67 |
|               2 |      787,360.00 |      982,891.77 |        1,017.41 |
|               3 |      787,360.00 |      991,433.50 |        1,008.64 |


