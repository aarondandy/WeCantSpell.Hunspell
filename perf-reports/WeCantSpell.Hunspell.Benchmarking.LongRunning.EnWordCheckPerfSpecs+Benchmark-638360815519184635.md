# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_11/20/2023 12:52:31 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.22631.0
ProcessorCount=16
CLR=6.0.25,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    6,178,080.00 |    6,178,029.33 |    6,177,928.00 |           87.76 |
|TotalCollections [Gen0] |     collections |           10.00 |           10.00 |           10.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] WordsChecked |      operations |      671,328.00 |      671,328.00 |      671,328.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,549,016.04 |    7,079,767.55 |    6,187,793.82 |      772,822.08 |
|TotalCollections [Gen0] |     collections |           12.22 |           11.46 |           10.02 |            1.25 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] WordsChecked |      operations |      820,297.87 |      769,313.49 |      672,400.07 |       83,967.53 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,177,928.00 |    6,187,793.82 |          161.61 |
|               2 |    6,178,080.00 |    7,549,016.04 |          132.47 |
|               3 |    6,178,080.00 |    7,502,492.80 |          133.29 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           10.00 |           10.02 |   99,840,560.00 |
|               2 |           10.00 |           12.22 |   81,839,540.00 |
|               3 |           10.00 |           12.14 |   82,347,030.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  998,405,600.00 |
|               2 |            0.00 |            0.00 |  818,395,400.00 |
|               3 |            0.00 |            0.00 |  823,470,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  998,405,600.00 |
|               2 |            0.00 |            0.00 |  818,395,400.00 |
|               3 |            0.00 |            0.00 |  823,470,300.00 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      671,328.00 |      672,400.07 |        1,487.21 |
|               2 |      671,328.00 |      820,297.87 |        1,219.07 |
|               3 |      671,328.00 |      815,242.52 |        1,226.63 |


