# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/17/2022 1:41:52 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.3,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    1,054,496.00 |    1,054,496.00 |    1,054,496.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           45.00 |           45.00 |           45.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          637.00 |          635.00 |          632.00 |            2.65 |
|[Counter] WordsChecked |      operations |      629,888.00 |      629,888.00 |      629,888.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,668,502.90 |    1,661,011.01 |    1,654,126.94 |        7,207.23 |
|TotalCollections [Gen0] |     collections |           71.20 |           70.88 |           70.59 |            0.31 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,001.44 |        1,000.22 |          999.23 |            1.13 |
|[Counter] WordsChecked |      operations |      996,656.18 |      992,181.01 |      988,068.91 |        4,305.13 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,054,496.00 |    1,654,126.94 |          604.55 |
|               2 |    1,054,496.00 |    1,668,502.90 |          599.34 |
|               3 |    1,054,496.00 |    1,660,403.19 |          602.26 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           45.00 |           70.59 |   14,166,533.33 |
|               2 |           45.00 |           71.20 |   14,044,473.33 |
|               3 |           45.00 |           70.86 |   14,112,984.44 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  637,494,000.00 |
|               2 |            0.00 |            0.00 |  632,001,300.00 |
|               3 |            0.00 |            0.00 |  635,084,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  637,494,000.00 |
|               2 |            0.00 |            0.00 |  632,001,300.00 |
|               3 |            0.00 |            0.00 |  635,084,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          637.00 |          999.23 |    1,000,775.51 |
|               2 |          632.00 |        1,000.00 |    1,000,002.06 |
|               3 |          636.00 |        1,001.44 |      998,560.22 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      629,888.00 |      988,068.91 |        1,012.08 |
|               2 |      629,888.00 |      996,656.18 |        1,003.36 |
|               3 |      629,888.00 |      991,817.94 |        1,008.25 |


