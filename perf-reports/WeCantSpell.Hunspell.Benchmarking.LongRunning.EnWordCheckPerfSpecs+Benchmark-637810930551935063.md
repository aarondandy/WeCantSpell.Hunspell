# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_2/22/2022 2:17:35 AM_
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
|TotalBytesAllocated |           bytes |    1,549,704.00 |    1,549,701.33 |    1,549,696.00 |            4.62 |
|TotalCollections [Gen0] |     collections |           74.00 |           74.00 |           74.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,101.00 |          960.00 |          881.00 |          122.41 |
|[Counter] WordsChecked |      operations |      919,968.00 |      919,968.00 |      919,968.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,759,795.02 |    1,631,146.48 |    1,406,795.41 |      194,990.80 |
|TotalCollections [Gen0] |     collections |           84.03 |           77.89 |           67.18 |            9.31 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.65 |        1,000.19 |          999.47 |            0.63 |
|[Counter] WordsChecked |      operations |    1,044,686.66 |      968,317.04 |      835,135.90 |      115,752.07 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,549,696.00 |    1,406,795.41 |          710.84 |
|               2 |    1,549,704.00 |    1,726,849.01 |          579.09 |
|               3 |    1,549,704.00 |    1,759,795.02 |          568.25 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           74.00 |           67.18 |   14,886,200.00 |
|               2 |           74.00 |           82.46 |   12,127,259.46 |
|               3 |           74.00 |           84.03 |   11,900,218.92 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,101,578,800.00 |
|               2 |            0.00 |            0.00 |  897,417,200.00 |
|               3 |            0.00 |            0.00 |  880,616,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,101,578,800.00 |
|               2 |            0.00 |            0.00 |  897,417,200.00 |
|               3 |            0.00 |            0.00 |  880,616,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,101.00 |          999.47 |    1,000,525.70 |
|               2 |          898.00 |        1,000.65 |      999,351.00 |
|               3 |          881.00 |        1,000.44 |      999,564.36 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      919,968.00 |      835,135.90 |        1,197.41 |
|               2 |      919,968.00 |    1,025,128.56 |          975.49 |
|               3 |      919,968.00 |    1,044,686.66 |          957.22 |


