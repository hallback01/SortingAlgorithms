using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{

    public float radius = 3f;
    public float speed = Mathf.PI;
    public Vector2 origin = Vector2.zero;

    public uint start_ball_amount = 100;
    public GameObject ball_prefab;

    float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(uint i = 0; i < start_ball_amount; i++) {
            GameObject obj = Instantiate(ball_prefab, Vector2.zero, Quaternion.identity);
            obj.GetComponent<BallBehaviour>().visualizer = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime * speed;
        Vector2 position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        transform.position = origin + position;
    }
}
