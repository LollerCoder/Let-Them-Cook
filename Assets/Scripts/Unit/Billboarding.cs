using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour {
    //private Vector3 cameraDir;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    public Transform cameraTransform;

    [Header("Angle of Tilt")]
    [SerializeField] 
    public float minVerticalAngle = -30f;
    [SerializeField]
    public float maxVerticalAngle = 20f;

    private void Start()
    {
        this.cameraTransform = Camera.main.transform;

        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        //old
        //Vector3 originalPosition = transform.position;
        //transform.LookAt(transform.position + Camera.main.transform.forward);
        //transform.position = new Vector3(originalPosition.x, this.transform.position.y, originalPosition.z);

        Vector3 directionToCamera = cameraTransform.position - transform.position;

        // Calculate horizontal rotation (Y-axis)
        Vector3 flatDirection = new Vector3(directionToCamera.x, 0, directionToCamera.z);
        Quaternion horizontalRotation = Quaternion.LookRotation(flatDirection);

        // Apply horizontal rotation
        transform.rotation = horizontalRotation;

        // Calculate vertical tilt angle
        float verticalAngle = Vector3.SignedAngle(flatDirection, directionToCamera, transform.right);

        // Clamp vertical tilt
        verticalAngle = Mathf.Clamp(verticalAngle, minVerticalAngle, maxVerticalAngle);

        // Apply vertical tilt
        transform.rotation *= Quaternion.AngleAxis(verticalAngle, Vector3.right);
    }
}
