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
    public enum SortingImplementation {
        BuiltIn,
        CountingSort
    }

    public Mode mode;

    public float radius = 3f;
    public float speed = Mathf.PI;
    public Vector2 origin = Vector2.zero;

    public SortingImplementation sorting_implementation;

    public int start_ball_amount = 100;

    public float ball_divider = 0.1f;
    public GameObject ball_prefab;

    float angle = 0;

    List<BallBehaviour> balls;

    TimeMeasurment time_measurement;

    // Start is called before the first frame update
    void Start()
    {
        time_measurement = new TimeMeasurment();

        balls = new List<BallBehaviour>();

        for(int i = 0; i < start_ball_amount; i++) {
            GameObject obj = Instantiate(ball_prefab, Vector2.zero, Quaternion.identity);
            BallBehaviour ball_behaviour = obj.GetComponent<BallBehaviour>();
            ball_behaviour.sorting_controller = this;
            balls.Add(ball_behaviour);
        }
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime * speed;
        Vector2 position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        transform.position = origin + position;

        update_balls();
        sort_objects();
        change_colors();
    }

    void OnDestroy() {
        string text = "Sorting " + start_ball_amount + " balls took on average: " + (1000000 * time_measurement.get_average()).ToString() + "μs";

        Debug.Log(text);
        File.WriteAllText("/home/rasmus/Desktop/Time.txt", text + "\n");
    }

    void sort_objects() {
        float now = Time.realtimeSinceStartup;

        switch(sorting_implementation) {

            case SortingImplementation.BuiltIn: {
                sort_builtin();
                break;
            }

            case SortingImplementation.CountingSort: {
                sort_counting_sort();
                break;
            }

        }
        
        time_measurement.add_time(Time.realtimeSinceStartup - now);
    }

    void sort_builtin() {
        balls.Sort((b1, b2) => b1.distance_from_circle().CompareTo(b2.distance_from_circle()));
    }

    void sort_counting_sort() {
        sort_builtin();
    }

    void update_balls() {
        for(int i = 0; i < start_ball_amount; i++) {
            balls[i].update_ball();
        }
    }

    void change_colors() {

        for(int i = 0; i < start_ball_amount * ball_divider; i++) {
            balls[i].change_color();
        }

    }
}
