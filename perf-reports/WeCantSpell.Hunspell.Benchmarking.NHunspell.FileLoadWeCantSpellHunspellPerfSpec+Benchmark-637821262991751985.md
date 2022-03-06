# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/06/2022 01:18:19_
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
|TotalBytesAllocated |           bytes |   40,213,512.00 |   40,211,180.00 |   40,208,848.00 |        3,297.95 |
|TotalCollections [Gen0] |     collections |          743.00 |          741.50 |          740.00 |            2.12 |
|TotalCollections [Gen1] |     collections |          299.00 |          298.00 |          297.00 |            1.41 |
|TotalCollections [Gen2] |     collections |           82.00 |           81.00 |           80.00 |            1.41 |
|    Elapsed Time |              ms |       18,887.00 |       18,886.50 |       18,886.00 |            0.71 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,129,256.51 |    2,129,166.17 |    2,129,075.82 |          127.77 |
|TotalCollections [Gen0] |     collections |           39.34 |           39.26 |           39.18 |            0.11 |
|TotalCollections [Gen1] |     collections |           15.83 |           15.78 |           15.73 |            0.07 |
|TotalCollections [Gen2] |     collections |            4.34 |            4.29 |            4.24 |            0.07 |
|    Elapsed Time |              ms |        1,000.04 |        1,000.03 |        1,000.02 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.12 |            3.12 |            3.12 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   40,213,512.00 |    2,129,256.51 |          469.65 |
|               2 |   40,208,848.00 |    2,129,075.82 |          469.69 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          743.00 |           39.34 |   25,418,809.42 |
|               2 |          740.00 |           39.18 |   25,521,064.32 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          299.00 |           15.83 |   63,164,466.22 |
|               2 |          297.00 |           15.73 |   63,587,837.04 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           82.00 |            4.34 |  230,319,212.20 |
|               2 |           80.00 |            4.24 |  236,069,845.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,887.00 |        1,000.04 |      999,956.34 |
|               2 |       18,886.00 |        1,000.02 |      999,978.16 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.12 |  320,104,667.80 |
|               2 |           59.00 |            3.12 |  320,094,705.08 |


