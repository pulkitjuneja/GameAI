using System.Collections.Generic;
using UnityEngine;

public class CameraFollowComponent : MonoBehaviour
{
    public Transform target;
     public float smoothTime = 0.3f;
     public float xOffset;
     private Vector3 velocity = Vector3.zero;

    public void FixedUpdate () {
      Vector3 goalPos = target.position;
      goalPos.y = transform.position.y;
      goalPos.x -= xOffset;
      transform.position = Vector3.SmoothDamp (transform.position, goalPos, ref velocity, smoothTime);
    }

}
