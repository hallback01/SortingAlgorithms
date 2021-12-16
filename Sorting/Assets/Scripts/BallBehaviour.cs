using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{

    public Visualizer visualizer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float random_angle = Random.Range(0.0f, 360.0f);
        float random_range = Random.Range(0.0f, visualizer.radius);

        transform.position = new Vector2(Mathf.Cos(random_angle), Mathf.Sin(random_angle)) * random_range;

    }
}
