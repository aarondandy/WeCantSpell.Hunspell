# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_02/21/2022 23:48:32_
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
|TotalBytesAllocated |           bytes |    3,419,768.00 |    3,419,768.00 |    3,419,768.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           80.00 |           80.00 |           80.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,020.00 |        1,019.67 |        1,019.00 |            0.58 |
|[Counter] _wordsChecked |      operations |      696,192.00 |      696,192.00 |      696,192.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,357,037.39 |    3,353,537.53 |    3,351,370.22 |        3,059.57 |
|TotalCollections [Gen0] |     collections |           78.53 |           78.45 |           78.40 |            0.07 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.31 |          999.92 |          999.60 |            0.36 |
|[Counter] _wordsChecked |      operations |      683,421.38 |      682,708.89 |      682,267.67 |          622.86 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,419,768.00 |    3,351,370.22 |          298.39 |
|               2 |    3,419,768.00 |    3,357,037.39 |          297.88 |
|               3 |    3,419,768.00 |    3,352,204.98 |          298.31 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           80.00 |           78.40 |   12,755,111.25 |
|               2 |           80.00 |           78.53 |   12,733,578.75 |
|               3 |           80.00 |           78.42 |   12,751,935.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,020,408,900.00 |
|               2 |            0.00 |            0.00 |1,018,686,300.00 |
|               3 |            0.00 |            0.00 |1,020,154,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,020,408,900.00 |
|               2 |            0.00 |            0.00 |1,018,686,300.00 |
|               3 |            0.00 |            0.00 |1,020,154,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,020.00 |          999.60 |    1,000,400.88 |
|               2 |        1,019.00 |        1,000.31 |      999,692.15 |
|               3 |        1,020.00 |          999.85 |    1,000,151.76 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      696,192.00 |      682,267.67 |        1,465.70 |
|               2 |      696,192.00 |      683,421.38 |        1,463.23 |
|               3 |      696,192.00 |      682,437.61 |        1,465.34 |


