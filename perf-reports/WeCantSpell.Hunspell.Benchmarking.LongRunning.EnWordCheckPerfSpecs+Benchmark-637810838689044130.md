# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_2/21/2022 11:44:28 PM_
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
|TotalBytesAllocated |           bytes |    7,112,400.00 |    7,111,821.33 |    7,111,528.00 |          501.16 |
|TotalCollections [Gen0] |     collections |           72.00 |           72.00 |           72.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,047.00 |          981.67 |          891.00 |           81.03 |
|[Counter] WordsChecked |      operations |      903,392.00 |      903,392.00 |      903,392.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,986,467.04 |    7,280,733.07 |    6,792,144.39 |      626,071.65 |
|TotalCollections [Gen0] |     collections |           80.86 |           73.71 |           68.77 |            6.34 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.62 |        1,000.23 |          999.98 |            0.34 |
|[Counter] WordsChecked |      operations |    1,014,536.16 |      924,849.42 |      862,820.04 |       79,549.62 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,112,400.00 |    7,063,587.78 |          141.57 |
|               2 |    7,111,528.00 |    6,792,144.39 |          147.23 |
|               3 |    7,111,536.00 |    7,986,467.04 |          125.21 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           72.00 |           71.51 |   13,984,866.67 |
|               2 |           72.00 |           68.77 |   14,541,979.17 |
|               3 |           72.00 |           80.86 |   12,367,337.50 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,910,400.00 |
|               2 |            0.00 |            0.00 |1,047,022,500.00 |
|               3 |            0.00 |            0.00 |  890,448,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,910,400.00 |
|               2 |            0.00 |            0.00 |1,047,022,500.00 |
|               3 |            0.00 |            0.00 |  890,448,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,007.00 |        1,000.09 |      999,911.02 |
|               2 |        1,047.00 |          999.98 |    1,000,021.49 |
|               3 |          891.00 |        1,000.62 |      999,380.81 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      903,392.00 |      897,192.04 |        1,114.59 |
|               2 |      903,392.00 |      862,820.04 |        1,158.99 |
|               3 |      903,392.00 |    1,014,536.16 |          985.67 |


