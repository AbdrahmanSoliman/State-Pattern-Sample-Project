using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pursue : State
{
    public Pursue(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.PURSUE;
        agent.speed = 5;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        anim.SetTrigger("isRunning");
        base.Enter();
    }
    public override void Update()
    {
        agent.SetDestination(player.position);
        if(agent.hasPath) // we already passed a destination for agent, but because it may take sometime to create a path so it is better to wait a frame by checking if there is a path (error may happen if we didn't do that)
        {
            if (CanAttackPlayer())
            {
                nextState = new Attack(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer())
            {
                nextState = new Patrol(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }
        
    }
    public override void Exit()
    {
        anim.ResetTrigger("isRunning");
        base.Exit();
    }
}
