# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/12/2022 02:08:14_
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
|TotalBytesAllocated |           bytes |    2,297,976.00 |    2,297,976.00 |    2,297,976.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           77.00 |           77.00 |           77.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,088.00 |        1,048.67 |        1,007.00 |           40.55 |
|[Counter] _wordsChecked |      operations |      563,584.00 |      563,584.00 |      563,584.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,281,484.97 |    2,193,381.32 |    2,111,186.65 |       85,302.79 |
|TotalCollections [Gen0] |     collections |           76.45 |           73.50 |           70.74 |            2.86 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.46 |          999.93 |          999.56 |            0.47 |
|[Counter] _wordsChecked |      operations |      559,539.54 |      537,931.91 |      517,773.47 |       20,920.71 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,297,976.00 |    2,281,484.97 |          438.31 |
|               2 |    2,297,976.00 |    2,187,472.34 |          457.15 |
|               3 |    2,297,976.00 |    2,111,186.65 |          473.67 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           77.00 |           76.45 |   13,080,885.71 |
|               2 |           77.00 |           73.30 |   13,643,072.73 |
|               3 |           77.00 |           70.74 |   14,136,051.95 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,228,200.00 |
|               2 |            0.00 |            0.00 |1,050,516,600.00 |
|               3 |            0.00 |            0.00 |1,088,476,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,228,200.00 |
|               2 |            0.00 |            0.00 |1,050,516,600.00 |
|               3 |            0.00 |            0.00 |1,088,476,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,007.00 |          999.77 |    1,000,226.61 |
|               2 |        1,051.00 |        1,000.46 |      999,540.06 |
|               3 |        1,088.00 |          999.56 |    1,000,437.50 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      563,584.00 |      559,539.54 |        1,787.18 |
|               2 |      563,584.00 |      536,482.72 |        1,863.99 |
|               3 |      563,584.00 |      517,773.47 |        1,931.35 |


