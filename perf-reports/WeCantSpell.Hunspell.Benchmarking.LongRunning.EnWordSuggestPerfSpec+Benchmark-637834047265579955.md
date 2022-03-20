# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/20/2022 8:25:26 PM_
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
|TotalBytesAllocated |           bytes |    3,632,784.00 |    3,628,586.67 |    3,626,488.00 |        3,635.00 |
|TotalCollections [Gen0] |     collections |            5.00 |            5.00 |            5.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,372.00 |        1,369.00 |        1,366.00 |            3.00 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,655,021.29 |    2,650,755.52 |    2,643,135.19 |        6,615.11 |
|TotalCollections [Gen0] |     collections |            3.66 |            3.65 |            3.64 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.19 |        1,000.08 |          999.97 |            0.11 |
|[Counter] SuggestionQueries |      operations |          146.42 |          146.10 |          145.77 |            0.33 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,626,488.00 |    2,643,135.19 |          378.34 |
|               2 |    3,632,784.00 |    2,654,110.10 |          376.77 |
|               3 |    3,626,488.00 |    2,655,021.29 |          376.64 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            5.00 |            3.64 |  274,408,060.00 |
|               2 |            5.00 |            3.65 |  273,747,800.00 |
|               3 |            5.00 |            3.66 |  273,179,580.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,372,040,300.00 |
|               2 |            0.00 |            0.00 |1,368,739,000.00 |
|               3 |            0.00 |            0.00 |1,365,897,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,372,040,300.00 |
|               2 |            0.00 |            0.00 |1,368,739,000.00 |
|               3 |            0.00 |            0.00 |1,365,897,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,372.00 |          999.97 |    1,000,029.37 |
|               2 |        1,369.00 |        1,000.19 |      999,809.35 |
|               3 |        1,366.00 |        1,000.07 |      999,925.26 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          145.77 |    6,860,201.50 |
|               2 |          200.00 |          146.12 |    6,843,695.00 |
|               3 |          200.00 |          146.42 |    6,829,489.50 |


