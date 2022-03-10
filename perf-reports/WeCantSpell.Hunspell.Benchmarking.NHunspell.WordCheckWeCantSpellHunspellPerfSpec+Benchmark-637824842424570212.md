# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/10/2022 04:44:02_
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
|TotalBytesAllocated |           bytes |      771,336.00 |      771,336.00 |      771,336.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           78.00 |           78.00 |           78.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,023.00 |        1,010.00 |          997.00 |           13.00 |
|[Counter] _wordsChecked |      operations |      663,040.00 |      663,040.00 |      663,040.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      773,703.61 |      763,724.67 |      753,793.93 |        9,954.93 |
|TotalCollections [Gen0] |     collections |           78.24 |           77.23 |           76.23 |            1.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.06 |          999.92 |          999.73 |            0.17 |
|[Counter] _wordsChecked |      operations |      665,075.20 |      656,497.31 |      647,960.85 |        8,557.25 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      771,336.00 |      763,676.48 |        1,309.46 |
|               2 |      771,336.00 |      753,793.93 |        1,326.62 |
|               3 |      771,336.00 |      773,703.61 |        1,292.48 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           78.00 |           77.23 |   12,949,100.00 |
|               2 |           78.00 |           76.23 |   13,118,867.95 |
|               3 |           78.00 |           78.24 |   12,781,280.77 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,010,029,800.00 |
|               2 |            0.00 |            0.00 |1,023,271,700.00 |
|               3 |            0.00 |            0.00 |  996,939,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,010,029,800.00 |
|               2 |            0.00 |            0.00 |1,023,271,700.00 |
|               3 |            0.00 |            0.00 |  996,939,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,010.00 |          999.97 |    1,000,029.50 |
|               2 |        1,023.00 |          999.73 |    1,000,265.59 |
|               3 |          997.00 |        1,000.06 |      999,939.72 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      663,040.00 |      656,455.88 |        1,523.33 |
|               2 |      663,040.00 |      647,960.85 |        1,543.30 |
|               3 |      663,040.00 |      665,075.20 |        1,503.59 |


