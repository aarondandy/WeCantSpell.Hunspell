# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_2/21/2022 6:34:28 PM_
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
|TotalBytesAllocated |           bytes |    7,194,608.00 |    7,194,605.33 |    7,194,600.00 |            4.62 |
|TotalCollections [Gen0] |     collections |           76.00 |           76.00 |           76.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          927.00 |          907.33 |          886.00 |           20.55 |
|[Counter] WordsChecked |      operations |      953,120.00 |      953,120.00 |      953,120.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,123,983.74 |    7,935,008.48 |    7,764,633.84 |      180,395.61 |
|TotalCollections [Gen0] |     collections |           85.82 |           83.82 |           82.02 |            1.91 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.45 |        1,000.36 |        1,000.20 |            0.15 |
|[Counter] WordsChecked |      operations |    1,076,242.10 |    1,051,206.42 |    1,028,635.31 |       23,898.89 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,194,600.00 |    8,123,983.74 |          123.09 |
|               2 |    7,194,608.00 |    7,916,407.86 |          126.32 |
|               3 |    7,194,608.00 |    7,764,633.84 |          128.79 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           76.00 |           85.82 |   11,652,631.58 |
|               2 |           76.00 |           83.62 |   11,958,188.16 |
|               3 |           76.00 |           82.02 |   12,191,932.89 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  885,600,000.00 |
|               2 |            0.00 |            0.00 |  908,822,300.00 |
|               3 |            0.00 |            0.00 |  926,586,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  885,600,000.00 |
|               2 |            0.00 |            0.00 |  908,822,300.00 |
|               3 |            0.00 |            0.00 |  926,586,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          886.00 |        1,000.45 |      999,548.53 |
|               2 |          909.00 |        1,000.20 |      999,804.51 |
|               3 |          927.00 |        1,000.45 |      999,554.37 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      953,120.00 |    1,076,242.10 |          929.16 |
|               2 |      953,120.00 |    1,048,741.87 |          953.52 |
|               3 |      953,120.00 |    1,028,635.31 |          972.16 |


