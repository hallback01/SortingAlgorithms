using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class StateExperiment
{
    public StateInformation state_information;
    public int ticks_per_samplesize = 60;
    public int starting_sample_size = 100;
    public int sample_size_delta = 50;
    public int stop_sample_size = 200;
    public string csv_save_location = "/home/rasmus/Desktop/experiment-sorting.csv";
    float angle = 0;
    int ticks = 0;
    int current_sample_size = 0;

    bool done = false;
    TimeMeasurment time_measurment;

    StateInformation.SortingImplementation sorting_implementation;

    public void set_info(StateInformation state_info) {
        state_information = state_info;
    }

    public void start() {
        time_measurment = new TimeMeasurment();

        sorting_implementation = StateInformation.SortingImplementation.SelectionSort;
        current_sample_size = starting_sample_size;
        Debug.Log("Started selection sort experiment.");
        spawn_balls();
    }
    public void update() {

        if(!done) {
            ticks++;
            angle += Mathf.PI * 2 / ticks_per_samplesize;
            Vector2 position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Mathf.PI;
            state_information.sorting_controller.transform.position = Vector2.zero + position;

            if(ticks >= ticks_per_samplesize) {
                ticks = 0;

                if(current_sample_size >= stop_sample_size) {

                    switch(sorting_implementation) {
                        case StateInformation.SortingImplementation.SelectionSort: {
                            sorting_implementation = StateInformation.SortingImplementation.MapSort;
                            current_sample_size = starting_sample_size;
                            Debug.Log("Started map sort experiment.");
                            spawn_balls();
                            break;
                        }

                        case StateInformation.SortingImplementation.MapSort: {
                            sorting_implementation = StateInformation.SortingImplementation.MapSortLinq;
                            current_sample_size = starting_sample_size;
                            Debug.Log("Started map sort with System.Linq experiment.");
                            spawn_balls();
                            break;
                        }

                        case StateInformation.SortingImplementation.MapSortLinq: {
                            sorting_implementation = StateInformation.SortingImplementation.HeapSort;
                            current_sample_size = starting_sample_size;
                            Debug.Log("Started heap sort experiment.");
                            spawn_balls();
                            break;
                        }

                        case StateInformation.SortingImplementation.HeapSort: {
                            sorting_implementation = StateInformation.SortingImplementation.QuickSort;
                            current_sample_size = starting_sample_size;
                            Debug.Log("Started quick sort experiment.");
                            spawn_balls();
                            break;
                        }
                        case StateInformation.SortingImplementation.QuickSort: {
                            done = true;
                            Debug.Log("Done! Saving results to file.");
                            time_measurment.save(csv_save_location);
                            break;
                        }
                    }
                } else {
                    current_sample_size += sample_size_delta;
                    spawn_balls();
                }

            } else {
                update_balls();
                float now = Time.realtimeSinceStartup;
                sort();
                time_measurment.insert(sorting_implementation, current_sample_size, Time.realtimeSinceStartup - now);
                change_colors();
            }
        }
    }

    void sort() {
        switch(sorting_implementation) {
            case StateInformation.SortingImplementation.SelectionSort: {
                state_information.sort_selection_sort();
                break;
            }
            case StateInformation.SortingImplementation.MapSort: {
                state_information.sort_map_sort();
                break;
            }
            case StateInformation.SortingImplementation.MapSortLinq: {
                state_information.sort_map_sort_linq();
                break;
            }
            case StateInformation.SortingImplementation.HeapSort: {
                state_information.sort_heap_sort();
                break;
            }
            case StateInformation.SortingImplementation.QuickSort: {
                state_information.sort_quick_sort();
                break;
            }
        }
    }

    void spawn_balls() {
        for(int i = 0; i < state_information.balls.Count; i++) {
            GameObject.DestroyImmediate(state_information.balls[i].gameObject);
        }

        state_information.balls.Clear();

        for(int i = 0; i < current_sample_size; i++) {
            GameObject obj = GameObject.Instantiate(state_information.ball_prefab, Vector2.zero, Quaternion.identity);
            BallBehaviour ball_behaviour = obj.GetComponent<BallBehaviour>();
            ball_behaviour.sorting_controller = state_information.sorting_controller;
            state_information.balls.Add(ball_behaviour);
        }
    }

    void update_balls() {
        for(int i = 0; i < state_information.balls.Count; i++) {
            state_information.balls[i].update_ball(Vector2.zero, Mathf.PI);
        }
    }

    void change_colors() {
        for(int i = 0; i < state_information.balls.Count * 0.1; i++) {
            state_information.balls[i].change_color();
        }
    }
}
