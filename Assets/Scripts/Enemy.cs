using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum EnemyType { Turret,Patroling};

    [SerializeField]
    private int health;
    [SerializeField]
    private float actionDistance;
    [SerializeField]
    private EnemyType type;
   
    private GameObject target = null;
    private GameObject player;

    [SerializeField]
    private GameObject rightShootPoint;
    [SerializeField]
    private GameObject leftShootPoint;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float shootSpeed;
    [SerializeField]
    private float shootPossibility;


    public int Health { get => health; set => health = value; }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Mathf.Clamp(health, 0, 999);

        if(health == 0) { Destroy(gameObject); }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        ChoseAndApplyEnemyAction();
       

    }

    private void ChoseAndApplyEnemyAction()
    {
        if (IsPlayerClose())
        {
            transform.LookAt(player.transform.position);

            if(type.Equals(EnemyType.Turret))
            {
                ShootAtPlayer();
            }
            else
            {
                FollowAndAttackPlayer();
            }

        }
        else
        {
            if(type.Equals(EnemyType.Patroling))
            {
                KeepPatrolling();
            }
        }
    }

    private void KeepPatrolling()
    {
        throw new NotImplementedException();
    }

    private void FollowAndAttackPlayer()
    {
        throw new NotImplementedException();
    }

    private void ShootAtPlayer()
    {

        if(UnityEngine.Random.value <shootPossibility * Time.deltaTime)
        {
            GameObject proj = Instantiate(projectile, (UnityEngine.Random.value < 0.5f) ? rightShootPoint.transform.position : leftShootPoint.transform.position, Quaternion.identity) as GameObject;
            var direction = (player.transform.position - proj.transform.position).normalized;
            var moveAmount = direction * shootSpeed;

            proj.GetComponent<Rigidbody>().velocity = moveAmount;
            Destroy(proj,7);
        }
        
    }

    private bool IsPlayerClose()
    {
        return ((player.transform.position - transform.position).magnitude < actionDistance);
    }
}
