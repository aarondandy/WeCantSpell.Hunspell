# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_04/13/2022 23:10:05_
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
|    Elapsed Time |              ms |        1,075.00 |        1,059.67 |        1,046.00 |           14.57 |
|[Counter] _wordsChecked |      operations |    1,193,472.00 |    1,193,472.00 |    1,193,472.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.39 |        1,000.34 |        1,000.24 |            0.08 |
|[Counter] _wordsChecked |      operations |    1,141,417.76 |    1,126,792.12 |    1,110,472.82 |       15,541.84 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,045,604,900.00 |
|               2 |            0.00 |            0.00 |1,074,742,200.00 |
|               3 |            0.00 |            0.00 |1,057,587,100.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,045,604,900.00 |
|               2 |            0.00 |            0.00 |1,074,742,200.00 |
|               3 |            0.00 |            0.00 |1,057,587,100.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,045,604,900.00 |
|               2 |            0.00 |            0.00 |1,074,742,200.00 |
|               3 |            0.00 |            0.00 |1,057,587,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,045,604,900.00 |
|               2 |            0.00 |            0.00 |1,074,742,200.00 |
|               3 |            0.00 |            0.00 |1,057,587,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,046.00 |        1,000.38 |      999,622.28 |
|               2 |        1,075.00 |        1,000.24 |      999,760.19 |
|               3 |        1,058.00 |        1,000.39 |      999,609.74 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,193,472.00 |    1,141,417.76 |          876.10 |
|               2 |    1,193,472.00 |    1,110,472.82 |          900.52 |
|               3 |    1,193,472.00 |    1,128,485.78 |          886.14 |


