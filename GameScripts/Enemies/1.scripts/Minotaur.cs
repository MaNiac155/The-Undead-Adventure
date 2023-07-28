using Assets.StarterAssets.ThirdPersonController.Scripts;
using Assets.StarterAssets.ThirdPersonController.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Minotaur : EnemyController
{
    public GameObject Weapon;
    public DamagesOpen damage;
    
    private Animator ani;
    private NavMeshAgent agent;
    private EnemyStats stats;
    private Coroutine async;
    private bool AttackLock;
    private int i = 0;
    private float time = 2.0f;
    private float CD = 3f;
    
    private void Awake()
    {
        
        ani=GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
         stats = GetComponent<EnemyStats>();
         stats.data.currentHealth=stats.data.maxHealth;
         AttackLock = false;
         damage = Weapon.GetComponentInChildren<DamagesOpen>();
    }

    public override void StateIdle()
    {
        agent.speed = 0.0f;
        time -= Time.deltaTime;
        ani.SetFloat("Speed", agent.speed, 0.1f, Time.deltaTime);


        if(distance<5)
        {
            agent.speed = 4.0f;
            agent.SetDestination(player.transform.position);
            ani.SetFloat("Speed", agent.speed, 0.1f, Time.deltaTime);
        }
        if (distance < 3)
        {
            ChangeAttack();
        }
        if(stats.currentHealth<=0)
        {
            ChangeDead();
        }
    }



    public override void StateAttack()
    {
        if (AttackLock == false)
        {
            AttackLock = true;
            agent.speed = 0f;
            gameObject.transform.LookAt(player.transform.position);
            ani.SetTrigger("Attack1");
            
            if (async != null)
            {
                StopCoroutine(StateChange());
            }
            async=StartCoroutine(StateChange());
            ani.SetFloat("Speed", agent.speed, 0.1f, Time.deltaTime);
            
        }
        
        if(distance>=2)
        {
            ChangeIdle();
        }
        if(stats.currentHealth<=0)
        {
            ChangeDead();
        }
    }
    
    public override void StateDamage()
    {
        agent.speed = 0;
    }
    
    public override void StateDead()
    {
        agent.speed = 0;
        agent.enabled = false;
         Destroy(gameObject,1f);
    }
    
    public override void ChangeIdle()
    {
        ani.SetTrigger("Idle");
        base.ChangeIdle(); 
    }

    public override void ChangeWalk()
    {
        ani.SetTrigger("Walk");
        base.ChangeWalk();
    }
    
    public override void ChangeDamage()
    {
        ani.SetTrigger("Damage");
        if (async != null)
        {
            StopCoroutine(async);
        }
        async = StartCoroutine(StateChange());
        base.ChangeDamage();
    }

    public override void ChangeDead()
    {
        ani.SetTrigger("Death");
        base.ChangeDead();
    }
    
    private IEnumerator StateChange( )
    {   
        yield return new WaitForSeconds(CD);
        AttackLock = false;
        async = null;
    }

    public void OpenDamage()
    {
        damage.EnableDamage();
    }

    public void CloseDamage()
    {
        damage.DisableDamage();
    }

    //public GameObject attackTarget;
    //void Hit()
    //{
    //    if (attackTarget != null)
    //    {
    //        var targetStats = attackTarget.GetComponent<EnemyStats>();
    //        targetStats.CurrentHealth -= stats.AttackDamage;
    //        Debug.Log(attackTarget.GetComponent<EnemyStats>().CurrentHealth);
    //        if (targetStats.CurrentHealth <= 0)
    //        {
    //            ChangeDead();
    //        }
    //    }
    //}

}
