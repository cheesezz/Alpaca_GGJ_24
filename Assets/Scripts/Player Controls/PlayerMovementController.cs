using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Tooltip("Which Player will control this player Entity?")]
    public short playerControllerID = -1;

    [SerializeField] private PlayerPhysics privatePlayerPhysics;

    public Vector2 facingDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tempControllers();
    }

    void tempControllers()
    {
        if (playerControllerID == 0)
        {
            float horizontalMovement = 0f;
            float verticalMovement = (Input.GetKeyDown(KeyCode.O) && privatePlayerPhysics.canJump) ? 125 : 0;
            horizontalMovement += Input.GetKey(KeyCode.A) ? -1 : 0;
            horizontalMovement += Input.GetKey(KeyCode.D) ? 1 : 0;

            Vector2 movement = new Vector2(horizontalMovement, verticalMovement);
            //Debug.Log(movement);
            facingDirection.x = (movement.x != 0) ? movement.x : facingDirection.x;
            gameObject.GetComponent<Rigidbody2D>().AddForce(movement);
        }
        else
        {
            float horizontalMovement = 0f;
            float verticalMovement = (Input.GetKeyDown(KeyCode.Z) && privatePlayerPhysics.canJump) ? 125 : 0;
            horizontalMovement += Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;
            horizontalMovement += Input.GetKey(KeyCode.RightArrow) ? 1 : 0;

            Vector2 movement = new Vector2(horizontalMovement, verticalMovement);
            //Debug.Log(movement);
            facingDirection.x = (movement.x != 0) ? movement.x : facingDirection.x;

            gameObject.GetComponent<Rigidbody2D>().AddForce(movement);
        }
    }
}
