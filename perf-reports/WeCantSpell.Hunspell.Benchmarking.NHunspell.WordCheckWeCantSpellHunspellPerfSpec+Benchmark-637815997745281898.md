# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_02/27/2022 23:02:54_
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
|TotalBytesAllocated |           bytes |    4,982,952.00 |    4,982,952.00 |    4,982,952.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           78.00 |           78.00 |           78.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,022.00 |        1,016.67 |        1,012.00 |            5.03 |
|[Counter] _wordsChecked |      operations |      679,616.00 |      679,616.00 |      679,616.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,920,654.55 |    4,900,741.16 |    4,876,568.68 |       22,349.40 |
|TotalCollections [Gen0] |     collections |           77.02 |           76.71 |           76.33 |            0.35 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.18 |          999.88 |          999.35 |            0.46 |
|[Counter] _wordsChecked |      operations |      671,119.36 |      668,403.41 |      665,106.57 |        3,048.20 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,982,952.00 |    4,920,654.55 |          203.22 |
|               2 |    4,982,952.00 |    4,905,000.26 |          203.87 |
|               3 |    4,982,952.00 |    4,876,568.68 |          205.06 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           78.00 |           77.02 |   12,982,825.64 |
|               2 |           78.00 |           76.78 |   13,024,260.26 |
|               3 |           78.00 |           76.33 |   13,100,194.87 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,012,660,400.00 |
|               2 |            0.00 |            0.00 |1,015,892,300.00 |
|               3 |            0.00 |            0.00 |1,021,815,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,012,660,400.00 |
|               2 |            0.00 |            0.00 |1,015,892,300.00 |
|               3 |            0.00 |            0.00 |1,021,815,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,012.00 |          999.35 |    1,000,652.57 |
|               2 |        1,016.00 |        1,000.11 |      999,894.00 |
|               3 |        1,022.00 |        1,000.18 |      999,819.18 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      679,616.00 |      671,119.36 |        1,490.05 |
|               2 |      679,616.00 |      668,984.30 |        1,494.80 |
|               3 |      679,616.00 |      665,106.57 |        1,503.52 |


