using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TimeMeasurment
{
    SortedDictionary<(StateInformation.SortingImplementation,int), (float, int)> times;

    public TimeMeasurment() {
        times = new SortedDictionary<(StateInformation.SortingImplementation, int), (float, int)>();
    }

    public void insert(StateInformation.SortingImplementation sorter, int sample_size, float time) {
        if(times.ContainsKey((sorter, sample_size))) {
            float new_time = times[(sorter, sample_size)].Item1 + time;
            int new_amount = times[(sorter, sample_size)].Item2 + 1;
            times[(sorter, sample_size)] = (new_time, new_amount);
        } else {
            times.Add((sorter, sample_size), (time, 1));
        }
    }

    public void print() {
        foreach(KeyValuePair<(StateInformation.SortingImplementation, int), (float, int)> entry in times) {
            float time = (entry.Value.Item1 / (float)entry.Value.Item2) * 1000000.0f;
            int balls = entry.Key.Item2;
            StateInformation.SortingImplementation sorter = entry.Key.Item1;
            Debug.Log("Sorting with " + sorter + " with " + balls + " balls took " + time + " micro seconds");
        }
    }

    public void save(string location) {
        string csv_data = "SortingImplementation; Ball Amount; Time(μs)\n";

        foreach(KeyValuePair<(StateInformation.SortingImplementation, int), (float, int)> entry in times) {
            int time = (int)Mathf.Round((entry.Value.Item1 / (float)entry.Value.Item2) * 1000000.0f); //convert from seconds to microseonds because for some reason libreoffice doesn't accept floating points so I need to enlarge the numbers into readable integers..
            int balls = entry.Key.Item2;
            StateInformation.SortingImplementation sorter = entry.Key.Item1;

            csv_data += sorter + ";" + balls + ";" + time + "\n";
        }

        try {
            File.WriteAllText(location, csv_data);
        } catch {
            Debug.Log("Error! Could not write data to '" + location + "'.");
        }
    }
}
