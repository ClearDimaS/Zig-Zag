using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour, IInteracable
{
    [SerializeField] int pointsForPickUp = 1;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem particleSystem;
    bool hasInteracted = false;
    public void Interact()
    {
        if (hasInteracted == false) 
        {
            hasInteracted = true;
            Debug.Log(Time.time);
            animator.Play("BonusTaken");
            LevelManager.AddPoints(pointsForPickUp);
        }
    }

    void OnEnable() 
    {
        hasInteracted = false;
        animator.Play("BonusIdle");
    }

    public void PlayParticleEffect() 
    {
        particleSystem.Play();
    }
}
