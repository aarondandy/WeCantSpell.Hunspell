# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/27/2022 01:05:06_
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
|TotalBytesAllocated |           bytes |       53,016.00 |       53,016.00 |       53,016.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           31.00 |           31.00 |           31.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,012.00 |        1,009.00 |        1,005.00 |            3.61 |
|[Counter] _wordsChecked |      operations |      671,328.00 |      671,328.00 |      671,328.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |       52,785.47 |       52,553.77 |       52,400.74 |          204.07 |
|TotalCollections [Gen0] |     collections |           30.87 |           30.73 |           30.64 |            0.12 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.63 |        1,000.19 |          999.70 |            0.47 |
|[Counter] _wordsChecked |      operations |      668,408.86 |      665,474.87 |      663,537.14 |        2,584.14 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       53,016.00 |       52,400.74 |       19,083.70 |
|               2 |       53,016.00 |       52,475.09 |       19,056.66 |
|               3 |       53,016.00 |       52,785.47 |       18,944.61 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           31.00 |           30.64 |   32,636,819.35 |
|               2 |           31.00 |           30.68 |   32,590,577.42 |
|               3 |           31.00 |           30.87 |   32,398,945.16 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,011,741,400.00 |
|               2 |            0.00 |            0.00 |1,010,307,900.00 |
|               3 |            0.00 |            0.00 |1,004,367,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,011,741,400.00 |
|               2 |            0.00 |            0.00 |1,010,307,900.00 |
|               3 |            0.00 |            0.00 |1,004,367,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,012.00 |        1,000.26 |      999,744.47 |
|               2 |        1,010.00 |          999.70 |    1,000,304.85 |
|               3 |        1,005.00 |        1,000.63 |      999,370.45 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      671,328.00 |      663,537.14 |        1,507.07 |
|               2 |      671,328.00 |      664,478.62 |        1,504.94 |
|               3 |      671,328.00 |      668,408.86 |        1,496.09 |


