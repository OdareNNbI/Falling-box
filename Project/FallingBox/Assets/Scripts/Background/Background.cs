using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> spritesRenderers;

    private List<SpriteRenderer> allSpritesRenderers;

    public void Initialize()
    {
        allSpritesRenderers = new List<SpriteRenderer>(spritesRenderers);
    }
    
    public void UpdateBackground(float deltaTime)
    {
        for (int i = 0, n = spritesRenderers.Count; i < n; i++)
        {
            if (spritesRenderers[i].transform.position.y - spritesRenderers[i].bounds.extents.y >=
                CameraManager.Instance.CameraDownYPosition)
            {
                spritesRenderers[i] = Instantiate(spritesRenderers[i],
                    spritesRenderers[i].transform.position + Vector3.down * spritesRenderers[i].bounds.extents.y,
                    Quaternion.identity, transform);

                allSpritesRenderers.Add(spritesRenderers[i]);
            }
        }

        for (int i = allSpritesRenderers.Count - 1; i >= 0; i--)
        {
            if (allSpritesRenderers[i].transform.position.y - allSpritesRenderers[i].bounds.extents.y >=
                CameraManager.Instance.CameraUpYPosition)
            {
                Destroy(allSpritesRenderers[i].gameObject);

                allSpritesRenderers.RemoveAt(i);
            }
        }
    }
}
