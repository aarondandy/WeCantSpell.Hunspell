# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_4/19/2022 2:45:01 AM_
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
|TotalBytesAllocated |           bytes |    7,184,896.00 |    7,184,829.33 |    7,184,768.00 |           64.17 |
|TotalCollections [Gen0] |     collections |           12.00 |           12.00 |           12.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          785.00 |          783.33 |          781.00 |            2.08 |
|[Counter] WordsChecked |      operations |      878,528.00 |      878,528.00 |      878,528.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,192,301.64 |    9,169,678.10 |    9,153,654.19 |       20,151.25 |
|TotalCollections [Gen0] |     collections |           15.35 |           15.32 |           15.29 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.11 |          999.73 |          999.21 |            0.47 |
|[Counter] WordsChecked |      operations |    1,123,982.08 |    1,121,226.20 |    1,119,267.71 |        2,456.26 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,184,768.00 |    9,163,078.48 |          109.13 |
|               2 |    7,184,824.00 |    9,153,654.19 |          109.25 |
|               3 |    7,184,896.00 |    9,192,301.64 |          108.79 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           12.00 |           15.30 |   65,341,650.00 |
|               2 |           12.00 |           15.29 |   65,409,433.33 |
|               3 |           12.00 |           15.35 |   65,135,083.33 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  784,099,800.00 |
|               2 |            0.00 |            0.00 |  784,913,200.00 |
|               3 |            0.00 |            0.00 |  781,621,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  784,099,800.00 |
|               2 |            0.00 |            0.00 |  784,913,200.00 |
|               3 |            0.00 |            0.00 |  781,621,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          784.00 |          999.87 |    1,000,127.30 |
|               2 |          785.00 |        1,000.11 |      999,889.43 |
|               3 |          781.00 |          999.21 |    1,000,795.13 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      878,528.00 |    1,120,428.80 |          892.52 |
|               2 |      878,528.00 |    1,119,267.71 |          893.44 |
|               3 |      878,528.00 |    1,123,982.08 |          889.69 |


