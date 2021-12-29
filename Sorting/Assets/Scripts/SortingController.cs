using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

public class SortingController : MonoBehaviour
{
    public enum Mode {
        Simulation,
        Experiment
    }
    public Mode mode;
    TimeMeasurment time_measurement;
    public GameObject ball_prefab;
    public StateInformation state_information;
    public StateSimulation state_simulation;
    public StateExperiment state_experiment;

    void OnEnable() {
        state_information = new StateInformation(ball_prefab, this);
        state_experiment.set_info(state_information);
        state_simulation.set_info(state_information);
    }

    // Start is called before the first frame update
    void Start()
    {
        time_measurement = new TimeMeasurment();

        if(mode == Mode.Simulation) {
            state_simulation.start();
        } else {
            state_experiment.start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mode == Mode.Simulation) {
            state_simulation.update();
        } else {
            state_experiment.update();
        }
        
    }

    void OnDestroy() {
        //string text = "Sorting " + start_ball_amount + " balls took on average: " + (1000000 * time_measurement.get_average()).ToString() + "μs";

        //Debug.Log(text);
        //File.WriteAllText("/home/rasmus/Desktop/Time.txt", text + "\n");
    }
}
