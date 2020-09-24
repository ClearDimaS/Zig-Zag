using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControlls))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour, IMovableObject
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] float Speed = 1.8f;
    [HideInInspector] public bool MoveRight = true;

    private bool isActivated;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Activate", 1.0f);
    }

    public void ChangeDirection() 
    {
        MoveRight = !MoveRight;
    }

    public void MoveObject()
    {
        if (isActivated)
        {
            if (MoveRight)
                rb.velocity = new Vector3(Speed, rb.velocity.y, 0); //transform.position += xSpeed * Time.deltaTime; 
            else
                rb.velocity = new Vector3(0, rb.velocity.y, Speed); //transform.position += zSpeed * Time.deltaTime;
        }
    }

    public void Activate()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    public void ResetParams() 
    {
        transform.position = new Vector3(0, 4, 0);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        isActivated = false;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    // Ground (but it has np IInteractable interface) and Game over plane
    private void OnCollisionEnter(Collision collision)
    {
        var interactable = collision.gameObject.GetComponent<IInteracable>();
        if (interactable != null)
            interactable.Interact();
        isActivated = true;
    }

    //// Bonuses
    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.gameObject.GetComponent<IInteracable>();
        if (interactable != null)
            interactable.Interact();
    }
}
