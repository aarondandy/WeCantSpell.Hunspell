# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/13/2022 4:20:12 AM_
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
|TotalBytesAllocated |           bytes |    6,723,672.00 |    6,723,301.33 |    6,723,024.00 |          333.93 |
|TotalCollections [Gen0] |     collections |           63.00 |           63.00 |           63.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,073.00 |          893.00 |          801.00 |          155.90 |
|[Counter] WordsChecked |      operations |      770,784.00 |      770,784.00 |      770,784.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,397,690.74 |    7,674,129.71 |    6,269,900.49 |    1,216,287.27 |
|TotalCollections [Gen0] |     collections |           78.69 |           71.91 |           58.75 |           11.40 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.58 |        1,000.49 |        1,000.38 |            0.10 |
|[Counter] WordsChecked |      operations |      962,755.53 |      879,794.88 |      718,764.83 |      139,477.55 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,723,672.00 |    6,269,900.49 |          159.49 |
|               2 |    6,723,024.00 |    8,354,797.91 |          119.69 |
|               3 |    6,723,208.00 |    8,397,690.74 |          119.08 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           63.00 |           58.75 |   17,021,793.65 |
|               2 |           63.00 |           78.29 |   12,772,860.32 |
|               3 |           63.00 |           78.69 |   12,707,968.25 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,072,373,000.00 |
|               2 |            0.00 |            0.00 |  804,690,200.00 |
|               3 |            0.00 |            0.00 |  800,602,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,072,373,000.00 |
|               2 |            0.00 |            0.00 |  804,690,200.00 |
|               3 |            0.00 |            0.00 |  800,602,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,073.00 |        1,000.58 |      999,415.66 |
|               2 |          805.00 |        1,000.38 |      999,615.16 |
|               3 |          801.00 |        1,000.50 |      999,503.12 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      770,784.00 |      718,764.83 |        1,391.28 |
|               2 |      770,784.00 |      957,864.28 |        1,043.99 |
|               3 |      770,784.00 |      962,755.53 |        1,038.69 |


