# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_4/17/2022 5:03:11 PM_
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
|TotalBytesAllocated |           bytes |    3,875,328.00 |    3,875,280.00 |    3,875,256.00 |           41.57 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          807.00 |          805.00 |          803.00 |            2.00 |
|[Counter] WordsChecked |      operations |      919,968.00 |      919,968.00 |      919,968.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,824,807.10 |    4,814,757.83 |    4,803,439.51 |       10,740.17 |
|TotalCollections [Gen0] |     collections |           16.19 |           16.15 |           16.11 |            0.04 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.42 |        1,000.15 |          999.74 |            0.36 |
|[Counter] WordsChecked |      operations |    1,145,365.79 |    1,142,994.33 |    1,140,314.51 |        2,539.72 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,875,328.00 |    4,824,807.10 |          207.26 |
|               2 |    3,875,256.00 |    4,803,439.51 |          208.18 |
|               3 |    3,875,256.00 |    4,816,026.88 |          207.64 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |           16.19 |   61,785,300.00 |
|               2 |           13.00 |           16.11 |   62,058,992.31 |
|               3 |           13.00 |           16.16 |   61,896,792.31 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  803,208,900.00 |
|               2 |            0.00 |            0.00 |  806,766,900.00 |
|               3 |            0.00 |            0.00 |  804,658,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  803,208,900.00 |
|               2 |            0.00 |            0.00 |  806,766,900.00 |
|               3 |            0.00 |            0.00 |  804,658,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          803.00 |          999.74 |    1,000,260.15 |
|               2 |          807.00 |        1,000.29 |      999,711.15 |
|               3 |          805.00 |        1,000.42 |      999,575.53 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      919,968.00 |    1,145,365.79 |          873.08 |
|               2 |      919,968.00 |    1,140,314.51 |          876.95 |
|               3 |      919,968.00 |    1,143,302.69 |          874.66 |


