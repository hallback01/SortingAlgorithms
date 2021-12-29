using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class StateSimulation
{
    public StateInformation state_information;
    
    public float radius;
    public float speed;
    public Vector2 origin;
    public StateInformation.SortingImplementation sorting_implementation;
    public int start_ball_amount;
    [Range(0.001f, 1f)]
    public float ball_divider;
    float angle = 0;

    public void set_info(StateInformation state_info) {
        state_information = state_info;
    }

    public void start() {
        create_balls_simulation();
    }
    public void update() {

        angle += Time.deltaTime * speed;
        Vector2 position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        state_information.sorting_controller.transform.position = origin + position;

        update_balls();
        sort_objects();
        change_colors();

    }

    void sort_objects() {
        switch(sorting_implementation) {
            case StateInformation.SortingImplementation.SelectionSort: {
                state_information.sort_selection_sort();
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

    void create_balls_simulation() {
        state_information.balls.Clear();
        for(int i = 0; i < start_ball_amount; i++) {
            GameObject obj = GameObject.Instantiate(state_information.ball_prefab, Vector2.zero, Quaternion.identity);
            BallBehaviour ball_behaviour = obj.GetComponent<BallBehaviour>();
            ball_behaviour.sorting_controller = state_information.sorting_controller;
            state_information.balls.Add(ball_behaviour);
        }
    }

    void change_colors() {
        for(int i = 0; i < state_information.balls.Count * ball_divider; i++) {
            state_information.balls[i].change_color();
        }
    }

    void update_balls() {
        for(int i = 0; i < state_information.balls.Count; i++) {
            state_information.balls[i].update_ball(origin, radius);
        }
    }
}
