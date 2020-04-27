using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 6f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Thrust();
        Rotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            default:
                print("Dead");
                break;
        }
    }

    private void Thrust()
    {
        if ((Input.GetKey(KeyCode.Space)) || (Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.W)))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow)))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // resume physics control of rotation
    }
}