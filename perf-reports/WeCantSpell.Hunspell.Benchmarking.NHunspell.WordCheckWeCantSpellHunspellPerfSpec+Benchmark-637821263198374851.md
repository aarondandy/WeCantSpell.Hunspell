# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/06/2022 01:18:39_
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
|TotalBytesAllocated |           bytes |    5,645,816.00 |    5,645,816.00 |    5,645,816.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           75.00 |           75.00 |           75.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,015.00 |        1,012.33 |        1,008.00 |            3.79 |
|[Counter] _wordsChecked |      operations |      654,752.00 |      654,752.00 |      654,752.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,600,744.01 |    5,579,659.26 |    5,565,945.24 |       18,533.33 |
|TotalCollections [Gen0] |     collections |           74.40 |           74.12 |           73.94 |            0.25 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.79 |        1,000.46 |          999.95 |            0.45 |
|[Counter] _wordsChecked |      operations |      649,524.95 |      647,079.72 |      645,489.29 |        2,149.33 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,645,816.00 |    5,600,744.01 |          178.55 |
|               2 |    5,645,816.00 |    5,565,945.24 |          179.66 |
|               3 |    5,645,816.00 |    5,572,288.54 |          179.46 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           75.00 |           74.40 |   13,440,633.33 |
|               2 |           75.00 |           73.94 |   13,524,665.33 |
|               3 |           75.00 |           74.02 |   13,509,269.33 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,047,500.00 |
|               2 |            0.00 |            0.00 |1,014,349,900.00 |
|               3 |            0.00 |            0.00 |1,013,195,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,047,500.00 |
|               2 |            0.00 |            0.00 |1,014,349,900.00 |
|               3 |            0.00 |            0.00 |1,013,195,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,008.00 |          999.95 |    1,000,047.12 |
|               2 |        1,015.00 |        1,000.64 |      999,359.51 |
|               3 |        1,014.00 |        1,000.79 |      999,206.31 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      654,752.00 |      649,524.95 |        1,539.59 |
|               2 |      654,752.00 |      645,489.29 |        1,549.21 |
|               3 |      654,752.00 |      646,224.93 |        1,547.45 |


