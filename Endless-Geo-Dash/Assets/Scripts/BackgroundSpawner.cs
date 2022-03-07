/*
NAME: GPEJ
MEMBERS: Eric Chu, Jake Wong
COURSE: CPSC 254-01

FILE DESCRIPTION:
This file contains the BackgroundSpawner class, which will spawn new background objects as the player moves forward
*/
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject background_model;

    [SerializeField] private float background_model_length;

    private bool is_triggered = true;

    private void OnTriggerEnter()
    {
        if (is_triggered)
        {
            Vector3 spawn_position_1 = new Vector3(transform.position.x, 
                transform.position.y, transform.position.z + background_model_length);

            Quaternion orientation = Quaternion.Euler(Vector3.zero);

            Instantiate(background_model, spawn_position_1, orientation);

            // prevents double triggers
            is_triggered = false;   
        }
    }

    private void OnTriggerExit()
    {
        Destroy(transform.parent.gameObject, 5f);    
    }
}