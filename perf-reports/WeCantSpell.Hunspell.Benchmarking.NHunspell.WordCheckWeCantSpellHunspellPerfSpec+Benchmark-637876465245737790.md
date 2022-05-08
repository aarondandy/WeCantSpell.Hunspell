# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_05/08/2022 22:42:04_
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
|TotalBytesAllocated |           bytes |    5,645,056.00 |    5,645,056.00 |    5,645,056.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            9.00 |            9.00 |            9.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] WordsChecked |      operations |      671,328.00 |      671,328.00 |      671,328.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,638,309.76 |    5,623,722.05 |    5,600,786.27 |       20,106.49 |
|TotalCollections [Gen0] |     collections |            8.99 |            8.97 |            8.93 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] WordsChecked |      operations |      670,525.72 |      668,790.90 |      666,063.30 |        2,391.13 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,645,056.00 |    5,632,070.14 |          177.55 |
|               2 |    5,645,056.00 |    5,600,786.27 |          178.55 |
|               3 |    5,645,056.00 |    5,638,309.76 |          177.36 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            9.00 |            8.98 |  111,367,300.00 |
|               2 |            9.00 |            8.93 |  111,989,355.56 |
|               3 |            9.00 |            8.99 |  111,244,055.56 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,002,305,700.00 |
|               2 |            0.00 |            0.00 |1,007,904,200.00 |
|               3 |            0.00 |            0.00 |1,001,196,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,002,305,700.00 |
|               2 |            0.00 |            0.00 |1,007,904,200.00 |
|               3 |            0.00 |            0.00 |1,001,196,500.00 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      671,328.00 |      669,783.68 |        1,493.02 |
|               2 |      671,328.00 |      666,063.30 |        1,501.36 |
|               3 |      671,328.00 |      670,525.72 |        1,491.37 |


