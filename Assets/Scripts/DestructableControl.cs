using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructableControl : MonoBehaviour
{

    public static int DestructableCount = 0;

    [SerializeField]
    private GameObject unitObject;

    [SerializeField]
    private int health;

    private int startingHealth;

    public int Health { get => health; set => health = value; }

    private void Start()
    {
        UpdateDestructableCount(1);
        startingHealth = health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        

        if (health <= 0) { DestroyObject(); }
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
        UpdateDestructableCount(-1);
        Destroy(gameObject);
    }

    private void UpdateDestructableCount(int amount)
    {
        DestructableCount += amount;
        GameObject.Find("RemainingText").GetComponent<Text>().text = "Remaining Buildings: " + DestructableCount;
    }
}
