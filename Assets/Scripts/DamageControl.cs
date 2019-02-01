using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControl : MonoBehaviour
{
    public enum DamageType
    {
        SingleShot,
        Continuous
    };

    [SerializeField]
    private int damage = 0;
    [SerializeField]
    private DamageType typeOfDamage = DamageType.SingleShot;

    public int Damage { get => damage; set => damage = value; }
    public DamageType TypeOfDamage { get => typeOfDamage; set => typeOfDamage = value; }

    public void ResolveDamage(Collision col)
    {

        if (damage.Equals(0)) { return; }

        if(CompareTag("EnemyProjectile") && col.collider.CompareTag("Player"))
        {
            GameObject.FindObjectOfType<PlayerController>().TakeDamage(damage);
            if (typeOfDamage.Equals(DamageType.SingleShot)) { damage = 0; }
        }
        else if((CompareTag("Occupied") || CompareTag("Object")) && col.collider.CompareTag("Enemy") )
        {
            col.collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            if (typeOfDamage.Equals(DamageType.SingleShot)) { damage = 0; }
        }
        else if ((CompareTag("Occupied") || CompareTag("Object")) && col.collider.CompareTag("Destructable"))
        {
            col.collider.gameObject.GetComponent<DestructableControl>().TakeDamage(damage);
            if (typeOfDamage.Equals(DamageType.SingleShot)) { damage = 0; }
        }

       
    }
}
