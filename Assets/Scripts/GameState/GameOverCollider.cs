using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCollider : MonoBehaviour, IInteracable
{
    [SerializeField] private PlayerMover playerComponent;
    public void Interact()
    {
        LevelManager.OnLevelFailed?.Invoke();
        if (playerComponent != null)
        {
            playerComponent.ResetParams();
        }
    }
}
