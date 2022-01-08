# SortingAlgorithms
Sorting algorithm experimentation in unity.


This is an assignment for school, where I try and compare the speed of different sorting algorithms based on the size of a list of balls where the sorting comparison is made based on the distance of a known point(in this case, this point is moving in a circular motion around all the balls. Which means the balls are all contained inside a circle).
There are two parts, a simulation part and an experimentation part. In the simulation part, a sorting algorithm can be chosen with a chosen amount of balls.
The experiment part goes through every sorting algorithm while saving the average time it took to sort the list(Because a single iteration is ran over multiple ticks, the time collected is always the average time per tick). It starts sorting a list with a start ball amount, then every iteration it increases the ball amount depending on a delta variable. After the amount of balls is equal to the chosen end ball amount. Start iterating over again, but with the next sorting algorithm until there are no algorithm left. Then, it dumps the data collected into a .csv file.


Example output (Start ball amount is 1000, and the end ball amount is 2000. The delta value is 100 with every iteration spanning across 1024 ticks):
```
SortingImplementation; Ball Amount; Time(μs)
SelectionSort;1000;3777
SelectionSort;1100;4564
SelectionSort;1200;5574
SelectionSort;1300;6363
SelectionSort;1400;7406
SelectionSort;1500;8891
SelectionSort;1600;9788
SelectionSort;1700;10971
SelectionSort;1800;12156
SelectionSort;1900;13746
SelectionSort;2000;15682
MapSort;1000;2021
MapSort;1100;2234
MapSort;1200;2446
MapSort;1300;2683
MapSort;1400;2896
MapSort;1500;3112
MapSort;1600;3322
MapSort;1700;3556
MapSort;1800;3774
MapSort;1900;4022
MapSort;2000;4237
MapSortLinq;1000;715
MapSortLinq;1100;800
MapSortLinq;1200;880
MapSortLinq;1300;959
MapSortLinq;1400;1015
MapSortLinq;1500;1089
MapSortLinq;1600;1185
MapSortLinq;1700;1242
MapSortLinq;1800;1323
MapSortLinq;1900;1386
MapSortLinq;2000;1482
HeapSort;1000;573
HeapSort;1100;636
HeapSort;1200;705
HeapSort;1300;782
HeapSort;1400;841
HeapSort;1500;917
HeapSort;1600;988
HeapSort;1700;1058
HeapSort;1800;1137
HeapSort;1900;1207
HeapSort;2000;1271
QuickSort;1000;378
QuickSort;1100;421
QuickSort;1200;470
QuickSort;1300;510
QuickSort;1400;555
QuickSort;1500;602
QuickSort;1600;650
QuickSort;1700;700
QuickSort;1800;744
QuickSort;1900;790
QuickSort;2000;834
QuickSortMedianPivot;1000;335
QuickSortMedianPivot;1100;368
QuickSortMedianPivot;1200;409
QuickSortMedianPivot;1300;450
QuickSortMedianPivot;1400;493
QuickSortMedianPivot;1500;531
QuickSortMedianPivot;1600;573
QuickSortMedianPivot;1700;614
QuickSortMedianPivot;1800;662
QuickSortMedianPivot;1900;696
QuickSortMedianPivot;2000;746
```
