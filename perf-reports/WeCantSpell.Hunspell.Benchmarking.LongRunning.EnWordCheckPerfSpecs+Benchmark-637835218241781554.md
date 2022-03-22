# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/22/2022 4:57:04 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.3,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    1,054,576.00 |    1,054,522.67 |    1,054,496.00 |           46.19 |
|TotalCollections [Gen0] |     collections |           45.00 |           45.00 |           45.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          644.00 |          638.00 |          634.00 |            5.29 |
|[Counter] WordsChecked |      operations |      629,888.00 |      629,888.00 |      629,888.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,664,765.23 |    1,653,670.15 |    1,637,118.21 |       14,608.98 |
|TotalCollections [Gen0] |     collections |           71.04 |           70.57 |           69.86 |            0.62 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.84 |        1,000.44 |          999.82 |            0.55 |
|[Counter] WordsChecked |      operations |      994,348.10 |      987,770.91 |      977,908.99 |        8,697.88 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,054,496.00 |    1,637,118.21 |          610.83 |
|               2 |    1,054,576.00 |    1,664,765.23 |          600.69 |
|               3 |    1,054,496.00 |    1,659,127.01 |          602.73 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           45.00 |           69.86 |   14,313,715.56 |
|               2 |           45.00 |           71.04 |   14,077,073.33 |
|               3 |           45.00 |           70.80 |   14,123,840.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  644,117,200.00 |
|               2 |            0.00 |            0.00 |  633,468,300.00 |
|               3 |            0.00 |            0.00 |  635,572,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  644,117,200.00 |
|               2 |            0.00 |            0.00 |  633,468,300.00 |
|               3 |            0.00 |            0.00 |  635,572,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          644.00 |          999.82 |    1,000,181.99 |
|               2 |          634.00 |        1,000.84 |      999,161.36 |
|               3 |          636.00 |        1,000.67 |      999,328.30 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      629,888.00 |      977,908.99 |        1,022.59 |
|               2 |      629,888.00 |      994,348.10 |        1,005.68 |
|               3 |      629,888.00 |      991,055.63 |        1,009.03 |


