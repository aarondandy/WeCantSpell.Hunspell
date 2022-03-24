# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/24/2022 04:43:00_
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
|TotalBytesAllocated |           bytes |      561,808.00 |      561,808.00 |      561,808.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           36.00 |           36.00 |           36.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,021.00 |        1,012.67 |        1,008.00 |            7.23 |
|[Counter] _wordsChecked |      operations |      671,328.00 |      671,328.00 |      671,328.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      557,515.80 |      554,898.46 |      550,449.96 |        3,872.52 |
|TotalCollections [Gen0] |     collections |           35.72 |           35.56 |           35.27 |            0.25 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.36 |        1,000.18 |          999.88 |            0.26 |
|[Counter] _wordsChecked |      operations |      666,199.07 |      663,071.50 |      657,755.80 |        4,627.43 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      561,808.00 |      557,515.80 |        1,793.67 |
|               2 |      561,808.00 |      556,729.62 |        1,796.20 |
|               3 |      561,808.00 |      550,449.96 |        1,816.70 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           36.00 |           35.72 |   27,991,633.33 |
|               2 |           36.00 |           35.67 |   28,031,161.11 |
|               3 |           36.00 |           35.27 |   28,350,947.22 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,698,800.00 |
|               2 |            0.00 |            0.00 |1,009,121,800.00 |
|               3 |            0.00 |            0.00 |1,020,634,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,698,800.00 |
|               2 |            0.00 |            0.00 |1,009,121,800.00 |
|               3 |            0.00 |            0.00 |1,020,634,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,008.00 |        1,000.30 |      999,701.19 |
|               2 |        1,009.00 |          999.88 |    1,000,120.71 |
|               3 |        1,021.00 |        1,000.36 |      999,641.63 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      671,328.00 |      666,199.07 |        1,501.05 |
|               2 |      671,328.00 |      665,259.63 |        1,503.17 |
|               3 |      671,328.00 |      657,755.80 |        1,520.32 |


