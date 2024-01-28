using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private GameObject slapObject, slapObjectContainer;
    [Range(1f, 1000f)]
    [SerializeField] float slapForce = 500f;
    [Range(0.01f, 1f)]
    [SerializeField] float slapVertical = 0.1f;
    private bool slapNow = false;
    private bool slamNow = false;
    public bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        if (movementController == null)
            movementController = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        return;
        switch (movementController.playerControllerID)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Vector2 facing = movementController.facingDirection;
                    slapObjectContainer = Instantiate(slapObject,
                        new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + facing,
                        slapObject.transform.rotation, transform);
                    slapObjectContainer.tag = "P1";
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Vector2 facing = movementController.facingDirection;
                    slapObjectContainer = Instantiate(slapObject,
                        new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + facing,
                        slapObject.transform.rotation, transform);
                    slapObjectContainer.tag = "P2";
                }
                break;
            default:
                break;
        }
    }

    public void OnSlap()
    {
        Vector2 facing = movementController.facingDirection;
        if (slapObjectContainer != null) { return; }

        Debug.Log("Slapping!");
        slapObjectContainer = Instantiate(slapObject,
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + facing,
            slapObject.transform.rotation, transform);

        slapNow = true;
    }

    public void OnSlam()
    {
        Vector2 facing = movementController.facingDirection;
        if (slapObjectContainer != null) { return; }

        Debug.Log("Slamming!");
        slapObjectContainer = Instantiate(slapObject, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + facing, slapObject.transform.rotation, transform);

        slamNow = true;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            if (slapNow && !slamNow)
            {
                Slap(other);
                slapNow = false;
            }
            else if (!slapNow && slamNow)
            {
                Slam(other);
                slamNow = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

    }

    /// <summary>
    /// Performs the physics for the slapping
    /// </summary>
    /// <param name="collision"></param>
    void Slap(Collider2D collision)
    {
        if (collision == null || collision.gameObject == this.gameObject)
            return;

        Vector2 dir = new Vector2(collision.transform.position.x - gameObject.transform.position.x,
            collision.transform.position.y - gameObject.transform.position.y + slapVertical);

        dir = new Vector2(slapForce * dir.x, slapForce * dir.y);

        Debug.Log("PlayerPhysics");
        collision.attachedRigidbody.AddForce(dir);
        Debug.Log(dir);
    }

    void Slam(Collider2D collision)
    {
        if (collision == null || collision.gameObject == this.gameObject) return;

        //slam downwards only
        collision.attachedRigidbody.velocity = Vector2.zero;    //dead stop the being-hit
        Vector2 dir = new Vector2(Vector2.down.x * slapForce, Vector2.down.y * slapForce);

        //Debug.Log("slam force: " + dir);
        collision.attachedRigidbody.AddForce(dir);
    }
}
