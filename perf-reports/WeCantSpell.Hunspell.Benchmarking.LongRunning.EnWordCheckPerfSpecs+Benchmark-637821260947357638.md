# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/6/2022 1:14:54 AM_
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
|TotalBytesAllocated |           bytes |      178,232.00 |      178,178.67 |      178,072.00 |           92.38 |
|TotalCollections [Gen0] |     collections |           62.00 |           62.00 |           62.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          747.00 |          743.00 |          739.00 |            4.00 |
|[Counter] WordsChecked |      operations |      754,208.00 |      754,208.00 |      754,208.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      240,778.01 |      239,673.51 |      238,416.09 |        1,188.36 |
|TotalCollections [Gen0] |     collections |           83.83 |           83.40 |           82.94 |            0.45 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.77 |          999.41 |          999.23 |            0.31 |
|[Counter] WordsChecked |      operations |    1,019,793.69 |    1,014,509.56 |    1,008,883.48 |        5,463.13 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      178,232.00 |      239,826.43 |        4,169.68 |
|               2 |      178,232.00 |      238,416.09 |        4,194.35 |
|               3 |      178,072.00 |      240,778.01 |        4,153.20 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           62.00 |           83.43 |   11,986,625.81 |
|               2 |           62.00 |           82.94 |   12,057,532.26 |
|               3 |           62.00 |           83.83 |   11,928,535.48 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  743,170,800.00 |
|               2 |            0.00 |            0.00 |  747,567,000.00 |
|               3 |            0.00 |            0.00 |  739,569,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  743,170,800.00 |
|               2 |            0.00 |            0.00 |  747,567,000.00 |
|               3 |            0.00 |            0.00 |  739,569,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          743.00 |          999.77 |    1,000,229.88 |
|               2 |          747.00 |          999.24 |    1,000,759.04 |
|               3 |          739.00 |          999.23 |    1,000,770.23 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      754,208.00 |    1,014,851.50 |          985.37 |
|               2 |      754,208.00 |    1,008,883.48 |          991.19 |
|               3 |      754,208.00 |    1,019,793.69 |          980.59 |


