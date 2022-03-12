# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/12/2022 3:32:48 AM_
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
|TotalBytesAllocated |           bytes |    2,944,688.00 |    2,944,629.33 |    2,944,520.00 |           94.77 |
|TotalCollections [Gen0] |     collections |           63.00 |           63.00 |           63.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          801.00 |          799.00 |          798.00 |            1.73 |
|[Counter] WordsChecked |      operations |      779,072.00 |      779,072.00 |      779,072.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,690,741.65 |    3,685,186.90 |    3,674,987.83 |        8,844.38 |
|TotalCollections [Gen0] |     collections |           78.96 |           78.84 |           78.62 |            0.19 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.18 |          999.94 |          999.65 |            0.27 |
|[Counter] WordsChecked |      operations |      976,457.03 |      975,004.21 |      972,286.41 |        2,355.56 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,944,680.00 |    3,690,741.65 |          270.95 |
|               2 |    2,944,688.00 |    3,674,987.83 |          272.11 |
|               3 |    2,944,520.00 |    3,689,831.22 |          271.02 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           63.00 |           78.96 |   12,664,379.37 |
|               2 |           63.00 |           78.62 |   12,718,703.17 |
|               3 |           63.00 |           78.95 |   12,666,815.87 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  797,855,900.00 |
|               2 |            0.00 |            0.00 |  801,278,300.00 |
|               3 |            0.00 |            0.00 |  798,009,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  797,855,900.00 |
|               2 |            0.00 |            0.00 |  801,278,300.00 |
|               3 |            0.00 |            0.00 |  798,009,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          798.00 |        1,000.18 |      999,819.42 |
|               2 |          801.00 |          999.65 |    1,000,347.44 |
|               3 |          798.00 |          999.99 |    1,000,011.78 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      779,072.00 |      976,457.03 |        1,024.11 |
|               2 |      779,072.00 |      972,286.41 |        1,028.50 |
|               3 |      779,072.00 |      976,269.20 |        1,024.31 |


