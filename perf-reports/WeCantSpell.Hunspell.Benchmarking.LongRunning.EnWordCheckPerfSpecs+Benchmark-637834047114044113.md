# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/20/2022 8:25:11 PM_
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
|TotalBytesAllocated |           bytes |    7,870,016.00 |    7,869,946.67 |    7,869,872.00 |           72.15 |
|TotalCollections [Gen0] |     collections |           43.00 |           43.00 |           43.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,046.00 |          876.67 |          604.00 |          238.43 |
|[Counter] WordsChecked |      operations |      613,312.00 |      613,312.00 |      613,312.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   13,030,905.44 |    9,530,663.13 |    7,526,477.44 |    3,041,927.18 |
|TotalCollections [Gen0] |     collections |           71.20 |           52.07 |           41.12 |           16.62 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.50 |        1,000.31 |        1,000.10 |            0.20 |
|[Counter] WordsChecked |      operations |    1,015,519.78 |      742,734.36 |      586,544.74 |      237,067.14 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,870,016.00 |    8,034,606.52 |          124.46 |
|               2 |    7,869,952.00 |    7,526,477.44 |          132.86 |
|               3 |    7,869,872.00 |   13,030,905.44 |           76.74 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           43.00 |           43.90 |   22,779,413.95 |
|               2 |           43.00 |           41.12 |   24,317,104.65 |
|               3 |           43.00 |           71.20 |   14,045,093.02 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  979,514,800.00 |
|               2 |            0.00 |            0.00 |1,045,635,500.00 |
|               3 |            0.00 |            0.00 |  603,939,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  979,514,800.00 |
|               2 |            0.00 |            0.00 |1,045,635,500.00 |
|               3 |            0.00 |            0.00 |  603,939,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          980.00 |        1,000.50 |      999,504.90 |
|               2 |        1,046.00 |        1,000.35 |      999,651.53 |
|               3 |          604.00 |        1,000.10 |      999,899.01 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      613,312.00 |      626,138.57 |        1,597.09 |
|               2 |      613,312.00 |      586,544.74 |        1,704.90 |
|               3 |      613,312.00 |    1,015,519.78 |          984.72 |


