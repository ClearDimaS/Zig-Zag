using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bonusParent;

    [SerializeField] private GameObject bonusPrefab;

    [Tooltip("If Bonuses are spawned randomly or depending on a number of a tile in a batch")]
    [SerializeField] private bool ifRandomly = true;
    [Tooltip("Number of obejcts in a batch. One bonus per one batch")]
    [SerializeField] private int blockTileCount = 5;

    private List<GameObject> bonuses = new List<GameObject>();
    public void SpawnBonuses(List<Vector3> tilePositions, int tileWidth) 
    {
        foreach (GameObject obj in bonuses) 
        {
            obj.SetActive(false);
        }

        int length = tilePositions.Count / tileWidth / blockTileCount;
        int i = 0;
        int posIndex = 0;
        int bonusInd = 0;
        while (i < length) 
        {
            if (ifRandomly)
            {
                posIndex = (i * blockTileCount + Random.Range(0, blockTileCount)) * tileWidth; // Random.Range(0, tileWidth); replace "+ timeWidth / 2" with this comment to get randomly spawned bonuses through path width too
            }
            else 
            {
                posIndex = i * blockTileCount + (i % 5) * blockTileCount; // Random.Range(0, tileWidth)
            }

            if (bonuses.Count > bonusInd)
            {
                bonuses[bonusInd].transform.position = tilePositions[posIndex] + new Vector3(0, 2.0f, 0);
                bonuses[bonusInd].SetActive(true);
            }
            else 
            {
                var bonus = Instantiate(bonusPrefab, tilePositions[posIndex] + new Vector3(0, 2.0f, 0), Quaternion.identity);
                bonus.transform.parent = bonusParent.transform;

                bonuses.Add(bonus);

            }

            bonusInd++;
            i += 1;
        }
    }
}
