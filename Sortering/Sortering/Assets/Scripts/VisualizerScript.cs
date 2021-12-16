using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerScript : MonoBehaviour
{


    public float circle_radius = 2f;
    public float speed = 1f;

    float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        angle += Time.deltaTime * speed;
        Vector2 origin = new Vector2(0f, 0f);
        Vector2 position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * circle_radius;

        transform.position = origin + position;

    }
}
