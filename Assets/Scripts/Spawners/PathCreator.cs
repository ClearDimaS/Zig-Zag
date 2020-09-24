using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator 
{
    
    public void CreatePath(MeshCreatorPool meshCreator, BonusSpawner bonusSpawner, GameObject blockPrefab, Vector3 startPos, int StartPlatformSize = 3, int PathWidth = 1, int PathLength = 20)
    {
        Vector3 partSize;
        if (blockPrefab.GetComponent<BoxCollider>())
        {
            partSize = blockPrefab.GetComponent<BoxCollider>().size;
        }
        else 
        {
            partSize = blockPrefab.AddComponent<BoxCollider>().size;
        }

        List<Vector3> startPlatformPositionsList = GenerateStartPlatformShape(partSize, startPos, StartPlatformSize);
        List<Vector3> pathPositionsList = GeneratePathShape(partSize, startPos, StartPlatformSize, PathWidth, PathLength);

        meshCreator.CreateMesh(startPlatformPositionsList, pathPositionsList, blockPrefab);
        bonusSpawner.SpawnBonuses(pathPositionsList, PathWidth);
    }

    private List<Vector3> GeneratePathShape(Vector3 partSize, Vector3 startPos, int StartPlatformSize, int PathWidth, int PathLength)
    {
        System.Random rand = new System.Random();

        List<Vector3> pathPositionsList = new List<Vector3>();

        bool whichSideStart = rand.NextDouble() > 0.5;

        if (whichSideStart)
            startPos += new Vector3((StartPlatformSize / 2) * partSize.x, 0, (StartPlatformSize) * partSize.z);
        else
            startPos += new Vector3((StartPlatformSize) * partSize.x, 0, (StartPlatformSize / 2) * partSize.z);

        bool lastMoveRight = !whichSideStart;
        bool moveRight = !whichSideStart;
        int pathLen = -1;

        // Add blocks to satisfy path width requirement
        for (int i = 0; i < PathLength; i++)
        {
            int i2 = 1;
            for (int i1 = 0; i1 < PathWidth; i1++)
            {
                if (i1 == 0)
                    pathPositionsList.Add(startPos);
                else if (i1 % 2 == 1)
                {
                    if (!moveRight)
                    {
                        pathPositionsList.Add(new Vector3(startPos.x + partSize.x * (i2), startPos.y, startPos.z));
                    }
                    else
                    {
                        pathPositionsList.Add(new Vector3(startPos.x, startPos.y, startPos.z + partSize.z * (i2)));
                    }
                }
                else if (i1 % 2 == 0)
                {
                    if (!moveRight)
                    {
                        pathPositionsList.Add(new Vector3(startPos.x - partSize.x * (i2), startPos.y, startPos.z));
                    }
                    else
                    {
                        pathPositionsList.Add(new Vector3(startPos.x, startPos.y, startPos.z - partSize.z * (i2)));
                    }
                    i2++;
                }
            }

            pathLen++; // counter one row added

            if (pathLen >= PathWidth) // if length of blocks after the last turning right or left is greater than its width try to change direction. Necessary if we want to make sure that no part of the path gets less than required width
            {
                moveRight = rand.NextDouble() >= 0.5;
            }

            // Move zero index of the path ahead left or right
            if (moveRight)
            {
                startPos = new Vector3(startPos.x + partSize.x, startPos.y, startPos.z);
            }
            else
            {
                startPos = new Vector3(startPos.x, startPos.y, startPos.z + partSize.z);
            }

            // Make this offset to get rid of the edges
            if (lastMoveRight != moveRight)
            {
                pathLen = 0;
                if (moveRight)
                {
                    startPos = new Vector3(startPos.x + partSize.x * (PathWidth / 2), startPos.y, startPos.z - partSize.z * (PathWidth / 2));
                }
                else
                {
                    startPos = new Vector3(startPos.x - partSize.x * (PathWidth / 2), startPos.y, startPos.z + partSize.z * (PathWidth / 2));
                }
            }

            lastMoveRight = moveRight;
        }

        return pathPositionsList;
    }

    private List<Vector3> GenerateStartPlatformShape(Vector3 size, Vector3 startPos, int blockCount = 3)
    {
        // Generate starting platform sarting with position = startPos Vector3
        var retVal = new List<Vector3>();
        for (int z = 0; z < blockCount; z++) 
        {
            for (int x = 0; x < blockCount; x++) 
            {
                retVal.Add(startPos + new Vector3(x * size.x, 0, z * size.z));
            }
        }
        return retVal;
    }
}
