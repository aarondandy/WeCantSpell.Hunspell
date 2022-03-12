# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/12/2022 02:08:03_
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
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,078.00 |        1,025.33 |          972.00 |           53.00 |
|[Counter] _wordsChecked |      operations |    1,243,200.00 |    1,243,200.00 |    1,243,200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.96 |        1,000.47 |          999.77 |            0.63 |
|[Counter] _wordsChecked |      operations |    1,280,245.57 |    1,215,251.90 |    1,152,976.80 |       63,677.92 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,025,291,400.00 |
|               2 |            0.00 |            0.00 |1,078,252,400.00 |
|               3 |            0.00 |            0.00 |  971,063,700.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,025,291,400.00 |
|               2 |            0.00 |            0.00 |1,078,252,400.00 |
|               3 |            0.00 |            0.00 |  971,063,700.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,025,291,400.00 |
|               2 |            0.00 |            0.00 |1,078,252,400.00 |
|               3 |            0.00 |            0.00 |  971,063,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,025,291,400.00 |
|               2 |            0.00 |            0.00 |1,078,252,400.00 |
|               3 |            0.00 |            0.00 |  971,063,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,026.00 |        1,000.69 |      999,309.36 |
|               2 |        1,078.00 |          999.77 |    1,000,234.14 |
|               3 |          972.00 |        1,000.96 |      999,036.73 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,243,200.00 |    1,212,533.33 |          824.72 |
|               2 |    1,243,200.00 |    1,152,976.80 |          867.32 |
|               3 |    1,243,200.00 |    1,280,245.57 |          781.10 |


