# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_4/13/2022 11:06:36 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.4,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    5,423,944.00 |    5,423,898.67 |    5,423,808.00 |           78.52 |
|TotalCollections [Gen0] |     collections |           15.00 |           15.00 |           15.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,014.00 |          939.00 |          792.00 |          127.31 |
|[Counter] WordsChecked |      operations |      878,528.00 |      878,528.00 |      878,528.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,848,385.61 |    5,854,324.39 |    5,348,319.68 |      860,929.04 |
|TotalCollections [Gen0] |     collections |           18.94 |           16.19 |           14.79 |            2.38 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.25 |        1,000.04 |          999.89 |            0.19 |
|[Counter] WordsChecked |      operations |    1,109,247.90 |      948,244.89 |      866,300.69 |      139,440.16 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,423,808.00 |    5,348,319.68 |          186.97 |
|               2 |    5,423,944.00 |    5,366,267.89 |          186.35 |
|               3 |    5,423,944.00 |    6,848,385.61 |          146.02 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           15.00 |           14.79 |   67,607,626.67 |
|               2 |           15.00 |           14.84 |   67,383,193.33 |
|               3 |           15.00 |           18.94 |   52,800,220.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,014,114,400.00 |
|               2 |            0.00 |            0.00 |1,010,747,900.00 |
|               3 |            0.00 |            0.00 |  792,003,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,014,114,400.00 |
|               2 |            0.00 |            0.00 |1,010,747,900.00 |
|               3 |            0.00 |            0.00 |  792,003,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,014.00 |          999.89 |    1,000,112.82 |
|               2 |        1,011.00 |        1,000.25 |      999,750.64 |
|               3 |          792.00 |        1,000.00 |    1,000,004.17 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      878,528.00 |      866,300.69 |        1,154.33 |
|               2 |      878,528.00 |      869,186.07 |        1,150.50 |
|               3 |      878,528.00 |    1,109,247.90 |          901.51 |


