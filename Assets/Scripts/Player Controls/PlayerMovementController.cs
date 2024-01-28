using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Tooltip("Which Player will control this player Entity?")]
    public short playerControllerID = -1;

    [SerializeField] private PlayerPhysics privatePlayerPhysics;

    public Vector2 facingDirection = Vector2.zero;
    public bool canMove = true;
    Rigidbody2D m_rigidbody;

    [SerializeField] bool keyboardControls = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        // if (keyboardControls)
        // {
        //     InputDevice[] input = new InputDevice[] { Keyboard.current, Mouse.current };
        //     GetComponent<PlayerInput>().SwitchCurrentControlScheme(input);
        // }
        // else
        // {
        //     GetComponent<PlayerInput>().SwitchCurrentControlScheme(Gamepad.all[0]);
        // }
    }
    SpriteRenderer m_SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;

        if (tempControllersFlag)
            tempControllers();
        
        m_rigidbody.AddForce(forceApplied);

        if (Math.Abs(m_rigidbody.velocity.x) > maxSpeed)
        {
            bool negative = m_rigidbody.velocity.x < 0;
            m_rigidbody.velocity = new Vector2(maxSpeed * (negative ? -1 : 1), m_rigidbody.velocity.y);
        }

        Debug.DrawLine(transform.position, transform.position + new Vector3(facingDirection.x * 5f, facingDirection.y * 5f));
    }

    PlayerInput m_playerInput;

    public void OnJump()
    {
        if (privatePlayerPhysics.canJump) m_rigidbody.AddForce(new Vector2(0, jumpForce));
    }

    Vector2 forceApplied = Vector2.zero;
    Vector2 leftStick;
    public float maxSpeed = 5f;
    public float movementForce = 100f;
    public float jumpForce = 25.5f;

    public void OnMove(InputValue value)
    {
        leftStick = value.Get<Vector2>();
        
        if (leftStick.magnitude != 0)
            facingDirection = leftStick.normalized;

        m_SpriteRenderer.flipX = facingDirection.x > 0;

        forceApplied.x = leftStick.x * movementForce * Time.deltaTime;
    }

    public bool tempControllersFlag = true;
    public int tempID = 0;

    void tempControllers()
    {
        if (tempID == 0)
        {
            float horizontalMovement = 0f;
            float verticalMovement = (Input.GetKeyDown(KeyCode.O) && privatePlayerPhysics.canJump) ? 125 : 0;
            horizontalMovement += Input.GetKey(KeyCode.A) ? -1 : 0;
            horizontalMovement += Input.GetKey(KeyCode.D) ? 1 : 0;

            forceApplied.x = horizontalMovement * Time.deltaTime * movementForce;
            forceApplied.y = verticalMovement * jumpForce;

            if (Input.GetKeyDown(KeyCode.P))
            {
                GetComponent<PlayerInteractionController>().OnSlap();
            }
        }
        else
        {
            float horizontalMovement = 0f;
            float verticalMovement = (Input.GetKeyDown(KeyCode.Z) && privatePlayerPhysics.canJump) ? 125 : 0;
            horizontalMovement += Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;
            horizontalMovement += Input.GetKey(KeyCode.RightArrow) ? 1 : 0;

            forceApplied.x = horizontalMovement * Time.deltaTime * movementForce;
            forceApplied.y = verticalMovement * jumpForce;
        }
        facingDirection = forceApplied.normalized;
    }
}
