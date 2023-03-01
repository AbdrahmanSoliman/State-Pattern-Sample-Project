using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE { IDLE, PATROL, PURSUE, ATTACK, SLEEP, RUNAWAY }
    public enum EVENT { ENTER, UPDATE, EXIT }

    public STATE name;
    protected EVENT stage; 
    protected State nextState;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected NavMeshAgent agent;

    float visDistance = 10f;
    float visAngle = 30f;
    float shootDist = 7f;
    float visBehindAngle = 30f;

    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        player = _player;
        stage = EVENT.ENTER;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if(stage == EVENT.ENTER) Enter();
        if(stage == EVENT.UPDATE) Update();
        if(stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if(direction.magnitude < visDistance && angle < visAngle) // in the range of npc's sight (distance and angle)
        {
            return true;
        }
        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;

        if(direction.magnitude < shootDist)
        {
            return true;
        }
        return false;
    }

    public bool CanFeelPlayerBehindHim()
    {
        Vector3 direction = npc.transform.position - player.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if(direction.magnitude < 2 && angle < visBehindAngle) // in the range of npc's back sight (distance and angle)
        {
            return true;
        }
        return false;
    }

}