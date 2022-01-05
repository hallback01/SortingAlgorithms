using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Information : MonoBehaviour
{

    public Text time_text;
    public SortingController sorting_controller;
    float time_average_per_second = 0f;
    float time = 0;
    float timer = 0;
    int amount = 0;

    // Update is called once per frame
    void Update()
    {

        //calculate average time
        time += sorting_controller.get_current_time();
        amount++;
        timer -= Time.deltaTime;

        if(timer <= 0f) {
            time_average_per_second = time / amount;
            time = 0f;
            amount = 0;
            timer = 1f;
        }

        string information = "-- Sorting information --\n";
        information += "Sorting Mode: " + sorting_controller.get_sorting_mode() + "\n";
        information += "Current Tick Time: " + sorting_controller.get_current_time() + "ms\n";
        information += "Average Tick Time: " + time_average_per_second + "ms\n";
        information += "Current Ball Amount: " + sorting_controller.get_ball_amount() + "\n";
        information += "Current sorting algorithm: " + sorting_controller.get_current_sorting_algorithm() + "\n";

        time_text.text = information;
    }
}
