using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float spikesYSize;
    [SerializeField] private float moveUpYSpeed;
    [SerializeField] private float moveDownYSpeed;
    
    public void CreateSpikes()
    {
        transform.position = new Vector3(0f, CameraManager.Instance.CameraUpYPosition + spikesYSize);
    }

    public void MoveSpikes(float deltaTime, bool isUpMove)
    {
        if (isUpMove)
        {
            transform.Translate(0f, moveUpYSpeed * deltaTime, 0f);  
        }
        else
        {
            transform.Translate(0f, -moveDownYSpeed * deltaTime, 0f);  
        }
    }
}
