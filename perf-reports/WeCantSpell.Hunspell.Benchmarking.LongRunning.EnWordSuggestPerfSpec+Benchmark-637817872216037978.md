# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/2/2022 3:07:01 AM_
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
|TotalBytesAllocated |           bytes |    8,163,480.00 |    8,163,480.00 |    8,163,480.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,594.00 |        1,572.33 |        1,561.00 |           18.77 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,228,912.06 |    5,191,203.01 |    5,120,139.65 |       61,581.18 |
|TotalCollections [Gen0] |     collections |           16.01 |           15.90 |           15.68 |            0.19 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.86 |          999.76 |          999.67 |            0.10 |
|[Counter] SuggestionQueries |      operations |          128.10 |          127.18 |          125.44 |            1.51 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,163,480.00 |    5,228,912.06 |          191.24 |
|               2 |    8,163,480.00 |    5,224,557.32 |          191.40 |
|               3 |    8,163,480.00 |    5,120,139.65 |          195.31 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.01 |   62,448,784.00 |
|               2 |           25.00 |           16.00 |   62,500,836.00 |
|               3 |           25.00 |           15.68 |   63,775,448.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,561,219,600.00 |
|               2 |            0.00 |            0.00 |1,562,520,900.00 |
|               3 |            0.00 |            0.00 |1,594,386,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,561,219,600.00 |
|               2 |            0.00 |            0.00 |1,562,520,900.00 |
|               3 |            0.00 |            0.00 |1,594,386,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,561.00 |          999.86 |    1,000,140.68 |
|               2 |        1,562.00 |          999.67 |    1,000,333.48 |
|               3 |        1,594.00 |          999.76 |    1,000,242.28 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          128.10 |    7,806,098.00 |
|               2 |          200.00 |          128.00 |    7,812,604.50 |
|               3 |          200.00 |          125.44 |    7,971,931.00 |


