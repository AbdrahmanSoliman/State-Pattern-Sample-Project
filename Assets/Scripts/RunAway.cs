using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : State
{
    public RunAway(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.RUNAWAY;
        agent.speed = 6f;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        anim.SetTrigger("isRunning"); 
        agent.SetDestination(GameEnvironment.Singleton.Safepoint.transform.position);
        base.Enter();
    }

    public override void Update()
    {
        if(agent.remainingDistance < 0.1f)
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isRunning");
        base.Exit();
    }
}
