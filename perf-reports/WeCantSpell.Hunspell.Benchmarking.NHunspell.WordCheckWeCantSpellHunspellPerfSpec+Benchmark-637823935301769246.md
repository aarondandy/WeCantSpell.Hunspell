# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/09/2022 03:32:10_
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
|    Elapsed Time |              ms |          999.00 |          990.67 |          982.00 |            8.50 |
|[Counter] _wordsChecked |      operations |      663,040.00 |      663,040.00 |      663,040.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,514,489.28 |    5,466,529.06 |    5,422,558.56 |       46,095.04 |
|TotalCollections [Gen0] |     collections |           77.38 |           76.71 |           76.09 |            0.65 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.23 |          999.88 |          999.53 |            0.35 |
|[Counter] _wordsChecked |      operations |      675,109.27 |      669,237.75 |      663,854.68 |        5,643.17 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,415,904.00 |    5,462,539.34 |          183.07 |
|               2 |    5,415,904.00 |    5,422,558.56 |          184.41 |
|               3 |    5,415,904.00 |    5,514,489.28 |          181.34 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           76.00 |           76.65 |   13,045,561.84 |
|               2 |           76.00 |           76.09 |   13,141,747.37 |
|               3 |           76.00 |           77.38 |   12,922,664.47 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  991,462,700.00 |
|               2 |            0.00 |            0.00 |  998,772,800.00 |
|               3 |            0.00 |            0.00 |  982,122,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  991,462,700.00 |
|               2 |            0.00 |            0.00 |  998,772,800.00 |
|               3 |            0.00 |            0.00 |  982,122,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          991.00 |          999.53 |    1,000,466.90 |
|               2 |          999.00 |        1,000.23 |      999,772.57 |
|               3 |          982.00 |          999.88 |    1,000,124.75 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      663,040.00 |      668,749.31 |        1,495.33 |
|               2 |      663,040.00 |      663,854.68 |        1,506.35 |
|               3 |      663,040.00 |      675,109.27 |        1,481.24 |


