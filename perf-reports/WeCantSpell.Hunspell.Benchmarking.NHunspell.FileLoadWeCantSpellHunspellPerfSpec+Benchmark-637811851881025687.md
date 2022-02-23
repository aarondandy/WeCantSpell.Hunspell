# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_02/23/2022 03:53:08_
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
|TotalBytesAllocated |           bytes |  127,145,472.00 |   78,827,056.00 |   30,508,640.00 |   68,332,559.22 |
|TotalCollections [Gen0] |     collections |        1,224.00 |        1,218.50 |        1,213.00 |            7.78 |
|TotalCollections [Gen1] |     collections |          395.00 |          393.50 |          392.00 |            2.12 |
|TotalCollections [Gen2] |     collections |          116.00 |          111.50 |          107.00 |            6.36 |
|    Elapsed Time |              ms |       21,408.00 |       21,371.50 |       21,335.00 |           51.62 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,959,328.50 |    3,692,191.57 |    1,425,054.64 |    3,206,215.80 |
|TotalCollections [Gen0] |     collections |           57.17 |           57.01 |           56.85 |            0.23 |
|TotalCollections [Gen1] |     collections |           18.45 |           18.41 |           18.37 |            0.05 |
|TotalCollections [Gen2] |     collections |            5.42 |            5.22 |            5.02 |            0.29 |
|    Elapsed Time |              ms |          999.97 |          999.97 |          999.96 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            2.77 |            2.76 |            2.76 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  127,145,472.00 |    5,959,328.50 |          167.80 |
|               2 |   30,508,640.00 |    1,425,054.64 |          701.73 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,213.00 |           56.85 |   17,589,065.70 |
|               2 |        1,224.00 |           57.17 |   17,490,809.72 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          392.00 |           18.37 |   54,427,389.54 |
|               2 |          395.00 |           18.45 |   54,199,369.87 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          107.00 |            5.02 |  199,397,539.25 |
|               2 |          116.00 |            5.42 |  184,558,199.14 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       21,335.00 |          999.97 |    1,000,025.16 |
|               2 |       21,408.00 |          999.96 |    1,000,035.09 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            2.77 |  361,619,266.10 |
|               2 |           59.00 |            2.76 |  362,860,188.14 |


