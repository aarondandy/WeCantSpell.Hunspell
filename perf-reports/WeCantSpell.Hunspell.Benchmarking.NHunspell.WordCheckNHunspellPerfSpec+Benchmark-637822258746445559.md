# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/07/2022 04:57:54_
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
|    Elapsed Time |              ms |        1,011.00 |        1,001.33 |          994.00 |            8.74 |
|[Counter] _wordsChecked |      operations |    1,301,216.00 |    1,301,216.00 |    1,301,216.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.47 |        1,000.19 |        1,000.04 |            0.24 |
|[Counter] _wordsChecked |      operations |    1,309,681.91 |    1,299,798.96 |    1,287,144.55 |       11,521.45 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  993,535,900.00 |
|               2 |            0.00 |            0.00 |1,010,932,300.00 |
|               3 |            0.00 |            0.00 |  998,960,200.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  993,535,900.00 |
|               2 |            0.00 |            0.00 |1,010,932,300.00 |
|               3 |            0.00 |            0.00 |  998,960,200.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  993,535,900.00 |
|               2 |            0.00 |            0.00 |1,010,932,300.00 |
|               3 |            0.00 |            0.00 |  998,960,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  993,535,900.00 |
|               2 |            0.00 |            0.00 |1,010,932,300.00 |
|               3 |            0.00 |            0.00 |  998,960,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          994.00 |        1,000.47 |      999,533.10 |
|               2 |        1,011.00 |        1,000.07 |      999,933.04 |
|               3 |          999.00 |        1,000.04 |      999,960.16 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,301,216.00 |    1,309,681.91 |          763.54 |
|               2 |    1,301,216.00 |    1,287,144.55 |          776.91 |
|               3 |    1,301,216.00 |    1,302,570.41 |          767.71 |


