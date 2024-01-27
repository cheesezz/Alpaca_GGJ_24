using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] BoxCollider2D interactionCollider;
    [SerializeField] PlayerMovementController playerMovementController;

    [Range(1f, 1000f)]
    [SerializeField] float pushingForce;


    // Start is called before the first frame update
    void Start()
    {
        if (interactionCollider == null)
        {
            Debug.LogError("This object has no interaction collider assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //update the interaction collider's position
        Vector2 colliderPos = playerMovementController.facingDirection;
        interactionCollider.transform.SetLocalPositionAndRotation(colliderPos, Quaternion.identity);

    }

    private void TryPush(Rigidbody2D playerRB)
    {
        //there is no player to push
        if (playerRB == null)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 slapVec = new Vector2(playerMovementController.facingDirection.x * pushingForce, 0f);
            Debug.Log("slap vec: " + slapVec);
            playerRB.AddForce(slapVec);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TryPush(collision.attachedRigidbody);
    }
}
