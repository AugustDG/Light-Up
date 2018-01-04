using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AMPStudios
{
  public class SwipeControls : MonoBehaviour
  {
    public bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    public Vector2 swipeDelta;
    bool isCenter;
    Vector2 startTouch;

    private void Update()
    {
      tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

      #region Standalone
      if(Input.GetMouseButtonDown(0))
      {
        isCenter = true;
        tap = true;
        startTouch = Input.mousePosition;
      }else if (Input.GetMouseButtonUp(0))
      {
        Reset();
      }
      #endregion

      #region Mobile
      if(Input.touches.Length > 0)
      {
        if (Input.touches[0].phase == TouchPhase.Began)
        {
          isCenter = true;
          tap = true;
          startTouch = Input.touches[0].position;
        }
        else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
        {
          Reset();
        }
      }
      #endregion

      //Calculate the distance
      swipeDelta = Vector2.zero;
      
      if(isCenter)
      {
        if (Input.touches.Length > 0)
          swipeDelta = Input.touches[0].position - startTouch;
        else if (Input.GetMouseButton(0))
          swipeDelta = (Vector2)Input.mousePosition - startTouch;
      }

      //Cross the deadzone?
      if (swipeDelta.magnitude > 100)
      {
        //Direction?
        float x = swipeDelta.x;
        float y = swipeDelta.y;

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
          //Left or right?

          if (x < 0)
            swipeLeft = true;
          else
            swipeRight = true;

        }else if (Mathf.Abs(x) < Mathf.Abs(y))
        {
          //Up or down?

          if (y < 0)
            swipeDown = true;
          else
            swipeUp = true;
        }

        Reset();
      }
    }

    private void Reset()
    {
      isCenter = false;
      startTouch = swipeDelta = Vector2.zero;
    }

  }
}