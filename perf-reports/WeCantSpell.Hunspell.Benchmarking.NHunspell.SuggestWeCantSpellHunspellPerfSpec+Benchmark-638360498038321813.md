# WeCantSpell.Hunspell.Benchmarking.NHunspell.SuggestWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project suggest English (US) words?__
_11/20/2023 04:03:23_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 6.2.9200.0
ProcessorCount=16
CLR=4.0.30319.42000,IsMono=False,MaxGcGeneration=2
```

### NBench Settings
```ini
RunMode=Throughput, TestMode=Measurement
NumberOfIterations=1, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,171,096.00 |    3,171,096.00 |    3,171,096.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          225.00 |          225.00 |          225.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            1.00 |            1.00 |            1.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] SuggestionQueries |      operations |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      294,636.82 |      294,636.82 |      294,636.82 |            0.00 |
|TotalCollections [Gen0] |     collections |           20.91 |           20.91 |           20.91 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.09 |            0.09 |            0.09 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] SuggestionQueries |      operations |           92.91 |           92.91 |           92.91 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,171,096.00 |      294,636.82 |        3,394.01 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          225.00 |           20.91 |   47,834,347.11 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            1.00 |            0.09 |10,762,728,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |10,762,728,100.00 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,000.00 |           92.91 |   10,762,728.10 |


