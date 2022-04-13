# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_04/13/2022 23:09:55_
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
|TotalBytesAllocated |           bytes |  119,441,512.00 |  105,026,524.00 |   90,611,536.00 |   20,385,871.53 |
|TotalCollections [Gen0] |     collections |          486.00 |          484.00 |          482.00 |            2.83 |
|TotalCollections [Gen1] |     collections |          190.00 |          188.00 |          186.00 |            2.83 |
|TotalCollections [Gen2] |     collections |           48.00 |           46.00 |           44.00 |            2.83 |
|    Elapsed Time |              ms |       19,996.00 |       19,982.00 |       19,968.00 |           19.80 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,973,465.48 |    5,255,718.83 |    4,537,972.17 |    1,015,047.05 |
|TotalCollections [Gen0] |     collections |           24.31 |           24.22 |           24.14 |            0.12 |
|TotalCollections [Gen1] |     collections |            9.50 |            9.41 |            9.32 |            0.13 |
|TotalCollections [Gen2] |     collections |            2.40 |            2.30 |            2.20 |            0.14 |
|    Elapsed Time |              ms |        1,000.03 |        1,000.03 |        1,000.03 |            0.00 |
|[Counter] FilePairsLoaded |      operations |            2.95 |            2.95 |            2.95 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   90,611,536.00 |    4,537,972.17 |          220.36 |
|               2 |  119,441,512.00 |    5,973,465.48 |          167.41 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          482.00 |           24.14 |   41,426,154.98 |
|               2 |          486.00 |           24.31 |   41,142,688.27 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          186.00 |            9.32 |  107,351,648.92 |
|               2 |          190.00 |            9.50 |  105,238,665.79 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           44.00 |            2.20 |  453,804,697.73 |
|               2 |           48.00 |            2.40 |  416,569,718.75 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       19,968.00 |        1,000.03 |      999,970.29 |
|               2 |       19,996.00 |        1,000.03 |      999,967.32 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            2.95 |  338,430,622.03 |
|               2 |           59.00 |            2.95 |  338,904,177.97 |


