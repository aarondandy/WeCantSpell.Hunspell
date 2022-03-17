# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/17/2022 01:45:01_
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
|TotalBytesAllocated |           bytes |    3,130,304.00 |    3,130,304.00 |    3,130,304.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           67.00 |           67.00 |           67.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,029.00 |        1,021.33 |        1,015.00 |            7.09 |
|[Counter] _wordsChecked |      operations |      663,040.00 |      663,040.00 |      663,040.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,084,110.50 |    3,065,127.26 |    3,041,727.67 |       21,533.79 |
|TotalCollections [Gen0] |     collections |           66.01 |           65.60 |           65.10 |            0.46 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.20 |        1,000.04 |          999.88 |            0.16 |
|[Counter] _wordsChecked |      operations |      653,255.60 |      649,234.70 |      644,278.36 |        4,561.14 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,130,304.00 |    3,069,543.61 |          325.78 |
|               2 |    3,130,304.00 |    3,041,727.67 |          328.76 |
|               3 |    3,130,304.00 |    3,084,110.50 |          324.24 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           67.00 |           65.70 |   15,220,814.93 |
|               2 |           67.00 |           65.10 |   15,360,005.97 |
|               3 |           67.00 |           66.01 |   15,148,923.88 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,019,794,600.00 |
|               2 |            0.00 |            0.00 |1,029,120,400.00 |
|               3 |            0.00 |            0.00 |1,014,977,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,019,794,600.00 |
|               2 |            0.00 |            0.00 |1,029,120,400.00 |
|               3 |            0.00 |            0.00 |1,014,977,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,020.00 |        1,000.20 |      999,798.63 |
|               2 |        1,029.00 |          999.88 |    1,000,117.01 |
|               3 |        1,015.00 |        1,000.02 |      999,978.23 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      663,040.00 |      650,170.14 |        1,538.06 |
|               2 |      663,040.00 |      644,278.36 |        1,552.12 |
|               3 |      663,040.00 |      653,255.60 |        1,530.79 |


