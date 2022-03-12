# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/12/2022 4:17:11 AM_
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
|TotalBytesAllocated |           bytes |    4,083,640.00 |    4,083,640.00 |    4,083,640.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           71.00 |           71.00 |           71.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          793.00 |          791.67 |          791.00 |            1.15 |
|[Counter] WordsChecked |      operations |      762,496.00 |      762,496.00 |      762,496.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,166,955.19 |    5,158,448.95 |    5,150,329.35 |        8,319.66 |
|TotalCollections [Gen0] |     collections |           89.84 |           89.69 |           89.55 |            0.14 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.84 |        1,000.03 |          999.12 |            0.87 |
|[Counter] WordsChecked |      operations |      964,772.28 |      963,184.00 |      961,667.91 |        1,553.45 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,083,640.00 |    5,158,062.32 |          193.87 |
|               2 |    4,083,640.00 |    5,166,955.19 |          193.54 |
|               3 |    4,083,640.00 |    5,150,329.35 |          194.16 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           71.00 |           89.68 |   11,150,709.86 |
|               2 |           71.00 |           89.84 |   11,131,518.31 |
|               3 |           71.00 |           89.55 |   11,167,452.11 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  791,700,400.00 |
|               2 |            0.00 |            0.00 |  790,337,800.00 |
|               3 |            0.00 |            0.00 |  792,889,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  791,700,400.00 |
|               2 |            0.00 |            0.00 |  790,337,800.00 |
|               3 |            0.00 |            0.00 |  792,889,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          791.00 |          999.12 |    1,000,885.46 |
|               2 |          791.00 |        1,000.84 |      999,162.83 |
|               3 |          793.00 |        1,000.14 |      999,860.15 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      762,496.00 |      963,111.80 |        1,038.30 |
|               2 |      762,496.00 |      964,772.28 |        1,036.51 |
|               3 |      762,496.00 |      961,667.91 |        1,039.86 |


