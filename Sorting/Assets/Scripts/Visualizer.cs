using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{

    public float radius = 3f;
    public float speed = Mathf.PI;
    public Vector2 origin = Vector2.zero;

    float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime * speed;

        Vector2 position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        transform.position = origin + position;
    }
}
