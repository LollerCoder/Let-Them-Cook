using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWASDMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float runSpeed = 10f;

    [Header("References")]
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    Vector2 moveInput;
    Rigidbody myrigidbody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myrigidbody = GetComponent<Rigidbody>();

        //animations
        myAnimator.SetBool("Turn", true);
        myAnimator.SetBool("Ally", true);
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    private Vector2 GetInput()
    {
        Vector2 inputs = Vector2.zero;

        if (Input.GetKey(KeyCode.D))
        {
            inputs += Vector2.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputs += Vector2.left;
        }
        if (Input.GetKey(KeyCode.W))
        {
            inputs += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputs += Vector2.down;
        }

        return inputs;
    }

    private void Walk()
    {
        moveInput = GetInput();

        //moving
        Vector3 PlayerVelocity = new Vector3(moveInput.x * runSpeed, 0, moveInput.y * runSpeed);
        myrigidbody.velocity = PlayerVelocity;

        //flip
        if (moveInput.x == -1) mySpriteRenderer.flipX = true;
        if (moveInput.x == 1) mySpriteRenderer.flipX = false;

        //animating
        if (moveInput != Vector2.zero) myAnimator.SetBool("Walk", true);
        else myAnimator.SetBool("Walk", false);
    }

    public void SetRunSpeed(float _speed)
    {
        runSpeed = _speed;
    }
}
