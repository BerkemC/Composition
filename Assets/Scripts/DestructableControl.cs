using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableControl : MonoBehaviour
{
    [SerializeField]
    private GameObject unitObject;

    [SerializeField]
    private int health;

    private int startingHealth;

    public int Health { get => health; set => health = value; }

    private void Start()
    {
        startingHealth = health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Mathf.Clamp(health, 0, 999);

        if (health == 0) { DestroyObject(); }
    }

    private void DestroyObject()
    {
        var mesh = GetComponent<MeshRenderer>();
        var size = mesh.bounds.size/2;

        for(int i = 0; i < startingHealth/4; i++)
        {
            var obj = Instantiate(unitObject) as GameObject;
            obj.transform.parent = GameObject.Find("Objects").transform;
            obj.transform.position = new Vector3(transform.position.x + (Random.Range(-size.x, size.x)),
                                                 transform.position.y + (Random.Range(-size.y, size.y)), 
                                                 transform.position.z + (Random.Range(-size.z, size.z))
                                                 );
        }
        Destroy(gameObject);
    }
}
