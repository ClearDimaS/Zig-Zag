using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    // Update is called once per frame
    private Vector3 DifPos;
    private void Start()
    {
        DifPos = transform.position - playerTransform.position;
    }
    void FixedUpdate()
    {
        transform.position = DifPos + playerTransform.position;
    }
}
