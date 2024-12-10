using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float followspeed = 2f;

    public float minX;
    public float maxX;

    private void LateUpdate()
    {
        Vector3 viewpos = Camera.main.WorldToViewportPoint(playerTransform.position);

        if(viewpos.x < 0.45f || viewpos.x > 0.55f || viewpos.y < 0.01f || viewpos.y > 0.99f)
        { 
            Vector3 cameraPosition = playerTransform.position + offset;
            Vector3 following = Vector3.Lerp(transform.position, cameraPosition, followspeed * Time.deltaTime);

            float clampedX = Mathf.Clamp(following.x, minX, maxX);

            transform.position = new Vector3(clampedX, following.y, following.z);
     
        }
        
    }
}
