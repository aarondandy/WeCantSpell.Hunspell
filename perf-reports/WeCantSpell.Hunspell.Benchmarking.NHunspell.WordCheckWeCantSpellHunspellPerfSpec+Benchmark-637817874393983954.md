# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/02/2022 03:10:39_
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
NumberOfIterations=3, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,300,376.00 |    6,300,376.00 |    6,300,376.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           72.00 |           72.00 |           72.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          998.00 |          992.00 |          986.00 |            6.00 |
|[Counter] _wordsChecked |      operations |      629,888.00 |      629,888.00 |      629,888.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,390,095.50 |    6,352,639.63 |    6,314,569.26 |       37,766.87 |
|TotalCollections [Gen0] |     collections |           73.03 |           72.60 |           72.16 |            0.43 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.33 |        1,000.20 |        1,000.04 |            0.15 |
|[Counter] _wordsChecked |      operations |      638,857.82 |      635,113.12 |      631,306.99 |        3,775.79 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,300,376.00 |    6,353,254.13 |          157.40 |
|               2 |    6,300,376.00 |    6,314,569.26 |          158.36 |
|               3 |    6,300,376.00 |    6,390,095.50 |          156.49 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           72.00 |           72.60 |   13,773,291.67 |
|               2 |           72.00 |           72.16 |   13,857,670.83 |
|               3 |           72.00 |           73.03 |   13,693,883.33 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  991,677,000.00 |
|               2 |            0.00 |            0.00 |  997,752,300.00 |
|               3 |            0.00 |            0.00 |  985,959,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  991,677,000.00 |
|               2 |            0.00 |            0.00 |  997,752,300.00 |
|               3 |            0.00 |            0.00 |  985,959,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          992.00 |        1,000.33 |      999,674.40 |
|               2 |          998.00 |        1,000.25 |      999,751.80 |
|               3 |          986.00 |        1,000.04 |      999,959.03 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      629,888.00 |      635,174.56 |        1,574.37 |
|               2 |      629,888.00 |      631,306.99 |        1,584.02 |
|               3 |      629,888.00 |      638,857.82 |        1,565.29 |


