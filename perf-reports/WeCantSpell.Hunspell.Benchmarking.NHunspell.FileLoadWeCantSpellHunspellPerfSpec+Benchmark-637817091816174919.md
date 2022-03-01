# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/01/2022 05:26:21_
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
|TotalBytesAllocated |           bytes |  149,740,280.00 |  149,738,412.00 |  149,736,544.00 |        2,641.75 |
|TotalCollections [Gen0] |     collections |          937.00 |          937.00 |          937.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          342.00 |          342.00 |          342.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           90.00 |           90.00 |           90.00 |            0.00 |
|    Elapsed Time |              ms |       19,254.00 |       19,200.00 |       19,146.00 |           76.37 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,820,891.86 |    7,798,937.34 |    7,776,982.81 |       31,048.39 |
|TotalCollections [Gen0] |     collections |           48.94 |           48.80 |           48.67 |            0.19 |
|TotalCollections [Gen1] |     collections |           17.86 |           17.81 |           17.76 |            0.07 |
|TotalCollections [Gen2] |     collections |            4.70 |            4.69 |            4.67 |            0.02 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.00 |          999.99 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.08 |            3.07 |            3.06 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  149,740,280.00 |    7,820,891.86 |          127.86 |
|               2 |  149,736,544.00 |    7,776,982.81 |          128.58 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          937.00 |           48.94 |   20,433,500.32 |
|               2 |          937.00 |           48.67 |   20,548,355.71 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          342.00 |           17.86 |   55,983,011.11 |
|               2 |          342.00 |           17.76 |   56,297,688.01 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           90.00 |            4.70 |  212,735,442.22 |
|               2 |           90.00 |            4.67 |  213,931,214.44 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       19,146.00 |          999.99 |    1,000,009.91 |
|               2 |       19,254.00 |        1,000.01 |      999,990.10 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.08 |  324,511,691.53 |
|               2 |           59.00 |            3.06 |  326,335,750.85 |


