using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMeasurment
{

    int amount = 0;
    double total_time = 0.0;

    public void add_time(double time) {
        total_time += time;
        amount++;
    }

    public double get_average() {
        if(amount > 0) {
            return total_time / amount;
        } else {
            return 0;
        }
    }

}
