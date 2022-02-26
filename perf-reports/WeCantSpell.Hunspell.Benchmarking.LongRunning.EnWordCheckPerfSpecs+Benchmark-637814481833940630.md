# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_2/26/2022 4:56:23 AM_
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
|TotalBytesAllocated |           bytes |      547,384.00 |      547,328.00 |      547,216.00 |           96.99 |
|TotalCollections [Gen0] |     collections |           64.00 |           64.00 |           64.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          732.00 |          731.00 |          730.00 |            1.00 |
|[Counter] WordsChecked |      operations |      779,072.00 |      779,072.00 |      779,072.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      750,131.12 |      748,793.77 |      747,155.74 |        1,510.31 |
|TotalCollections [Gen0] |     collections |           87.73 |           87.56 |           87.36 |            0.19 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.69 |        1,000.07 |          999.15 |            0.82 |
|[Counter] WordsChecked |      operations |    1,067,962.47 |    1,065,840.56 |    1,063,399.94 |        2,297.90 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      547,216.00 |      750,131.12 |        1,333.10 |
|               2 |      547,384.00 |      749,094.46 |        1,334.95 |
|               3 |      547,384.00 |      747,155.74 |        1,338.41 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           64.00 |           87.73 |   11,398,340.62 |
|               2 |           64.00 |           87.58 |   11,417,618.75 |
|               3 |           64.00 |           87.36 |   11,447,245.31 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  729,493,800.00 |
|               2 |            0.00 |            0.00 |  730,727,600.00 |
|               3 |            0.00 |            0.00 |  732,623,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  729,493,800.00 |
|               2 |            0.00 |            0.00 |  730,727,600.00 |
|               3 |            0.00 |            0.00 |  732,623,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          730.00 |        1,000.69 |      999,306.58 |
|               2 |          731.00 |        1,000.37 |      999,627.36 |
|               3 |          732.00 |          999.15 |    1,000,852.05 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      779,072.00 |    1,067,962.47 |          936.36 |
|               2 |      779,072.00 |    1,066,159.26 |          937.95 |
|               3 |      779,072.00 |    1,063,399.94 |          940.38 |


