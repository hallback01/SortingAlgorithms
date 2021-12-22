using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{

    public SortingController sorting_controller;

    SpriteRenderer sprite_renderer;
    Color start_color;

    float distance = 0;

    void Start() {
        sprite_renderer = GetComponent<SpriteRenderer>();
        start_color = sprite_renderer.color;
    }

    // Update is called once per frame
    public void update_ball()
    {
        float random_angle = Random.Range(0.0f, 360.0f);
        float random_range = Random.Range(0.0f, sorting_controller.radius);
        transform.position = sorting_controller.origin + new Vector2(Mathf.Cos(random_angle), Mathf.Sin(random_angle)) * random_range;

        distance = Vector2.Distance(sorting_controller.transform.position, transform.position);

        change_back_color();
    }

    public void change_color() {
        sprite_renderer.color = Color.yellow;
    }

    public void change_back_color() {
        sprite_renderer.color = start_color;
    }

    public float distance_from_circle() {
        return distance;
    }
}
