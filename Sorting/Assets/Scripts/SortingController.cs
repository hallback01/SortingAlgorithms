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
    [Tooltip("This can't be changed after startup.")]
    public Mode mode;
    Mode current_mode;
    public GameObject ball_prefab;
    public StateInformation state_information;
    public StateSimulation state_simulation;
    public StateExperiment state_experiment;
    bool update_balls = false;

    void OnEnable() {
        state_information = new StateInformation(ball_prefab, this);
        state_experiment.set_info(state_information);
        state_simulation.set_info(state_information);
    }

    // Start is called before the first frame update
    void Start()
    {
        //we shouldn't change mode after startup.
        current_mode = mode;

        if(current_mode == Mode.Simulation) {
            Debug.Log("Started Simulation!");
            state_simulation.start();
        } else {
            Debug.Log("Started Experiment!");
            state_experiment.start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(current_mode == Mode.Simulation) {
            state_simulation.update();
        } else {
            state_experiment.update();
        }

        if(update_balls) {
            state_simulation.create_balls_simulation();
            update_balls = false;
        }
    }

    public Mode get_sorting_mode() {
        return current_mode;
    }

    public void OnValidate() {
        if(Application.isPlaying) {
            if(current_mode == Mode.Simulation) {
                if(state_information != null && state_simulation != null) {
                    if(state_information.balls.Count != state_simulation.ball_amount) {
                        update_balls = true;
                    }
                }
            }
        }
    }

    public string get_current_sorting_algorithm() {
        switch(current_mode) {
            case Mode.Simulation: {
                return state_simulation.get_sorting_algorithm().ToString();
            }
            case Mode.Experiment: {
                return state_experiment.get_sorting_algorithm().ToString();
            }
            default: {
                return "Unknown";
            }
        }
    }

    public int get_ball_amount() {
        return state_information.balls.Count;
    }

    public float get_current_time() {
        switch(current_mode) {
            case Mode.Simulation: {
                return state_simulation.get_sorting_time();
            }
            case Mode.Experiment: {
                return state_experiment.get_sorting_time();
            }
            default: {
                return 0f;
            }
        }
    }

}
