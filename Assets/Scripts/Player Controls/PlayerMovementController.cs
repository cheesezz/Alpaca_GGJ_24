using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Tooltip("Which Player will control this player Entity?")]
    public short playerControllerID = -1;

    [SerializeField] private PlayerPhysics privatePlayerPhysics;

    public Vector2 facingDirection = Vector2.zero;
    Rigidbody2D m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //tempControllers();
        
        m_rigidbody.AddForce(forceApplied);

        if (Math.Abs(m_rigidbody.velocity.x) > maxSpeed)
        {
            bool negative = m_rigidbody.velocity.x < 0;
            m_rigidbody.velocity = new Vector2(maxSpeed * (negative ? -1 : 1), m_rigidbody.velocity.y);
        }
    }

    PlayerInput m_playerInput;

    public void OnJump()
    {
        m_rigidbody.AddForce(new Vector2(0, jumpForce));
    }

    Vector2 forceApplied = Vector2.zero;
    Vector2 leftStick;
    public float maxSpeed = 5f;
    public float movementForce = 100f;
    public float jumpForce = 25.5f;

    public void OnMove(InputValue value)
    {
        leftStick = value.Get<Vector2>();
        forceApplied.x = leftStick.x * movementForce * Time.deltaTime;
    }

    void tempControllers()
    {
        {
            float horizontalMovement = 0f;
            float verticalMovement = (Input.GetKeyDown(KeyCode.O) && privatePlayerPhysics.canJump) ? 125 : 0;
            horizontalMovement += Input.GetKey(KeyCode.A) ? -1 : 0;
            horizontalMovement += Input.GetKey(KeyCode.D) ? 1 : 0;

            Vector2 movement = new Vector2(horizontalMovement, verticalMovement);
            //Debug.Log(movement);

            gameObject.GetComponent<Rigidbody2D>().AddForce(movement);
        }
    }
}
