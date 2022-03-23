# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/23/2022 00:48:00_
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
NumberOfIterations=2, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |  123,561,968.00 |  123,554,488.00 |  123,547,008.00 |       10,578.32 |
|TotalCollections [Gen0] |     collections |          484.00 |          484.00 |          484.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          185.00 |          185.00 |          185.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           44.00 |           44.00 |           44.00 |            0.00 |
|    Elapsed Time |              ms |       18,353.00 |       18,343.50 |       18,334.00 |           13.44 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,739,763.57 |    6,735,698.98 |    6,731,634.39 |        5,748.20 |
|TotalCollections [Gen0] |     collections |           26.40 |           26.39 |           26.37 |            0.02 |
|TotalCollections [Gen1] |     collections |           10.09 |           10.09 |           10.08 |            0.01 |
|TotalCollections [Gen2] |     collections |            2.40 |            2.40 |            2.40 |            0.00 |
|    Elapsed Time |              ms |        1,000.04 |        1,000.01 |          999.99 |            0.04 |
|[Counter] FilePairsLoaded |      operations |            3.22 |            3.22 |            3.21 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  123,547,008.00 |    6,731,634.39 |          148.55 |
|               2 |  123,561,968.00 |    6,739,763.57 |          148.37 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          484.00 |           26.37 |   37,919,827.27 |
|               2 |          484.00 |           26.40 |   37,878,676.24 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          185.00 |           10.08 |   99,206,467.03 |
|               2 |          185.00 |           10.09 |   99,098,807.03 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           44.00 |            2.40 |  417,118,100.00 |
|               2 |           44.00 |            2.40 |  416,665,438.64 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,353.00 |          999.99 |    1,000,010.70 |
|               2 |       18,334.00 |        1,000.04 |      999,960.69 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.21 |  311,071,125.42 |
|               2 |           59.00 |            3.22 |  310,733,547.46 |


