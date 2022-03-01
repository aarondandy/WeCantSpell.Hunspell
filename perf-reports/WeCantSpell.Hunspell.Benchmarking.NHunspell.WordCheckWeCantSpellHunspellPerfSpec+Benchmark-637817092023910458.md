# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/01/2022 05:26:42_
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
|TotalBytesAllocated |           bytes |    5,203,936.00 |    5,203,936.00 |    5,203,936.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           77.00 |           77.00 |           77.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,022.00 |        1,020.00 |        1,019.00 |            1.73 |
|[Counter] _wordsChecked |      operations |      671,328.00 |      671,328.00 |      671,328.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,103,646.79 |    5,099,139.45 |    5,091,748.49 |        6,452.05 |
|TotalCollections [Gen0] |     collections |           75.52 |           75.45 |           75.34 |            0.10 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.97 |          999.46 |          999.04 |            0.47 |
|[Counter] _wordsChecked |      operations |      658,390.30 |      657,808.84 |      656,855.37 |          832.34 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,203,936.00 |    5,103,646.79 |          195.94 |
|               2 |    5,203,936.00 |    5,091,748.49 |          196.40 |
|               3 |    5,203,936.00 |    5,102,023.09 |          196.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           77.00 |           75.52 |   13,242,214.29 |
|               2 |           77.00 |           75.34 |   13,273,158.44 |
|               3 |           77.00 |           75.49 |   13,246,428.57 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,019,650,500.00 |
|               2 |            0.00 |            0.00 |1,022,033,200.00 |
|               3 |            0.00 |            0.00 |1,019,975,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,019,650,500.00 |
|               2 |            0.00 |            0.00 |1,022,033,200.00 |
|               3 |            0.00 |            0.00 |1,019,975,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,019.00 |          999.36 |    1,000,638.37 |
|               2 |        1,022.00 |          999.97 |    1,000,032.49 |
|               3 |        1,019.00 |          999.04 |    1,000,956.82 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      671,328.00 |      658,390.30 |        1,518.86 |
|               2 |      671,328.00 |      656,855.37 |        1,522.41 |
|               3 |      671,328.00 |      658,180.84 |        1,519.34 |


