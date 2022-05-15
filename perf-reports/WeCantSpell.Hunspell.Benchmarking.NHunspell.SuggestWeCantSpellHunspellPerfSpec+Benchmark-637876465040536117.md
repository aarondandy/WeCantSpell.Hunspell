# WeCantSpell.Hunspell.Benchmarking.NHunspell.SuggestWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project suggest English (US) words?__
_05/08/2022 22:41:44_
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
|TotalBytesAllocated |           bytes |    2,377,352.00 |    2,377,352.00 |    2,377,352.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          224.00 |          224.00 |          224.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            1.00 |            1.00 |            1.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] SuggestionQueries |      operations |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      221,657.76 |      221,657.76 |      221,657.76 |            0.00 |
|TotalCollections [Gen0] |     collections |           20.89 |           20.89 |           20.89 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.09 |            0.09 |            0.09 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] SuggestionQueries |      operations |           93.24 |           93.24 |           93.24 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,377,352.00 |      221,657.76 |        4,511.46 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          224.00 |           20.89 |   47,880,925.89 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            1.00 |            0.09 |10,725,327,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |10,725,327,400.00 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,000.00 |           93.24 |   10,725,327.40 |


