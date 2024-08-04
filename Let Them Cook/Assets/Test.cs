using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
     private float xMin = -0.5f, xMax = 0.5f;
    private float timeValue = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         // Compute the sin position.
        float xValue = Mathf.Sin(timeValue * 5.0f);

        // Now compute the Clamp value.
        float xPos = Mathf.Clamp(xValue, xMin, xMax);

        // Update the position of the cube.
        transform.position = new Vector3(xPos, 0.0f, 0.0f);

        // Increase animation time.
        timeValue = timeValue + Time.deltaTime;

        // Reset the animation time if it is greater than the planned time.
        if (xValue > Mathf.PI * 2.0f)
        {
            timeValue = 0.0f;
        }
    }
}
