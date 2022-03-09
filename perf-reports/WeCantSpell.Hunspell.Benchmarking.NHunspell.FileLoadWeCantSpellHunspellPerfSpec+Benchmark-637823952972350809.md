# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/09/2022 04:01:37_
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
|TotalBytesAllocated |           bytes |   30,697,000.00 |   30,695,600.00 |   30,694,200.00 |        1,979.90 |
|TotalCollections [Gen0] |     collections |          505.00 |          505.00 |          505.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          212.00 |          212.00 |          212.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           69.00 |           69.00 |           69.00 |            0.00 |
|    Elapsed Time |              ms |       15,733.00 |       15,554.00 |       15,375.00 |          253.14 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,996,464.25 |    1,973,797.96 |    1,951,131.67 |       32,054.98 |
|TotalCollections [Gen0] |     collections |           32.85 |           32.47 |           32.10 |            0.53 |
|TotalCollections [Gen1] |     collections |           13.79 |           13.63 |           13.47 |            0.22 |
|TotalCollections [Gen2] |     collections |            4.49 |            4.44 |            4.39 |            0.07 |
|    Elapsed Time |              ms |        1,000.05 |        1,000.03 |        1,000.01 |            0.03 |
|[Counter] FilePairsLoaded |      operations |            3.84 |            3.79 |            3.75 |            0.06 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,697,000.00 |    1,951,131.67 |          512.52 |
|               2 |   30,694,200.00 |    1,996,464.25 |          500.89 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          505.00 |           32.10 |   31,154,298.61 |
|               2 |          505.00 |           32.85 |   30,444,118.42 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          212.00 |           13.47 |   74,211,890.57 |
|               2 |          212.00 |           13.79 |   72,520,187.74 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           69.00 |            4.39 |  228,013,344.93 |
|               2 |           69.00 |            4.49 |  222,815,649.28 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,733.00 |        1,000.01 |      999,994.97 |
|               2 |       15,375.00 |        1,000.05 |      999,953.16 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.75 |  266,659,674.58 |
|               2 |           59.00 |            3.84 |  260,581,013.56 |


