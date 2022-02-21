# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_2/21/2022 1:44:21 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.2,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    1,591,280.00 |    1,591,277.33 |    1,591,272.00 |            4.62 |
|TotalCollections [Gen0] |     collections |           76.00 |           76.00 |           76.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,058.00 |          913.33 |          840.00 |          125.29 |
|[Counter] WordsChecked |      operations |      944,832.00 |      944,832.00 |      944,832.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,895,496.14 |    1,763,148.62 |    1,503,515.27 |      224,863.32 |
|TotalCollections [Gen0] |     collections |           90.53 |           84.21 |           71.81 |           10.74 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.59 |        1,000.18 |          999.65 |            0.48 |
|[Counter] WordsChecked |      operations |    1,125,462.15 |    1,046,881.51 |      892,725.66 |      133,511.35 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,591,272.00 |    1,503,515.27 |          665.11 |
|               2 |    1,591,280.00 |    1,890,434.45 |          528.98 |
|               3 |    1,591,280.00 |    1,895,496.14 |          527.57 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           76.00 |           71.81 |   13,925,890.79 |
|               2 |           76.00 |           90.29 |   11,075,705.26 |
|               3 |           76.00 |           90.53 |   11,046,128.95 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,058,367,700.00 |
|               2 |            0.00 |            0.00 |  841,753,600.00 |
|               3 |            0.00 |            0.00 |  839,505,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,058,367,700.00 |
|               2 |            0.00 |            0.00 |  841,753,600.00 |
|               3 |            0.00 |            0.00 |  839,505,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,058.00 |          999.65 |    1,000,347.54 |
|               2 |          842.00 |        1,000.29 |      999,707.36 |
|               3 |          840.00 |        1,000.59 |      999,411.67 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      944,832.00 |      892,725.66 |        1,120.16 |
|               2 |      944,832.00 |    1,122,456.74 |          890.90 |
|               3 |      944,832.00 |    1,125,462.15 |          888.52 |


