# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/1/2022 5:23:12 AM_
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
|    Elapsed Time |              ms |        1,604.00 |        1,587.33 |        1,578.00 |           14.47 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,173,492.24 |    5,144,504.32 |    5,091,565.45 |       45,915.53 |
|TotalCollections [Gen0] |     collections |           15.84 |           15.75 |           15.59 |            0.14 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.42 |        1,000.26 |        1,000.04 |            0.20 |
|[Counter] SuggestionQueries |      operations |          126.75 |          126.04 |          124.74 |            1.12 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,163,480.00 |    5,168,455.27 |          193.48 |
|               2 |    8,163,480.00 |    5,173,492.24 |          193.29 |
|               3 |    8,163,480.00 |    5,091,565.45 |          196.40 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           15.83 |   63,179,264.00 |
|               2 |           25.00 |           15.84 |   63,117,752.00 |
|               3 |           25.00 |           15.59 |   64,133,360.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,579,481,600.00 |
|               2 |            0.00 |            0.00 |1,577,943,800.00 |
|               3 |            0.00 |            0.00 |1,603,334,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,579,481,600.00 |
|               2 |            0.00 |            0.00 |1,577,943,800.00 |
|               3 |            0.00 |            0.00 |1,603,334,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,580.00 |        1,000.33 |      999,671.90 |
|               2 |        1,578.00 |        1,000.04 |      999,964.39 |
|               3 |        1,604.00 |        1,000.42 |      999,584.79 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          126.62 |    7,897,408.00 |
|               2 |          200.00 |          126.75 |    7,889,719.00 |
|               3 |          200.00 |          124.74 |    8,016,670.00 |


