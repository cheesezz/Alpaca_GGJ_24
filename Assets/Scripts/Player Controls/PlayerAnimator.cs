using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : MonoBehaviour
{
    Animator m_animator;
    PlayerPhysics m_playerPhysics;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_playerPhysics = GetComponent<PlayerPhysics>();
    }

    public void OnSlap()
    {
        if (m_playerPhysics.isStunned) return;

        m_animator.SetTrigger("Slapping");
    }

    public void OnSlam()
    {
        if (m_playerPhysics.isStunned) return;

        m_animator.SetTrigger("Slam Attack");
    }

    public void IsFlying(bool val)
    {
        m_animator.SetBool("isSlapped", val);
    }

    public void OnFart()
    {
        if (m_playerPhysics.isStunned) return;

        m_animator.SetTrigger("Fart");
        AudioManager.instance.PlaySFX(AudioManager.AvailableSFX.Fart);
    }

    public void OnMove(InputValue value)
    {
        if (m_playerPhysics.isStunned) return;

        Vector2 leftStick = value.Get<Vector2>();

        bool moving = leftStick.magnitude > 0f;
        m_animator.SetBool("Moving", moving);
    }
}
