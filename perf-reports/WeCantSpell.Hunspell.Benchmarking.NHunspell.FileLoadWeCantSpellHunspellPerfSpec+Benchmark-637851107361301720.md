# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_04/09/2022 14:18:56_
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
|TotalBytesAllocated |           bytes |  110,990,656.00 |  110,516,044.00 |  110,041,432.00 |      671,202.73 |
|TotalCollections [Gen0] |     collections |          489.00 |          485.50 |          482.00 |            4.95 |
|TotalCollections [Gen1] |     collections |          191.00 |          187.50 |          184.00 |            4.95 |
|TotalCollections [Gen2] |     collections |           47.00 |           44.00 |           41.00 |            4.24 |
|    Elapsed Time |              ms |       15,214.00 |       15,150.00 |       15,086.00 |           90.51 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,295,509.10 |    7,294,827.68 |    7,294,146.25 |          963.68 |
|TotalCollections [Gen0] |     collections |           32.41 |           32.05 |           31.68 |            0.52 |
|TotalCollections [Gen1] |     collections |           12.66 |           12.38 |           12.09 |            0.40 |
|TotalCollections [Gen2] |     collections |            3.12 |            2.91 |            2.69 |            0.30 |
|    Elapsed Time |              ms |        1,000.03 |        1,000.01 |          999.98 |            0.03 |
|[Counter] FilePairsLoaded |      operations |            3.91 |            3.89 |            3.88 |            0.02 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  110,990,656.00 |    7,295,509.10 |          137.07 |
|               2 |  110,041,432.00 |    7,294,146.25 |          137.10 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          482.00 |           31.68 |   31,563,399.79 |
|               2 |          489.00 |           32.41 |   30,851,260.12 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          184.00 |           12.09 |   82,682,384.24 |
|               2 |          191.00 |           12.66 |   78,985,686.91 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           41.00 |            2.69 |  371,062,407.32 |
|               2 |           47.00 |            3.12 |  320,984,387.23 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,214.00 |        1,000.03 |      999,970.99 |
|               2 |       15,086.00 |          999.98 |    1,000,017.65 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.88 |  257,856,927.12 |
|               2 |           59.00 |            3.91 |  255,699,427.12 |


