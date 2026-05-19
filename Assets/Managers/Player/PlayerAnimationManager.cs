using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : PlayerElement
{
    private Animator animator;
    public override void Awake()
    {
        type = TypeOfPlayerScripts.Animation;
        base.Awake();
    }
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public override void PlayerUpdate()
    {
    }

    public void UpdateMovementAnimationData(float newMovementData)
    {
        if(newMovementData > 0.05f || newMovementData < -0.05f)
        {
            newMovementData = newMovementData / player.stats.statics.GetTopSpeed() / 2 + 0.5f * newMovementData < 0f ? -1f : 1f;

            animator.speed = Mathf.Abs(newMovementData);
            animator.SetFloat("Speed", newMovementData);

        }
        else
        {
            animator.SetFloat("Speed", 0);
            animator.speed = 1f;
        }
        Debug.Log(newMovementData);
    }
}
