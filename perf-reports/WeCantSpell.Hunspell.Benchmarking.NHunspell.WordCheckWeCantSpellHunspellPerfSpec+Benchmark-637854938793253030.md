# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_04/14/2022 00:44:39_
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
|TotalBytesAllocated |           bytes |    3,351,408.00 |    3,351,408.00 |    3,351,408.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           15.00 |           15.00 |           15.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,024.00 |        1,022.00 |        1,019.00 |            2.65 |
|[Counter] _wordsChecked |      operations |      646,464.00 |      646,464.00 |      646,464.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,287,375.19 |    3,279,878.08 |    3,275,210.55 |        6,557.40 |
|TotalCollections [Gen0] |     collections |           14.71 |           14.68 |           14.66 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.72 |        1,000.18 |          999.53 |            0.60 |
|[Counter] _wordsChecked |      operations |      634,112.50 |      632,666.36 |      631,766.03 |        1,264.88 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,351,408.00 |    3,287,375.19 |          304.19 |
|               2 |    3,351,408.00 |    3,275,210.55 |          305.32 |
|               3 |    3,351,408.00 |    3,277,048.49 |          305.15 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           15.00 |           14.71 |   67,965,226.67 |
|               2 |           15.00 |           14.66 |   68,217,660.00 |
|               3 |           15.00 |           14.67 |   68,179,400.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,019,478,400.00 |
|               2 |            0.00 |            0.00 |1,023,264,900.00 |
|               3 |            0.00 |            0.00 |1,022,691,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,019,478,400.00 |
|               2 |            0.00 |            0.00 |1,023,264,900.00 |
|               3 |            0.00 |            0.00 |1,022,691,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,019.00 |          999.53 |    1,000,469.48 |
|               2 |        1,024.00 |        1,000.72 |      999,282.13 |
|               3 |        1,023.00 |        1,000.30 |      999,697.95 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      646,464.00 |      634,112.50 |        1,577.01 |
|               2 |      646,464.00 |      631,766.03 |        1,582.86 |
|               3 |      646,464.00 |      632,120.55 |        1,581.98 |


