# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_5/8/2022 5:47:12 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.4,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    1,983,072.00 |    1,983,002.67 |    1,982,968.00 |           60.04 |
|TotalCollections [Gen0] |     collections |           10.00 |           10.00 |           10.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          757.00 |          756.00 |          754.00 |            1.73 |
|[Counter] WordsChecked |      operations |      944,832.00 |      944,832.00 |      944,832.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,628,789.44 |    2,623,857.73 |    2,620,028.79 |        4,483.22 |
|TotalCollections [Gen0] |     collections |           13.26 |           13.23 |           13.21 |            0.02 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,001.24 |        1,000.32 |          999.51 |            0.87 |
|[Counter] WordsChecked |      operations |    1,252,483.21 |    1,250,177.17 |    1,248,374.68 |        2,100.04 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,983,072.00 |    2,628,789.44 |          380.40 |
|               2 |    1,982,968.00 |    2,622,754.98 |          381.28 |
|               3 |    1,982,968.00 |    2,620,028.79 |          381.68 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           10.00 |           13.26 |   75,436,700.00 |
|               2 |           10.00 |           13.23 |   75,606,300.00 |
|               3 |           10.00 |           13.21 |   75,684,970.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  754,367,000.00 |
|               2 |            0.00 |            0.00 |  756,063,000.00 |
|               3 |            0.00 |            0.00 |  756,849,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  754,367,000.00 |
|               2 |            0.00 |            0.00 |  756,063,000.00 |
|               3 |            0.00 |            0.00 |  756,849,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          754.00 |          999.51 |    1,000,486.74 |
|               2 |          757.00 |        1,001.24 |      998,762.22 |
|               3 |          757.00 |        1,000.20 |      999,801.45 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      944,832.00 |    1,252,483.21 |          798.41 |
|               2 |      944,832.00 |    1,249,673.64 |          800.21 |
|               3 |      944,832.00 |    1,248,374.68 |          801.04 |


