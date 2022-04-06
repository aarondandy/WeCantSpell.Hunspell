# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_04/06/2022 20:06:01_
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
|TotalBytesAllocated |           bytes |  120,540,968.00 |  119,982,776.00 |  119,424,584.00 |      789,402.70 |
|TotalCollections [Gen0] |     collections |          484.00 |          484.00 |          484.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          189.00 |          188.50 |          188.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           46.00 |           45.50 |           45.00 |            0.71 |
|    Elapsed Time |              ms |       18,217.00 |       18,139.50 |       18,062.00 |          109.60 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,674,011.60 |    6,614,769.26 |    6,555,526.92 |       83,781.32 |
|TotalCollections [Gen0] |     collections |           26.80 |           26.68 |           26.57 |            0.16 |
|TotalCollections [Gen1] |     collections |           10.46 |           10.39 |           10.32 |            0.10 |
|TotalCollections [Gen2] |     collections |            2.53 |            2.51 |            2.49 |            0.02 |
|    Elapsed Time |              ms |        1,000.04 |        1,000.01 |          999.98 |            0.04 |
|[Counter] FilePairsLoaded |      operations |            3.27 |            3.25 |            3.24 |            0.02 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  120,540,968.00 |    6,674,011.60 |          149.83 |
|               2 |  119,424,584.00 |    6,555,526.92 |          152.54 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          484.00 |           26.80 |   37,316,624.79 |
|               2 |          484.00 |           26.57 |   37,639,233.47 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          189.00 |           10.46 |   95,562,150.26 |
|               2 |          188.00 |           10.32 |   96,901,005.32 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           45.00 |            2.49 |  401,361,031.11 |
|               2 |           46.00 |            2.53 |  396,030,195.65 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,062.00 |        1,000.04 |      999,958.28 |
|               2 |       18,217.00 |          999.98 |    1,000,021.35 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.27 |  306,122,820.34 |
|               2 |           59.00 |            3.24 |  308,769,305.08 |


