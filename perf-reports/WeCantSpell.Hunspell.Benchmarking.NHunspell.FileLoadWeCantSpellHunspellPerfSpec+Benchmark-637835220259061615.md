# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/22/2022 05:00:25_
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
|TotalBytesAllocated |           bytes |   90,554,808.00 |   90,554,460.00 |   90,554,112.00 |          492.15 |
|TotalCollections [Gen0] |     collections |          487.00 |          487.00 |          487.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          188.00 |          188.00 |          188.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           45.00 |           45.00 |           45.00 |            0.00 |
|    Elapsed Time |              ms |       20,289.00 |       20,272.00 |       20,255.00 |           24.04 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,470,834.13 |    4,467,096.58 |    4,463,359.03 |        5,285.69 |
|TotalCollections [Gen0] |     collections |           24.04 |           24.02 |           24.00 |            0.03 |
|TotalCollections [Gen1] |     collections |            9.28 |            9.27 |            9.27 |            0.01 |
|TotalCollections [Gen2] |     collections |            2.22 |            2.22 |            2.22 |            0.00 |
|    Elapsed Time |              ms |        1,000.03 |        1,000.03 |        1,000.03 |            0.00 |
|[Counter] FilePairsLoaded |      operations |            2.91 |            2.91 |            2.91 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   90,554,112.00 |    4,470,834.13 |          223.67 |
|               2 |   90,554,808.00 |    4,463,359.03 |          224.05 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          487.00 |           24.04 |   41,590,166.32 |
|               2 |          487.00 |           24.00 |   41,660,140.45 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          188.00 |            9.28 |  107,736,228.72 |
|               2 |          188.00 |            9.27 |  107,917,491.49 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           45.00 |            2.22 |  450,098,022.22 |
|               2 |           45.00 |            2.22 |  450,855,297.78 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       20,255.00 |        1,000.03 |      999,970.92 |
|               2 |       20,289.00 |        1,000.03 |      999,974.78 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            2.91 |  343,295,101.69 |
|               2 |           59.00 |            2.91 |  343,872,684.75 |


