# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_04/09/2022 14:45:19_
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
|TotalBytesAllocated |           bytes |    3,285,704.00 |    3,285,704.00 |    3,285,704.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           15.00 |           15.00 |           15.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,021.00 |        1,017.00 |        1,013.00 |            4.00 |
|[Counter] _wordsChecked |      operations |      646,464.00 |      646,464.00 |      646,464.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,246,233.37 |    3,231,177.18 |    3,216,835.41 |       14,711.99 |
|TotalCollections [Gen0] |     collections |           14.82 |           14.75 |           14.69 |            0.07 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.83 |        1,000.11 |          999.60 |            0.64 |
|[Counter] _wordsChecked |      operations |      638,698.13 |      635,735.82 |      632,914.07 |        2,894.59 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,285,704.00 |    3,246,233.37 |          308.05 |
|               2 |    3,285,704.00 |    3,216,835.41 |          310.86 |
|               3 |    3,285,704.00 |    3,230,462.76 |          309.55 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           15.00 |           14.82 |   67,477,260.00 |
|               2 |           15.00 |           14.69 |   68,093,920.00 |
|               3 |           15.00 |           14.75 |   67,806,673.33 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,012,158,900.00 |
|               2 |            0.00 |            0.00 |1,021,408,800.00 |
|               3 |            0.00 |            0.00 |1,017,100,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,012,158,900.00 |
|               2 |            0.00 |            0.00 |1,021,408,800.00 |
|               3 |            0.00 |            0.00 |1,017,100,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,013.00 |        1,000.83 |      999,169.69 |
|               2 |        1,021.00 |          999.60 |    1,000,400.39 |
|               3 |        1,017.00 |          999.90 |    1,000,098.43 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      646,464.00 |      638,698.13 |        1,565.68 |
|               2 |      646,464.00 |      632,914.07 |        1,579.99 |
|               3 |      646,464.00 |      635,595.26 |        1,573.33 |


