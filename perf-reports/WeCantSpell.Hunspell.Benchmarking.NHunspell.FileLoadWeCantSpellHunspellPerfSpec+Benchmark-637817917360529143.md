# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/02/2022 04:22:16_
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
|TotalBytesAllocated |           bytes |  149,547,304.00 |  149,546,496.00 |  149,545,688.00 |        1,142.68 |
|TotalCollections [Gen0] |     collections |          826.00 |          825.50 |          825.00 |            0.71 |
|TotalCollections [Gen1] |     collections |          321.00 |          320.00 |          319.00 |            1.41 |
|TotalCollections [Gen2] |     collections |           81.00 |           80.50 |           80.00 |            0.71 |
|    Elapsed Time |              ms |       19,665.00 |       19,595.50 |       19,526.00 |           98.29 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,658,910.52 |    7,631,905.27 |    7,604,900.02 |       38,191.19 |
|TotalCollections [Gen0] |     collections |           42.30 |           42.13 |           41.95 |            0.25 |
|TotalCollections [Gen1] |     collections |           16.44 |           16.33 |           16.22 |            0.15 |
|TotalCollections [Gen2] |     collections |            4.15 |            4.11 |            4.07 |            0.06 |
|    Elapsed Time |              ms |        1,000.03 |        1,000.02 |        1,000.00 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.02 |            3.01 |            3.00 |            0.02 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  149,545,688.00 |    7,604,900.02 |          131.49 |
|               2 |  149,547,304.00 |    7,658,910.52 |          130.57 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          825.00 |           41.95 |   23,835,619.15 |
|               2 |          826.00 |           42.30 |   23,639,133.05 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          319.00 |           16.22 |   61,643,842.63 |
|               2 |          321.00 |           16.44 |   60,828,423.36 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           80.00 |            4.07 |  245,804,822.50 |
|               2 |           81.00 |            4.15 |  241,060,788.89 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       19,665.00 |        1,000.03 |      999,968.77 |
|               2 |       19,526.00 |        1,000.00 |      999,996.10 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.00 |  333,294,674.58 |
|               2 |           59.00 |            3.02 |  330,947,862.71 |


