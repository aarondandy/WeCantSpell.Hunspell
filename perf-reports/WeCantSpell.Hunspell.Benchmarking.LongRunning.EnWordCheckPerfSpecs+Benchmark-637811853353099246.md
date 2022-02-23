# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_2/23/2022 3:55:35 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.2,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    1,466,008.00 |    1,465,890.67 |    1,465,656.00 |          203.23 |
|TotalCollections [Gen0] |     collections |           70.00 |           70.00 |           70.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          860.00 |          857.67 |          854.00 |            3.21 |
|[Counter] WordsChecked |      operations |      870,240.00 |      870,240.00 |      870,240.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,716,495.90 |    1,709,414.35 |    1,705,170.96 |        6,172.92 |
|TotalCollections [Gen0] |     collections |           81.96 |           81.63 |           81.44 |            0.29 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.54 |        1,000.14 |          999.92 |            0.35 |
|[Counter] WordsChecked |      operations |    1,018,932.64 |    1,014,809.98 |    1,012,453.11 |        3,582.54 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,466,008.00 |    1,706,576.20 |          585.97 |
|               2 |    1,465,656.00 |    1,705,170.96 |          586.45 |
|               3 |    1,466,008.00 |    1,716,495.90 |          582.58 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           70.00 |           81.49 |   12,271,922.86 |
|               2 |           70.00 |           81.44 |   12,279,087.14 |
|               3 |           70.00 |           81.96 |   12,201,002.86 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  859,034,600.00 |
|               2 |            0.00 |            0.00 |  859,536,100.00 |
|               3 |            0.00 |            0.00 |  854,070,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  859,034,600.00 |
|               2 |            0.00 |            0.00 |  859,536,100.00 |
|               3 |            0.00 |            0.00 |  854,070,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          859.00 |          999.96 |    1,000,040.28 |
|               2 |          860.00 |        1,000.54 |      999,460.58 |
|               3 |          854.00 |          999.92 |    1,000,082.20 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      870,240.00 |    1,013,044.18 |          987.12 |
|               2 |      870,240.00 |    1,012,453.11 |          987.70 |
|               3 |      870,240.00 |    1,018,932.64 |          981.42 |


