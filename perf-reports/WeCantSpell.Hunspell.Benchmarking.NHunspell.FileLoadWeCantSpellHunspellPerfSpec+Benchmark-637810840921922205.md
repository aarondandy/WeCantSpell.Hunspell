# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_02/21/2022 23:48:12_
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
|TotalBytesAllocated |           bytes |   31,034,776.00 |   31,011,292.00 |   30,987,808.00 |       33,211.39 |
|TotalCollections [Gen0] |     collections |        1,118.00 |        1,115.50 |        1,113.00 |            3.54 |
|TotalCollections [Gen1] |     collections |          375.00 |          374.50 |          374.00 |            0.71 |
|TotalCollections [Gen2] |     collections |          105.00 |          102.50 |          100.00 |            3.54 |
|    Elapsed Time |              ms |       19,100.00 |       19,008.50 |       18,917.00 |          129.40 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,638,117.33 |    1,631,484.79 |    1,624,852.25 |        9,379.83 |
|TotalCollections [Gen0] |     collections |           58.84 |           58.69 |           58.53 |            0.21 |
|TotalCollections [Gen1] |     collections |           19.77 |           19.70 |           19.63 |            0.10 |
|TotalCollections [Gen2] |     collections |            5.50 |            5.39 |            5.29 |            0.15 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.01 |        1,000.00 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.12 |            3.10 |            3.09 |            0.02 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,987,808.00 |    1,638,117.33 |          610.46 |
|               2 |   31,034,776.00 |    1,624,852.25 |          615.44 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,113.00 |           58.84 |   16,996,155.17 |
|               2 |        1,118.00 |           58.53 |   17,084,132.65 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          374.00 |           19.77 |   50,579,467.11 |
|               2 |          375.00 |           19.63 |   50,933,494.13 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          100.00 |            5.29 |  189,167,207.00 |
|               2 |          105.00 |            5.50 |  181,905,336.19 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,917.00 |        1,000.01 |      999,985.24 |
|               2 |       19,100.00 |        1,000.00 |    1,000,003.16 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.12 |  320,622,384.75 |
|               2 |           59.00 |            3.09 |  323,729,835.59 |


