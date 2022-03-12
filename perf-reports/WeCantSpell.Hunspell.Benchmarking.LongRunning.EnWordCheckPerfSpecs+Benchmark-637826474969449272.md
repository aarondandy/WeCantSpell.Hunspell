# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/12/2022 2:04:56 AM_
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
|TotalBytesAllocated |           bytes |    7,340,200.00 |    7,340,192.00 |    7,340,176.00 |           13.86 |
|TotalCollections [Gen0] |     collections |           65.00 |           65.00 |           65.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          891.00 |          835.67 |          807.00 |           47.93 |
|[Counter] WordsChecked |      operations |      745,920.00 |      745,920.00 |      745,920.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,085,714.82 |    8,801,560.18 |    8,245,006.05 |      482,025.85 |
|TotalCollections [Gen0] |     collections |           80.46 |           77.94 |           73.01 |            4.27 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.83 |          999.94 |          998.91 |            0.97 |
|[Counter] WordsChecked |      operations |      923,301.33 |      894,426.10 |      837,870.22 |       48,982.47 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,340,200.00 |    9,085,714.82 |          110.06 |
|               2 |    7,340,200.00 |    9,073,959.66 |          110.21 |
|               3 |    7,340,176.00 |    8,245,006.05 |          121.29 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           65.00 |           80.46 |   12,428,978.46 |
|               2 |           65.00 |           80.35 |   12,445,080.00 |
|               3 |           65.00 |           73.01 |   13,696,264.62 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  807,883,600.00 |
|               2 |            0.00 |            0.00 |  808,930,200.00 |
|               3 |            0.00 |            0.00 |  890,257,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  807,883,600.00 |
|               2 |            0.00 |            0.00 |  808,930,200.00 |
|               3 |            0.00 |            0.00 |  890,257,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          807.00 |          998.91 |    1,001,094.92 |
|               2 |          809.00 |        1,000.09 |      999,913.72 |
|               3 |          891.00 |        1,000.83 |      999,166.33 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      745,920.00 |      923,301.33 |        1,083.07 |
|               2 |      745,920.00 |      922,106.75 |        1,084.47 |
|               3 |      745,920.00 |      837,870.22 |        1,193.50 |


