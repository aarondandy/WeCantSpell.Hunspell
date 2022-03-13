# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/13/2022 04:23:11_
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
NumberOfIterations=3, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,027.00 |        1,008.33 |          994.00 |           16.92 |
|[Counter] _wordsChecked |      operations |    1,301,216.00 |    1,301,216.00 |    1,301,216.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.48 |          999.96 |          999.49 |            0.50 |
|[Counter] _wordsChecked |      operations |    1,308,958.36 |    1,290,658.45 |    1,266,360.69 |       21,923.07 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,516,500.00 |
|               2 |            0.00 |            0.00 |1,027,524,000.00 |
|               3 |            0.00 |            0.00 |  994,085,100.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,516,500.00 |
|               2 |            0.00 |            0.00 |1,027,524,000.00 |
|               3 |            0.00 |            0.00 |  994,085,100.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,516,500.00 |
|               2 |            0.00 |            0.00 |1,027,524,000.00 |
|               3 |            0.00 |            0.00 |  994,085,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,516,500.00 |
|               2 |            0.00 |            0.00 |1,027,524,000.00 |
|               3 |            0.00 |            0.00 |  994,085,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,004.00 |        1,000.48 |      999,518.43 |
|               2 |        1,027.00 |          999.49 |    1,000,510.22 |
|               3 |          994.00 |          999.91 |    1,000,085.61 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,301,216.00 |    1,296,656.31 |          771.21 |
|               2 |    1,301,216.00 |    1,266,360.69 |          789.66 |
|               3 |    1,301,216.00 |    1,308,958.36 |          763.97 |


