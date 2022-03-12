# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/12/2022 2:46:43 AM_
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
|TotalBytesAllocated |           bytes |      590,696.00 |      590,546.67 |      590,472.00 |          129.33 |
|TotalCollections [Gen0] |     collections |           68.00 |           68.00 |           68.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,075.00 |          897.00 |          805.00 |          154.18 |
|[Counter] WordsChecked |      operations |      770,784.00 |      770,784.00 |      770,784.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      733,199.48 |      670,002.29 |      549,168.74 |      104,681.85 |
|TotalCollections [Gen0] |     collections |           84.40 |           77.15 |           63.24 |           12.05 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.80 |          999.47 |          999.20 |            0.31 |
|[Counter] WordsChecked |      operations |      956,733.12 |      874,479.42 |      716,868.00 |      136,539.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      590,472.00 |      549,168.74 |        1,820.93 |
|               2 |      590,696.00 |      733,199.48 |        1,363.89 |
|               3 |      590,472.00 |      727,638.65 |        1,374.31 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           68.00 |           63.24 |   15,811,919.12 |
|               2 |           68.00 |           84.40 |   11,847,670.59 |
|               3 |           68.00 |           83.80 |   11,933,686.76 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,075,210,500.00 |
|               2 |            0.00 |            0.00 |  805,641,600.00 |
|               3 |            0.00 |            0.00 |  811,490,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,075,210,500.00 |
|               2 |            0.00 |            0.00 |  805,641,600.00 |
|               3 |            0.00 |            0.00 |  811,490,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,075.00 |          999.80 |    1,000,195.81 |
|               2 |          805.00 |          999.20 |    1,000,797.02 |
|               3 |          811.00 |          999.40 |    1,000,605.06 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      770,784.00 |      716,868.00 |        1,394.96 |
|               2 |      770,784.00 |      956,733.12 |        1,045.22 |
|               3 |      770,784.00 |      949,837.13 |        1,052.81 |


