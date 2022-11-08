﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerUseAbilityState
{
    private static readonly int AttackComboIndex = Animator.StringToHash("attackComboIndex");
    private float originalGravityScale;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
        string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.Animator.SetInteger(AttackComboIndex, Player.InputHandler.AttackComboIndex);
        Player.RigidBody.drag = PlayerData.attackMovementDrag;
        originalGravityScale = Player.RigidBody.gravityScale;
        Player.RigidBody.gravityScale = PlayerData.attackGravityScale;
        Player.InputHandler.ConsumeAttackInput();
        Player.InputHandler.ResetComboDetection();
        Collider2D collision = Physics2D.OverlapCircle(Player.attackCheck.position, PlayerData.attackCheckDistance,
            PlayerData.enemyLayer);
        if (collision != null)
        {
            if (collision.tag == "Drop")
            {
                collision.GetComponent<EnemyDrops>().DropDeath();
            }
            else
            {
                Player.cinemachineImpulseSource.GenerateImpulse();
                Health health = collision.GetComponent<Health>();
                if (health) health.CurHealth -= PlayerData.attackDamage;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        Player.RigidBody.drag = 0f;
        Player.RigidBody.gravityScale = originalGravityScale;
    }

    public override void Update()
    {
        base.Update();

        if (StateMachine.CurState == this)
        {
            if (animationFinished)
            {
                isAbilityDone = true;
                Player.InputHandler.StopComboDetection();
            }
        }
    }
}