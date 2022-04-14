# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_4/14/2022 12:58:11 PM_
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
|TotalBytesAllocated |           bytes |    7,242,288.00 |    7,242,288.00 |    7,242,288.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           12.00 |           12.00 |           12.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          771.00 |          766.33 |          763.00 |            4.16 |
|[Counter] WordsChecked |      operations |      878,528.00 |      878,528.00 |      878,528.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,496,916.84 |    9,456,494.77 |    9,400,424.24 |       50,113.49 |
|TotalCollections [Gen0] |     collections |           15.74 |           15.67 |           15.58 |            0.08 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.75 |        1,000.61 |        1,000.53 |            0.12 |
|[Counter] WordsChecked |      operations |    1,152,026.45 |    1,147,123.04 |    1,140,321.39 |        6,079.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,242,288.00 |    9,496,916.84 |          105.30 |
|               2 |    7,242,288.00 |    9,472,143.24 |          105.57 |
|               3 |    7,242,288.00 |    9,400,424.24 |          106.38 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           12.00 |           15.74 |   63,549,466.67 |
|               2 |           12.00 |           15.69 |   63,715,675.00 |
|               3 |           12.00 |           15.58 |   64,201,783.33 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  762,593,600.00 |
|               2 |            0.00 |            0.00 |  764,588,100.00 |
|               3 |            0.00 |            0.00 |  770,421,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  762,593,600.00 |
|               2 |            0.00 |            0.00 |  764,588,100.00 |
|               3 |            0.00 |            0.00 |  770,421,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          763.00 |        1,000.53 |      999,467.37 |
|               2 |          765.00 |        1,000.54 |      999,461.57 |
|               3 |          771.00 |        1,000.75 |      999,249.55 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      878,528.00 |    1,152,026.45 |          868.04 |
|               2 |      878,528.00 |    1,149,021.28 |          870.31 |
|               3 |      878,528.00 |    1,140,321.39 |          876.95 |


