# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/06/2022 06:14:32_
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
|TotalBytesAllocated |           bytes |    5,415,904.00 |    5,415,904.00 |    5,415,904.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           76.00 |           76.00 |           76.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,003.00 |        1,002.00 |        1,001.00 |            1.00 |
|[Counter] _wordsChecked |      operations |      663,040.00 |      663,040.00 |      663,040.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,412,499.54 |    5,404,903.67 |    5,398,749.47 |        6,987.48 |
|TotalCollections [Gen0] |     collections |           75.95 |           75.85 |           75.76 |            0.10 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.37 |          999.96 |          999.70 |            0.36 |
|[Counter] _wordsChecked |      operations |      662,623.21 |      661,693.29 |      660,939.86 |          855.44 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,415,904.00 |    5,403,461.99 |          185.07 |
|               2 |    5,415,904.00 |    5,398,749.47 |          185.23 |
|               3 |    5,415,904.00 |    5,412,499.54 |          184.76 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           76.00 |           75.83 |   13,188,192.11 |
|               2 |           76.00 |           75.76 |   13,199,703.95 |
|               3 |           76.00 |           75.95 |   13,166,171.05 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,002,302,600.00 |
|               2 |            0.00 |            0.00 |1,003,177,500.00 |
|               3 |            0.00 |            0.00 |1,000,629,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,002,302,600.00 |
|               2 |            0.00 |            0.00 |1,003,177,500.00 |
|               3 |            0.00 |            0.00 |1,000,629,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,002.00 |          999.70 |    1,000,302.00 |
|               2 |        1,003.00 |          999.82 |    1,000,176.97 |
|               3 |        1,001.00 |        1,000.37 |      999,629.37 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      663,040.00 |      661,516.79 |        1,511.68 |
|               2 |      663,040.00 |      660,939.86 |        1,513.00 |
|               3 |      663,040.00 |      662,623.21 |        1,509.15 |


