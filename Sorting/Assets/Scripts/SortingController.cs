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
        SelectionSort,
        HeapSort,
        QuickSort
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
            case SortingImplementation.SelectionSort: {
                sort_selection_sort();
                break;
            }
            case SortingImplementation.HeapSort: {
                sort_heap_sort();
                break;
            }
            case SortingImplementation.QuickSort: {
                sort_quick_sort();
                break;
            }
        }
        
        time_measurement.add_time(Time.realtimeSinceStartup - now);
    }

    void sort_builtin() {
        balls.Sort((b1, b2) => b1.distance_from_circle().CompareTo(b2.distance_from_circle()));
    }

    void sort_selection_sort() {
        for(int i = 0; i < balls.Count-1; i++) {
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

    void sort_heap_sort() {
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

    void sort_quick_sort() {

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
