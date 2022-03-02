# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/2/2022 3:06:44 AM_
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
|TotalBytesAllocated |           bytes |      178,232.00 |      178,120.00 |      178,064.00 |           96.99 |
|TotalCollections [Gen0] |     collections |           62.00 |           62.00 |           62.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          805.00 |          801.67 |          800.00 |            2.89 |
|[Counter] WordsChecked |      operations |      754,208.00 |      754,208.00 |      754,208.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      222,689.37 |      222,211.02 |      221,373.95 |          727.38 |
|TotalCollections [Gen0] |     collections |           77.54 |           77.35 |           77.01 |            0.30 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.49 |        1,000.10 |          999.85 |            0.34 |
|[Counter] WordsChecked |      operations |      943,223.24 |      940,902.54 |      936,767.85 |        3,589.70 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      178,064.00 |      222,569.73 |        4,492.97 |
|               2 |      178,064.00 |      222,689.37 |        4,490.56 |
|               3 |      178,232.00 |      221,373.95 |        4,517.24 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           62.00 |           77.50 |   12,903,820.97 |
|               2 |           62.00 |           77.54 |   12,896,888.71 |
|               3 |           62.00 |           77.01 |   12,985,762.90 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  800,036,900.00 |
|               2 |            0.00 |            0.00 |  799,607,100.00 |
|               3 |            0.00 |            0.00 |  805,117,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  800,036,900.00 |
|               2 |            0.00 |            0.00 |  799,607,100.00 |
|               3 |            0.00 |            0.00 |  805,117,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          800.00 |          999.95 |    1,000,046.12 |
|               2 |          800.00 |        1,000.49 |      999,508.88 |
|               3 |          805.00 |          999.85 |    1,000,145.71 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      754,208.00 |      942,716.52 |        1,060.76 |
|               2 |      754,208.00 |      943,223.24 |        1,060.19 |
|               3 |      754,208.00 |      936,767.85 |        1,067.50 |


