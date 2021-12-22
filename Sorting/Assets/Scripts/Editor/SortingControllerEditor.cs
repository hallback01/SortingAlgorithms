using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SortingController))]
public class SortingControllerEditor : Editor {
    

    SortingController sorting_controller;

    void OnEnable() {
        sorting_controller = (SortingController)target;
    }

    public override void OnInspectorGUI() {

        sorting_controller.ball_prefab = EditorGUILayout.ObjectField("Ball Prefab", sorting_controller.ball_prefab, typeof(GameObject), false) as GameObject;
        sorting_controller.mode = (SortingController.Mode)EditorGUILayout.EnumPopup("Controller Mode", sorting_controller.mode);        

        if(sorting_controller.mode == SortingController.Mode.Simulation) {

            EditorGUI.BeginChangeCheck();

            SortingController.SortingImplementation sorting_implementation = (SortingController.SortingImplementation)EditorGUILayout.EnumPopup("Sorting Implementation", sorting_controller.sorting_implementation);
            float radius = EditorGUILayout.FloatField("Radius", sorting_controller.radius);
            float speed = EditorGUILayout.FloatField("Speed", sorting_controller.speed);
            int start_ball_amount = EditorGUILayout.IntField("Amount of Balls at Start", sorting_controller.start_ball_amount);
            Vector2 origin = EditorGUILayout.Vector2Field("Origin", sorting_controller.origin);
            float ball_divider = EditorGUILayout.Slider("Yellow Ball Percentage", sorting_controller.ball_divider, 0.01f, 1f);
            
            if(EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(target, "Changed SortingController");
                sorting_controller.sorting_implementation = sorting_implementation;
                sorting_controller.radius = radius;
                sorting_controller.speed = speed;
                sorting_controller.start_ball_amount = start_ball_amount;
                sorting_controller.origin = origin;
                sorting_controller.ball_divider = ball_divider;
            }
            
        }

    }

}
