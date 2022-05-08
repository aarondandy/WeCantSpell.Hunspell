# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_05/08/2022 17:50:49_
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
|    Elapsed Time |              ms |        1,024.00 |        1,011.67 |          998.00 |           13.05 |
|[Counter] _wordsChecked |      operations |      671,328.00 |      671,328.00 |      671,328.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,655,595.77 |    5,580,069.15 |    5,512,249.91 |       71,983.06 |
|TotalCollections [Gen0] |     collections |            9.02 |            8.90 |            8.79 |            0.11 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.96 |          999.91 |          999.86 |            0.05 |
|[Counter] _wordsChecked |      operations |      672,581.42 |      663,599.56 |      655,534.28 |        8,560.45 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,645,056.00 |    5,572,361.75 |          179.46 |
|               2 |    5,645,056.00 |    5,512,249.91 |          181.41 |
|               3 |    5,645,056.00 |    5,655,595.77 |          176.82 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            9.00 |            8.88 |  112,560,611.11 |
|               2 |            9.00 |            8.79 |  113,788,100.00 |
|               3 |            9.00 |            9.02 |  110,904,044.44 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,013,045,500.00 |
|               2 |            0.00 |            0.00 |1,024,092,900.00 |
|               3 |            0.00 |            0.00 |  998,136,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,013,045,500.00 |
|               2 |            0.00 |            0.00 |1,024,092,900.00 |
|               3 |            0.00 |            0.00 |  998,136,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,013.00 |          999.96 |    1,000,044.92 |
|               2 |        1,024.00 |          999.91 |    1,000,090.72 |
|               3 |          998.00 |          999.86 |    1,000,136.67 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      671,328.00 |      662,682.97 |        1,509.02 |
|               2 |      671,328.00 |      655,534.28 |        1,525.47 |
|               3 |      671,328.00 |      672,581.42 |        1,486.81 |


