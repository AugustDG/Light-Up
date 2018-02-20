using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AMPStudios
{
  public class PlayerMovement : MonoBehaviour
  {
    public SwipeControls swipeControls;
    public Vector3 desiredPosition;
    public float moveScale;

    void Update()
    {
      if (swipeControls.swipeLeft)
        desiredPosition += Vector3.left;
      if (swipeControls.swipeRight)
        desiredPosition += Vector3.right;
      if (swipeControls.swipeUp)
        desiredPosition += Vector3.up;
      if (swipeControls.swipeDown)
        desiredPosition += Vector3.down;

      transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveScale * Time.deltaTime);

      if (transform.position.z > 10)
      {
        desiredPosition = new Vector3(0, 1, -1);
        transform.position = new Vector3(0, 1, -1);
      }
    }
  }
}