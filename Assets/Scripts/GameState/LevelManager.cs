using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] PlayerControlls playerControlls;
    [SerializeField] PlayerUI playerUI;
    //[SerializeField] private MeshCreator meshCreator;
    [SerializeField] private MeshCreatorPool meshCreatorPool;
    [SerializeField] private BonusSpawner bonusSpawner;
    [SerializeField] private GameObject blockPrefab;

    Vector3 startPos = new Vector3(0, 0, 0);

    [SerializeField] private int PathLength = 20;
    [SerializeField] private int StartPlatformSize = 3;
    [SerializeField] private int PathWidth = 1;

    private PathCreator pathCreator;
    public static int points;
    // Start is called before the first frame update
    void Start()
    {
        OnLevelFailed += ActivateGameOverPanel;
        LoadLevel();
    }

    public void LoadLevel()
    {
        if (pathCreator == null)
            pathCreator = new PathCreator();

        points = 0;

        playerControlls.StartGame();
        pathCreator.CreatePath(meshCreatorPool, bonusSpawner, blockPrefab, startPos, StartPlatformSize, PathWidth, PathLength);

        playerUI.RestartLevel();
    }

    public static Action OnLevelFailed;

    private void ActivateGameOverPanel()
    {
        playerControlls.PauseGame();
        playerUI.GameOver();
    }

    public static void AddPoints(int ammount = 1) 
    {
        points += ammount;
    }

    public static void SubtractPoints(int ammount = 1)
    {
        points -= ammount;
    }
}
