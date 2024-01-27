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
        slapObjectContainer = Instantiate(slapObject,
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + facing,
            slapObject.transform.rotation, transform);

        AudioManager.instance.PlaySFX(AudioManager.AvailableSFX.Slap);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            Slap(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
    }

    void Slap(Collider2D collision)
    {
        if (collision == null || collision.gameObject == this.gameObject)
            return;

        Vector2 dir = new Vector2(gameObject.transform.position.x - collision.transform.position.x,
            gameObject.transform.position.y - collision.transform.position.y + slapVertical);

        dir = new Vector2(slapForce * dir.x, slapForce * dir.y);
        
        gameObject.GetComponent<Rigidbody2D>().AddForce(dir);
        Debug.Log(dir);
    }
}
