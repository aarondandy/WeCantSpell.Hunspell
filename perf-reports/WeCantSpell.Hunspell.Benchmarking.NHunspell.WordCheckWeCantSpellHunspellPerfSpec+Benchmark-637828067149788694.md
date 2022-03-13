# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/13/2022 22:18:34_
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
|TotalBytesAllocated |           bytes |    5,980,584.00 |    5,980,584.00 |    5,980,584.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           62.00 |           62.00 |           62.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          971.00 |          968.00 |          966.00 |            2.65 |
|[Counter] _wordsChecked |      operations |      621,600.00 |      621,600.00 |      621,600.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,192,616.72 |    6,177,808.63 |    6,159,512.92 |       16,825.22 |
|TotalCollections [Gen0] |     collections |           64.20 |           64.04 |           63.85 |            0.17 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.25 |          999.92 |          999.45 |            0.41 |
|[Counter] _wordsChecked |      operations |      643,637.90 |      642,098.81 |      640,197.22 |        1,748.75 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,980,584.00 |    6,181,296.25 |          161.78 |
|               2 |    5,980,584.00 |    6,192,616.72 |          161.48 |
|               3 |    5,980,584.00 |    6,159,512.92 |          162.35 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           62.00 |           64.08 |   15,605,308.06 |
|               2 |           62.00 |           64.20 |   15,576,780.65 |
|               3 |           62.00 |           63.85 |   15,660,496.77 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  967,529,100.00 |
|               2 |            0.00 |            0.00 |  965,760,400.00 |
|               3 |            0.00 |            0.00 |  970,950,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  967,529,100.00 |
|               2 |            0.00 |            0.00 |  965,760,400.00 |
|               3 |            0.00 |            0.00 |  970,950,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          967.00 |          999.45 |    1,000,547.16 |
|               2 |          966.00 |        1,000.25 |      999,751.97 |
|               3 |          971.00 |        1,000.05 |      999,949.33 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      621,600.00 |      642,461.30 |        1,556.51 |
|               2 |      621,600.00 |      643,637.90 |        1,553.67 |
|               3 |      621,600.00 |      640,197.22 |        1,562.02 |


