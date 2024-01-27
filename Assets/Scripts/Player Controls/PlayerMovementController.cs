using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Tooltip("Which Player will control this player Entity?")]
    public short playerID = -1;

    public Vector2 facingDirection = Vector2.right;

    short assignedPlayerControllerID = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool initialized = false;

    public void Initialize(int assignedController)
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
            float verticalMovement = Input.GetKeyDown(KeyCode.O) ? 125 : 0;
            horizontalMovement += Input.GetKey(KeyCode.A) ? -1 : 0;
            horizontalMovement += Input.GetKey(KeyCode.D) ? 1 : 0;

            Vector2 movement = new Vector2(horizontalMovement, verticalMovement);
            Debug.Log(movement);

            gameObject.GetComponent<Rigidbody2D>().AddForce(movement);
        }
        else
        {
            float horizontalMovement = 0f;
            float verticalMovement = Input.GetKeyDown(KeyCode.Z) ? 125 : 0;
            horizontalMovement += Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;
            horizontalMovement += Input.GetKey(KeyCode.RightArrow) ? 1 : 0;

            Vector2 movement = new Vector2(horizontalMovement, verticalMovement);
            Debug.Log(movement);

            gameObject.GetComponent<Rigidbody2D>().AddForce(movement);
        }
    }
}
