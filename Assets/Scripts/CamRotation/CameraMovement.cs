using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{    //https://www.youtube.com/watch?v=rDJOilo4Xrg&t=316s

  private Vector3 previousPosition;
  private Camera cam;
  void Start()
    {
        cam = Camera.main;
  
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetMouseButtonDown(0))  
      {
        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
      }

      if (Input.GetMouseButton(0))
      {
          Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

          cam.transform.position = transform.position;

          cam.transform.Rotate(new Vector3(1,0,0), direction.y * 180);
          cam.transform.Rotate(new Vector3(0,1,0), -direction.x * 180,Space.World);
          cam.transform.Translate(new Vector3(0,0,-10));

          previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
      }
    }
}
