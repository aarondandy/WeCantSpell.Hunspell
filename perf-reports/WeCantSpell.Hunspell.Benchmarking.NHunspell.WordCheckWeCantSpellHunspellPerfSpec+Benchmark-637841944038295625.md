# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/29/2022 23:46:43_
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
|TotalBytesAllocated |           bytes |    5,734,880.00 |    5,734,880.00 |    5,734,880.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           14.00 |           14.00 |           14.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          991.00 |          988.33 |          984.00 |            3.79 |
|[Counter] _wordsChecked |      operations |      621,600.00 |      621,600.00 |      621,600.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,826,267.93 |    5,801,280.98 |    5,788,716.80 |       21,639.45 |
|TotalCollections [Gen0] |     collections |           14.22 |           14.16 |           14.13 |            0.05 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.33 |          999.77 |          999.29 |            0.52 |
|[Counter] _wordsChecked |      operations |      631,505.48 |      628,797.16 |      627,435.34 |        2,345.49 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,734,880.00 |    5,826,267.93 |          171.64 |
|               2 |    5,734,880.00 |    5,788,716.80 |          172.75 |
|               3 |    5,734,880.00 |    5,788,858.21 |          172.75 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           14.00 |           14.22 |   70,308,178.57 |
|               2 |           14.00 |           14.13 |   70,764,264.29 |
|               3 |           14.00 |           14.13 |   70,762,535.71 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  984,314,500.00 |
|               2 |            0.00 |            0.00 |  990,699,700.00 |
|               3 |            0.00 |            0.00 |  990,675,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  984,314,500.00 |
|               2 |            0.00 |            0.00 |  990,699,700.00 |
|               3 |            0.00 |            0.00 |  990,675,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          984.00 |          999.68 |    1,000,319.61 |
|               2 |          990.00 |          999.29 |    1,000,706.77 |
|               3 |          991.00 |        1,000.33 |      999,672.55 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      621,600.00 |      631,505.48 |        1,583.52 |
|               2 |      621,600.00 |      627,435.34 |        1,593.79 |
|               3 |      621,600.00 |      627,450.66 |        1,593.75 |


