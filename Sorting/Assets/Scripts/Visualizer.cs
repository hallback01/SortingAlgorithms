using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Visualizer : MonoBehaviour
{

    public float radius = 3f;
    public float speed = Mathf.PI;
    public Vector2 origin = Vector2.zero;

    public uint start_ball_amount = 100;

    [Range(1, 100)]
    public uint ball_divider = 10;
    public GameObject ball_prefab;

    float angle = 0;

    List<BallBehaviour> balls;

    TimeMeasurment time_measurement;

    // Start is called before the first frame update
    void Start()
    {
        time_measurement = new TimeMeasurment();

        balls = new List<BallBehaviour>();

        for(uint i = 0; i < start_ball_amount; i++) {
            GameObject obj = Instantiate(ball_prefab, Vector2.zero, Quaternion.identity);
            BallBehaviour ball_behaviour = obj.GetComponent<BallBehaviour>();
            ball_behaviour.visualizer = this;
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
        Debug.Log("Sorting " + start_ball_amount + " balls took on average: " + (1000000 * time_measurement.get_average()).ToString() + "μs");
    }

    void sort_objects() {
        float now = Time.realtimeSinceStartup;
        balls.Sort((b1, b2) => b1.distance_from_circle().CompareTo(b2.distance_from_circle()));
        time_measurement.add_time(Time.realtimeSinceStartup - now);
    }

    void update_balls() {
        for(int i = 0; i < start_ball_amount; i++) {
            balls[i].update_ball();
        }
    }

    void change_colors() {

        for(int i = 0; i < start_ball_amount / ball_divider; i++) {
            balls[i].change_color();
        }

    }
}
