# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/21/2022 4:23:33 AM_
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
|TotalBytesAllocated |           bytes |    6,035,376.00 |    6,035,328.00 |    6,035,232.00 |           83.14 |
|TotalCollections [Gen0] |     collections |           45.00 |           45.00 |           45.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,083.00 |          907.00 |          626.00 |          245.93 |
|[Counter] WordsChecked |      operations |      638,176.00 |      638,176.00 |      638,176.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,641,429.30 |    7,059,084.90 |    5,568,062.95 |    2,245,287.70 |
|TotalCollections [Gen0] |     collections |           71.89 |           52.63 |           41.52 |           16.74 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.66 |          999.95 |          999.14 |            0.76 |
|[Counter] WordsChecked |      operations |    1,019,501.62 |      746,430.30 |      588,762.68 |      237,428.98 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,035,376.00 |    5,967,762.45 |          167.57 |
|               2 |    6,035,376.00 |    5,568,062.95 |          179.60 |
|               3 |    6,035,232.00 |    9,641,429.30 |          103.72 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           45.00 |           44.50 |   22,473,995.56 |
|               2 |           45.00 |           41.52 |   24,087,275.56 |
|               3 |           45.00 |           71.89 |   13,910,413.33 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,011,329,800.00 |
|               2 |            0.00 |            0.00 |1,083,927,400.00 |
|               3 |            0.00 |            0.00 |  625,968,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,011,329,800.00 |
|               2 |            0.00 |            0.00 |1,083,927,400.00 |
|               3 |            0.00 |            0.00 |  625,968,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,012.00 |        1,000.66 |      999,337.75 |
|               2 |        1,083.00 |          999.14 |    1,000,856.33 |
|               3 |          626.00 |        1,000.05 |      999,949.84 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      638,176.00 |      631,026.59 |        1,584.72 |
|               2 |      638,176.00 |      588,762.68 |        1,698.48 |
|               3 |      638,176.00 |    1,019,501.62 |          980.87 |


