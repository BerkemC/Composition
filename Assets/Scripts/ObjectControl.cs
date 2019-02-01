using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private float minDistance;

    private GameObject target = null;

    public GameObject Target { get => target; set => target = value; }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<DamageControl>().ResolveDamage(collision);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(target && (target.transform.position - transform.position).magnitude > minDistance)
       {
            var direction = (target.transform.position - transform.position).normalized;
            var amount = direction * speed * Time.deltaTime;
            var pos = transform.position;
            pos += amount;
            transform.position = pos;

       }
    }
}
