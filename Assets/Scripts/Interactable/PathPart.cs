using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPart : MonoBehaviour, IInteracable
{
    [SerializeField] Animator animator;
    public void Interact()
    {
        animator.Play("PathPartFall");
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        animator.Play("PathPartIdle");
    }
}
