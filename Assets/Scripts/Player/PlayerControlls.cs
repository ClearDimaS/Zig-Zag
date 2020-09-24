using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerControlls : MonoBehaviour
{
    IMovableObject playerMover;
    private bool GameActive;
    // Start is called before the first frame update
    void Start()
    {
        playerMover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            playerMover.ChangeDirection(); //.MoveRight = !playerMover.MoveRight;
    }

    private void FixedUpdate()
    {
        if(GameActive)
            playerMover.MoveObject();
    }

    public void StartGame() 
    {
        GameActive = true;
    }

    public void PauseGame()
    {
        GameActive = false;
    }
}
