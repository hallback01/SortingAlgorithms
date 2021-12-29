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
    public int stop_sample_size = 5000;
    float angle = 0;

    public void set_info(StateInformation state_info) {
        state_information = state_info;
    }

    public void start() {

    }
    public void update() {
        angle += Mathf.PI * 2 / ticks_per_samplesize;
        Vector2 position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Mathf.PI;
        state_information.sorting_controller.transform.position = Vector2.zero + position;

        update_balls();
    }

    void update_balls() {
        for(int i = 0; i < state_information.balls.Count; i++) {
            state_information.balls[i].update_ball(Vector2.zero, Mathf.PI);
        }
    }
}
