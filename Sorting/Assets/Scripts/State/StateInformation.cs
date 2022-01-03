using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StateInformation
{
    public enum SortingImplementation {
        SelectionSort,
        MapSort,
        HeapSort,
        QuickSort
    }
    public List<BallBehaviour> balls;
    public GameObject ball_prefab;
    public SortingController sorting_controller;

    public StateInformation(GameObject ball_prfb, SortingController srt_cntrl) {
        balls = new List<BallBehaviour>();
        ball_prefab = ball_prfb;
        sorting_controller = srt_cntrl;
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
                BallBehaviour a = balls[i];
                BallBehaviour b = balls[lowest];
                balls[lowest] = a;
                balls[i] = b;
            }
        }
    }

    public void sort_heap_sort() {
        for(int i = balls.Count/2 - 1; i >= 0; i--) {
            heapify(balls.Count, i);
        }
        for(int i = balls.Count - 1; i >= 0; i--) {
            BallBehaviour a = balls[i];
            BallBehaviour b = balls[0];
            balls[0] = a;
            balls[i] = b;
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
            BallBehaviour a = balls[i];
            BallBehaviour b = balls[largest];
            balls[i] = b;
            balls[largest] = a;
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

    public void sort_quick_sort() {

    }
}
