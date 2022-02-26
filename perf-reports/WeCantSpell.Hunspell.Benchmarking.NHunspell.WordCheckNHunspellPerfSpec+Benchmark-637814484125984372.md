# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_02/26/2022 05:00:12_
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
|    Elapsed Time |              ms |        1,008.00 |        1,005.00 |        1,003.00 |            2.65 |
|[Counter] _wordsChecked |      operations |    1,301,216.00 |    1,301,216.00 |    1,301,216.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.61 |        1,000.28 |          999.80 |            0.42 |
|[Counter] _wordsChecked |      operations |    1,298,111.31 |    1,295,116.84 |    1,290,635.37 |        3,953.58 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,198,000.00 |
|               2 |            0.00 |            0.00 |1,002,391,700.00 |
|               3 |            0.00 |            0.00 |1,003,557,100.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,198,000.00 |
|               2 |            0.00 |            0.00 |1,002,391,700.00 |
|               3 |            0.00 |            0.00 |1,003,557,100.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,198,000.00 |
|               2 |            0.00 |            0.00 |1,002,391,700.00 |
|               3 |            0.00 |            0.00 |1,003,557,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,198,000.00 |
|               2 |            0.00 |            0.00 |1,002,391,700.00 |
|               3 |            0.00 |            0.00 |1,003,557,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,008.00 |          999.80 |    1,000,196.43 |
|               2 |        1,003.00 |        1,000.61 |      999,393.52 |
|               3 |        1,004.00 |        1,000.44 |      999,558.86 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,301,216.00 |    1,290,635.37 |          774.81 |
|               2 |    1,301,216.00 |    1,298,111.31 |          770.35 |
|               3 |    1,301,216.00 |    1,296,603.85 |          771.25 |


