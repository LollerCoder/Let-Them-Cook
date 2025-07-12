using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWASDMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    public float RunSpeed = 10.0f;
    private float _runSpeed;

    [Header("References")]
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    Vector2 moveInput;
    Rigidbody myrigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //observers
        EventBroadcaster.Instance.AddObserver(EventNames.Dialogue_Events.ON_DIALOGUE_START, this.NoWalk);
        EventBroadcaster.Instance.AddObserver(EventNames.Dialogue_Events.ON_DIALOGUE_FINISHED, this.YesWalk);

        myAnimator = GetComponentInChildren<Animator>();
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        myrigidbody = GetComponent<Rigidbody>();

        if (myrigidbody == null) Debug.Log("Rigid body not found!");
        else Debug.Log("Rigid body found");

        if (myrigidbody.IsSleeping())
        {
            Debug.Log("Rigid body waking up!");
            myrigidbody.WakeUp();
        }
        else
        {
            Debug.Log("Rigidbody already awake!");
        }


        //animations
        myAnimator.SetBool("Turn", true);
        myAnimator.SetBool("Ally", true);

        Debug.Log("Setting speed");
        this._runSpeed = RunSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    public void NoWalk()
    {
        this._runSpeed = 0;
    }
    
    public void YesWalk()
    {
        this._runSpeed = RunSpeed;
    }

    private Vector2 GetInput()
    {
        Vector2 inputs = Vector2.zero;

        if (Input.GetKey(KeyCode.D) && !CheckWall(Vector3.right))
        {
            inputs += Vector2.right;
        }
        if (Input.GetKey(KeyCode.A) && !CheckWall(Vector3.left))
        {
            inputs += Vector2.left;
        }
        if (Input.GetKey(KeyCode.W) && !CheckWall(Vector3.forward))
        {
            inputs += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S) && !CheckWall(Vector3.back))
        {
            inputs += Vector2.down;
        }

        return inputs;
    }

    private bool CheckWall(Vector3 dir)
    {
        RaycastHit hit;
        Vector3 originPoint = transform.position;
        //Debug.DrawRay(originPoint, dir, Color.red, 15f);
        Physics.Raycast(originPoint, dir, out hit, 5f);

        if (hit.collider == null) return false;

        //Debug.Log(hit);

        if (hit.collider.tag == "Border")
        {
            //Debug.Log("Found wall");
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Walk()
    {
        if (_runSpeed <= 0) return;

        moveInput = GetInput();

        //moving
        Vector3 PlayerVelocity = new Vector3(moveInput.x * _runSpeed, 0, moveInput.y * _runSpeed);
        myrigidbody.velocity = PlayerVelocity;
        //this.gameObject.transform.position += PlayerVelocity;
        //Debug.Log("Velocitty: " + PlayerVelocity);

        //flip
        if (moveInput.x == -1) mySpriteRenderer.flipX = true;
        if (moveInput.x == 1) mySpriteRenderer.flipX = false;

        //animating
        if (moveInput != Vector2.zero) myAnimator.SetBool("Walk", true);
        else myAnimator.SetBool("Walk", false);
    }

    public void SetRunSpeed(float _speed)
    {
        _runSpeed = _speed;
    }
}
