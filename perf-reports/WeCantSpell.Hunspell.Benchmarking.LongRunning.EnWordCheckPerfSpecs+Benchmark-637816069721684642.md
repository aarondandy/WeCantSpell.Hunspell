# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_2/28/2022 1:02:52 AM_
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
|TotalBytesAllocated |           bytes |      924,568.00 |      924,512.00 |      924,400.00 |           96.99 |
|TotalCollections [Gen0] |     collections |           66.00 |           66.00 |           66.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          788.00 |          782.00 |          778.00 |            5.29 |
|[Counter] WordsChecked |      operations |      803,936.00 |      803,936.00 |      803,936.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,187,341.83 |    1,181,749.56 |    1,173,083.07 |        7,610.27 |
|TotalCollections [Gen0] |     collections |           84.76 |           84.36 |           83.74 |            0.55 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.81 |          999.56 |          999.12 |            0.38 |
|[Counter] WordsChecked |      operations |    1,032,424.70 |    1,027,624.49 |    1,020,026.34 |        6,655.92 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      924,568.00 |    1,187,341.83 |          842.22 |
|               2 |      924,568.00 |    1,173,083.07 |          852.45 |
|               3 |      924,400.00 |    1,184,823.80 |          844.01 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           66.00 |           84.76 |   11,798,292.42 |
|               2 |           66.00 |           83.74 |   11,941,700.00 |
|               3 |           66.00 |           84.59 |   11,821,218.18 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  778,687,300.00 |
|               2 |            0.00 |            0.00 |  788,152,200.00 |
|               3 |            0.00 |            0.00 |  780,200,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  778,687,300.00 |
|               2 |            0.00 |            0.00 |  788,152,200.00 |
|               3 |            0.00 |            0.00 |  780,200,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          778.00 |          999.12 |    1,000,883.42 |
|               2 |          788.00 |          999.81 |    1,000,193.15 |
|               3 |          780.00 |          999.74 |    1,000,256.92 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      803,936.00 |    1,032,424.70 |          968.59 |
|               2 |      803,936.00 |    1,020,026.34 |          980.37 |
|               3 |      803,936.00 |    1,030,422.44 |          970.48 |


