# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/9/2022 2:41:58 PM_
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
|TotalBytesAllocated |           bytes |    1,401,312.00 |    1,401,312.00 |    1,401,312.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            2.00 |            2.00 |            2.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,434.00 |        1,313.33 |        1,248.00 |          104.62 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,122,611.83 |    1,071,277.33 |      977,362.26 |       81,450.53 |
|TotalCollections [Gen0] |     collections |            1.60 |            1.53 |            1.39 |            0.12 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.16 |          999.97 |          999.79 |            0.19 |
|[Counter] SuggestionQueries |      operations |          160.22 |          152.90 |          139.49 |           11.62 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,401,312.00 |      977,362.26 |        1,023.16 |
|               2 |    1,401,312.00 |    1,122,611.83 |          890.78 |
|               3 |    1,401,312.00 |    1,113,857.91 |          897.78 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            2.00 |            1.39 |  716,884,650.00 |
|               2 |            2.00 |            1.60 |  624,130,250.00 |
|               3 |            2.00 |            1.59 |  629,035,350.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,433,769,300.00 |
|               2 |            0.00 |            0.00 |1,248,260,500.00 |
|               3 |            0.00 |            0.00 |1,258,070,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,433,769,300.00 |
|               2 |            0.00 |            0.00 |1,248,260,500.00 |
|               3 |            0.00 |            0.00 |1,258,070,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,434.00 |        1,000.16 |      999,839.12 |
|               2 |        1,248.00 |          999.79 |    1,000,208.73 |
|               3 |        1,258.00 |          999.94 |    1,000,056.20 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          139.49 |    7,168,846.50 |
|               2 |          200.00 |          160.22 |    6,241,302.50 |
|               3 |          200.00 |          158.97 |    6,290,353.50 |


