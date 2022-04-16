# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_04/16/2022 13:08:21_
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
|TotalBytesAllocated |           bytes |    1,933,736.00 |    1,933,736.00 |    1,933,736.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,008.00 |        1,006.33 |        1,005.00 |            1.53 |
|[Counter] _wordsChecked |      operations |      671,328.00 |      671,328.00 |      671,328.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,922,587.49 |    1,921,164.46 |    1,918,773.98 |        2,082.71 |
|TotalCollections [Gen0] |     collections |           12.93 |           12.92 |           12.90 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.20 |          999.79 |          999.21 |            0.52 |
|[Counter] _wordsChecked |      operations |      667,457.61 |      666,963.58 |      666,133.69 |          723.05 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,933,736.00 |    1,922,587.49 |          520.13 |
|               2 |    1,933,736.00 |    1,922,131.90 |          520.26 |
|               3 |    1,933,736.00 |    1,918,773.98 |          521.17 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |           12.93 |   77,369,130.77 |
|               2 |           13.00 |           12.92 |   77,387,469.23 |
|               3 |           13.00 |           12.90 |   77,522,900.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,005,798,700.00 |
|               2 |            0.00 |            0.00 |1,006,037,100.00 |
|               3 |            0.00 |            0.00 |1,007,797,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,005,798,700.00 |
|               2 |            0.00 |            0.00 |1,006,037,100.00 |
|               3 |            0.00 |            0.00 |1,007,797,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,005.00 |          999.21 |    1,000,794.73 |
|               2 |        1,006.00 |          999.96 |    1,000,036.88 |
|               3 |        1,008.00 |        1,000.20 |      999,799.31 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      671,328.00 |      667,457.61 |        1,498.22 |
|               2 |      671,328.00 |      667,299.45 |        1,498.58 |
|               3 |      671,328.00 |      666,133.69 |        1,501.20 |


