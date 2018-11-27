using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public void CreateSpikes()
    {
        transform.position = new Vector3(0f, CameraManager.Instance.CameraUpYPosition);
    }

    public void UpdateSpikes()
    {
        transform.position = new Vector3(0f, CameraManager.Instance.CameraUpYPosition);
    }
}
