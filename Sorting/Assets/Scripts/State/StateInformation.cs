using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StateInformation
{
    public enum SortingImplementation {
        SelectionSort,
        MapSort,
        MapSortLinq,
        HeapSort,
        QuickSort,
        QuickSortMedianPivot
    }
    public List<BallBehaviour> balls;
    public GameObject ball_prefab;
    public SortingController sorting_controller;

    public StateInformation(GameObject ball_prfb, SortingController srt_cntrl) {
        balls = new List<BallBehaviour>();
        ball_prefab = ball_prfb;
        sorting_controller = srt_cntrl;
    }

    void swap_by_index(int a, int b) {
        BallBehaviour ball_a = balls[a];
        BallBehaviour ball_b = balls[b];
        balls[a] = ball_b;
        balls[b] = ball_a;
    }

    public void sort_selection_sort() {
        for(int i = 0; i < balls.Count; i++) {
            int lowest = i;
            for(int j = i+1; j < balls.Count; j++) {
                if(balls[lowest].distance_from_circle() > balls[j].distance_from_circle()) {
                    lowest = j;
                }
            }
            if(lowest != i) {
                swap_by_index(lowest, i);
            }
        }
    }

    public void sort_heap_sort() {
        for(int i = balls.Count/2 - 1; i >= 0; i--) {
            heapify(balls.Count, i);
        }
        for(int i = balls.Count - 1; i >= 0; i--) {
            swap_by_index(i, 0);
            heapify(i, 0);
        }
    }

    void heapify(int n, int i) {
        int largest = i;
        int left = 2*i + 1;
        int right = 2*i + 2;
        if(left < n && balls[left].distance_from_circle() > balls[largest].distance_from_circle()) {
            largest = left;
        }
        if(right < n && balls[right].distance_from_circle() > balls[largest].distance_from_circle()) {
            largest = right;
        }
        if(largest != i) {
            swap_by_index(largest, i);
            heapify(n, largest);
        }
    }

    public void sort_map_sort() {
        SortedDictionary<(float, int), BallBehaviour> remaining = new SortedDictionary<(float, int), BallBehaviour>();
        int index = 0;
        foreach(BallBehaviour value in balls) {
            remaining.Add((value.distance_from_circle(), index), value);
            index++;
        }

        for(int i = 0; i < balls.Count; i++) {
            var first = remaining.First();
            balls[i] = first.Value;
            remaining.Remove(first.Key);
        }
    }

    public void sort_map_sort_linq() {
        SortedDictionary<(float, int), BallBehaviour> remaining = new SortedDictionary<(float, int), BallBehaviour>();
        int index = 0;
        foreach(BallBehaviour value in balls) {
            remaining.Add((value.distance_from_circle(), index), value);
            index++;
        }

        balls = remaining.Select(pair => pair.Value).ToList();
    }

    public void sort_quick_sort() {
        quick_sort(0, balls.Count-1);
    }

    public void sort_quick_sort_median_pivot() {
        quick_sort_median(0, balls.Count-1);
    }

    void quick_sort(int start, int end) {
        if(end > start) {
            int divide_index = quick_sort_divide(start, end);
            quick_sort(start, divide_index-1);
            quick_sort(divide_index+1, end);
        }
    }

    int quick_sort_divide(int start, int end) {

        int random = Random.Range(start, end+1);

        int pivot_index = random;
        float pivot = balls[pivot_index].distance_from_circle();

        swap_by_index(pivot_index, end);

        int i = start;

        for(int j = start; j < end; j++) {
            if(balls[j].distance_from_circle() < pivot) {
                swap_by_index(i ,j);
                i++;
            }
        }

        swap_by_index(i, end);
        return i;
    }

    void quick_sort_median(int start, int end) {
        if(end > start) {
            int divide_index = quick_sort_divide_median(start, end);
            quick_sort_median(start, divide_index-1);
            quick_sort_median(divide_index+1, end);
        }
    }

    int quick_sort_divide_median(int start, int end) {

        int random = choose_pivot(start, end);

        int pivot_index = random;
        float pivot = balls[pivot_index].distance_from_circle();

        swap_by_index(pivot_index, end);

        int i = start;

        for(int j = start; j < end; j++) {
            if(balls[j].distance_from_circle() < pivot) {
                swap_by_index(i ,j);
                i++;
            }
        }

        swap_by_index(i, end);
        return i;
    }

    int choose_pivot(int start, int end) {

        int a = start;
        int b = end;
        int c = (start + end) / 2;

        float av = balls[a].distance_from_circle();
        float bv = balls[b].distance_from_circle();
        float cv = balls[c].distance_from_circle();

        if(av > bv) {

            if(av < cv) {
                return a;
            } else if(bv > cv) {
                return b;
            } else {
                return c;
            }
        } else {
            if(av > cv) {
                return a;
            } else if(bv < cv) {
                return b;
            } else {
                return c;
            }
        }
    }
}
