using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;

    private Vector3 offset; //for 3rd person view

    private Vector3 moveCam;    //total position container



    private float transition = 0.0f;    //the time record in seconds from which animation runs

    readonly private float animationTime = 3.0f;    //the time record in seconds upto which animation runs

    private Vector3 animationOffset = new Vector3(0, 5, 5);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovementVariable();

    }


    private void MovementVariable()
    {

        if (transition > 1)
        {
            moveCam.x = 0.0f;
            transform.position = moveCam;    //normal movement of camera
        }
        else
        {
            //Animation of camera

            transform.position = Vector3.Lerp(moveCam + animationOffset, moveCam, transition);
            transition += Time.deltaTime * 1 / animationTime;
            transform.LookAt(player);
        }
        moveCam = player.position - offset;

        // X
        moveCam.x = 0.0f;

        //Y
        moveCam.y = Mathf.Clamp(moveCam.y, 3, 5);
    }

}
