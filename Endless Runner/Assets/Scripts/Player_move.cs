using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_move : MonoBehaviour
{
     CharacterController controller;
    [SerializeField] private float speed = 5.0f;
    private Vector3 movePlayer;
    readonly private float gravity = 9.8f;
    private float velocity = 0.0f;
    public bool isDead = false;
    private float startTime = 0.0f;

    new public AudioSource audio;

    readonly private float animationTime = 3.0f;    //sync with camera animation to restrict movement at that time

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        Movement();
    }

    private void Movement()
    {
        if (Time.time - startTime < animationTime)
        {
            controller.Move((Vector3.forward * speed) * Time.deltaTime);

            Gravitation();      //gravity remains constant
            return;
        }
        movePlayer = Vector3.zero;

        //Left-right movement by X-axis
        movePlayer.x = Input.GetAxisRaw("Horizontal");
        if (Input.GetMouseButton(0))
        {
            if(Input.mousePosition.x > (Screen.width/2))
            {
                movePlayer.x = 1;
            }
            else
            {
                movePlayer.x = -1;
            }
        }

        //Up-down movement by Y-axis
        Gravitation();

        //Foreward-backward movement by Z-axis
        movePlayer.z = 1.0f;

        controller.Move((movePlayer * speed) * Time.deltaTime);
    }

    private void Gravitation()  //for implementing gravitational force
    {
        if (controller.isGrounded)
        {
            velocity = -0.5f;
        }
        else
        {
            velocity -= gravity * Time.deltaTime;
        }
        movePlayer.y = velocity;
    }

    public void SetSpeed(int modifier) //for increment of speed level by level
    {
        speed += modifier;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle" || hit.gameObject.tag == "Limit")
        {
            isDead = true;
            GetComponent<Score>().StopScore();
            audio.Stop();
        }
    }
}
