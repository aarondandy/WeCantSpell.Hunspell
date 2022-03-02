# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/02/2022 03:10:18_
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
NumberOfIterations=2, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |  149,543,664.00 |  149,543,624.00 |  149,543,584.00 |           56.57 |
|TotalCollections [Gen0] |     collections |          904.00 |          903.50 |          903.00 |            0.71 |
|TotalCollections [Gen1] |     collections |          331.00 |          331.00 |          331.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           82.00 |           82.00 |           82.00 |            0.00 |
|    Elapsed Time |              ms |       20,126.00 |       19,825.50 |       19,525.00 |          424.97 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,658,967.08 |    7,544,719.55 |    7,430,472.02 |      161,570.41 |
|TotalCollections [Gen0] |     collections |           46.25 |           45.58 |           44.92 |            0.94 |
|TotalCollections [Gen1] |     collections |           16.95 |           16.70 |           16.45 |            0.36 |
|TotalCollections [Gen2] |     collections |            4.20 |            4.14 |            4.07 |            0.09 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.00 |          999.98 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.02 |            2.98 |            2.93 |            0.06 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  149,543,664.00 |    7,430,472.02 |          134.58 |
|               2 |  149,543,584.00 |    7,658,967.08 |          130.57 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          904.00 |           44.92 |   22,262,974.23 |
|               2 |          903.00 |           46.25 |   21,622,695.46 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          331.00 |           16.45 |   60,802,805.74 |
|               2 |          331.00 |           16.95 |   58,988,803.63 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           82.00 |            4.07 |  245,435,715.85 |
|               2 |           82.00 |            4.20 |  238,113,341.46 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       20,126.00 |        1,000.01 |      999,986.52 |
|               2 |       19,525.00 |          999.98 |    1,000,015.06 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            2.93 |  341,114,045.76 |
|               2 |           59.00 |            3.02 |  330,937,186.44 |


