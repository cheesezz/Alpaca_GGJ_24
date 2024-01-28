using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private GameObject slapObject, slapObjectContainer;
    [Range(1f, 10000f)]
    [SerializeField] float slapForce = 500f;
    [Range(0.01f, 1f)]
    [SerializeField] float slapVertical = 0.1f;
    private bool slapNow = false;
    private bool slamNow = false;
    public bool canJump = false;

    private Rigidbody2D m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        if (movementController == null)
            movementController = GetComponent<PlayerMovementController>();

        m_rigidbody = GetComponent<Rigidbody2D>();

        playerSizeTimesTwo = transform.lossyScale.x * 2;
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

    float playerSizeTimesTwo = 0f;

    public void OnSlap()
    {
        Vector2 facing = movementController.facingDirection;
        if (slapObjectContainer != null) { return; }

        Debug.Log("Attempting Slapping!");
        slapObjectContainer = Instantiate(slapObject,
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + facing,
            slapObject.transform.rotation, transform);

        var e = FindObjectsOfType<PlayerPhysics>();
        bool hasSlappedSomeone = false;
        foreach (var player in e)
        {
            if (player == this)
                continue;

            Vector3 fromOtherToThis = player.transform.position - transform.position;
            float distanceFromOtherPlayer = fromOtherToThis.magnitude;

            if (Mathf.Abs(distanceFromOtherPlayer) <= playerSizeTimesTwo)
            {
                fromOtherToThis.Normalize();
                if (Vector3.Angle(fromOtherToThis, movementController.facingDirection) < 40f) // 40f is max angle
                {
                    Debug.Log("Slapping Someone!");
                    player.m_rigidbody.AddForce(fromOtherToThis * slapForce);
                    hasSlappedSomeone = true;
                }
            }
        }
        if (hasSlappedSomeone)
            AudioManager.instance.PlaySFX(AudioManager.AvailableSFX.Slap);

        //slapNow = true;
    }

    public void OnSlam()
    {
        Vector2 facing = movementController.facingDirection;
        if (slapObjectContainer != null) { return; }

        Debug.Log("Slamming!");
        slapObjectContainer = Instantiate(slapObject, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + facing, slapObject.transform.rotation, transform);

        slamNow = true;
        AudioManager.instance.PlaySFX(AudioManager.AvailableSFX.Slam);
    }

    public void OnJump()
    {
        canJump = false;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
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
        if (other.gameObject.layer == 8)
        {
            canJump = true;
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
