using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreatorPool : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] GameObject MeshParent;

    private List<GameObject> meshParts = new List<GameObject>();

    internal void CreateMesh(List<Vector3> startPlatformPositinsList, List<Vector3> positionsList, GameObject part)
    {
        foreach (GameObject meshPart in meshParts) 
        {
            meshPart.SetActive(false);
        }
        int i = 0;
        foreach (Vector3 pos in startPlatformPositinsList) 
        {
            if (i < meshParts.Count)
            {
                meshParts[i].transform.position = pos;
                meshParts[i].SetActive(true);
            }
            else 
            {
                var obj = Instantiate(part, pos, Quaternion.identity);
                obj.transform.SetParent(MeshParent.transform);
                meshParts.Add(obj);
            }
            i++;
        }

        foreach (Vector3 pos in positionsList)
        {
            if (i < meshParts.Count)
            {
                meshParts[i].transform.position = pos;
                meshParts[i].SetActive(true);
            }
            else
            {
                var obj = Instantiate(part, pos, Quaternion.identity);
                obj.transform.SetParent(MeshParent.transform);
                meshParts.Add(obj);
            }
            i++;
        }
    }
}
