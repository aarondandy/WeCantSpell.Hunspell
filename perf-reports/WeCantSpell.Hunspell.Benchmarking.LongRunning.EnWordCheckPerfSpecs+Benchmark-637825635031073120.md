# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/11/2022 2:45:03 AM_
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
|TotalBytesAllocated |           bytes |    7,969,856.00 |    7,969,690.67 |    7,969,608.00 |          143.18 |
|TotalCollections [Gen0] |     collections |           67.00 |           67.00 |           67.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,065.00 |          895.33 |          810.00 |          146.94 |
|[Counter] WordsChecked |      operations |      812,224.00 |      812,224.00 |      812,224.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,829,628.25 |    9,045,849.67 |    7,478,481.62 |    1,357,380.55 |
|TotalCollections [Gen0] |     collections |           82.64 |           76.05 |           62.87 |           11.41 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.28 |          999.55 |          999.03 |            0.65 |
|[Counter] WordsChecked |      operations |    1,001,788.29 |      921,901.45 |      762,147.05 |      138,351.37 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,969,856.00 |    7,478,481.62 |          133.72 |
|               2 |    7,969,608.00 |    9,829,439.13 |          101.74 |
|               3 |    7,969,608.00 |    9,829,628.25 |          101.73 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           67.00 |           62.87 |   15,906,046.27 |
|               2 |           67.00 |           82.64 |   12,101,338.81 |
|               3 |           67.00 |           82.64 |   12,101,105.97 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,065,705,100.00 |
|               2 |            0.00 |            0.00 |  810,789,700.00 |
|               3 |            0.00 |            0.00 |  810,774,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,065,705,100.00 |
|               2 |            0.00 |            0.00 |  810,789,700.00 |
|               3 |            0.00 |            0.00 |  810,774,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,065.00 |          999.34 |    1,000,662.07 |
|               2 |          810.00 |          999.03 |    1,000,974.94 |
|               3 |          811.00 |        1,000.28 |      999,721.45 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      812,224.00 |      762,147.05 |        1,312.08 |
|               2 |      812,224.00 |    1,001,769.02 |          998.23 |
|               3 |      812,224.00 |    1,001,788.29 |          998.21 |


