using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpAmount;

    private Camera mainCam;




    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<Enemy>().Target = gameObject;
            collision.collider.tag = "Occupied";
            collision.collider.GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        if (Input.GetKey(KeyCode.A))
        {
            var pos = transform.position;
            pos += (-1 * transform.right * moveSpeed * Time.deltaTime);
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.D))
        {
            var pos = transform.position;
            pos += transform.right * moveSpeed * Time.deltaTime;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.W))
        {
            var pos = transform.position;
            pos += transform.forward * moveSpeed * Time.deltaTime;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.S))
        {
            var pos = transform.position;
            pos += -1 * transform.forward * moveSpeed * Time.deltaTime;
            transform.position = pos;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0) * jumpAmount;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray mouseRay = mainCam.ScreenPointToRay(Input.mousePosition);

           
            RaycastHit hit;
            if(Physics.Raycast(mouseRay,out hit))
            {
                GetComponent<NavMeshAgent>().SetDestination(hit.point);
            }

        }
    }
}
