using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : MonoBehaviour
{
    Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
    }

    public void OnSlap()
    {
        m_animator.SetTrigger("Slam Attack");
    }

    public void OnMove(InputValue value)
    {
        Vector2 leftStick = value.Get<Vector2>();

        bool moving = leftStick.magnitude > 0f;
        m_animator.SetBool("Moving", moving);
    }
}
