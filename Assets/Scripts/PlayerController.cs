using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpAmount;
    [SerializeField]
    private float rotationSpeed;

    private Camera mainCam;
    private int objectCount = 0;
    private Text objectCountText;

    private bool isAirbone = false;
    private bool isGunActive = false;
    private Animator animator;

    [SerializeField]
    private GameObject RightFirePoint;
    [SerializeField]
    private GameObject LeftFirePoint;

   


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<Enemy>().Target = gameObject;
            collision.collider.tag = "Occupied";
            collision.collider.GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color;
            collision.collider.transform.parent = GameObject.Find("Captures").transform;
            UpdateObjectCount(1);
        }
        else if(collision.collider.CompareTag("Ground"))
        {
            isAirbone = false;
        }
    }

    private void UpdateObjectCount(int amount)
    {
        objectCount += amount;
        objectCountText.text = objectCount.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        objectCountText = GameObject.Find("ObjectText").gameObject.GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateBasedOnCamera();
        CheckInputs();
        IncreaseGravity();
    }

    private void IncreaseGravity()
    {
        if(isAirbone)
        {
           
        }
    }

    private void RotateBasedOnCamera()
    {
        var horizontal = Input.GetAxis("Mouse X");
       
        var rot = transform.eulerAngles;

        rot.y += horizontal * Time.deltaTime * rotationSpeed;

        transform.localRotation = Quaternion.Euler(rot);
    }

    private void CheckInputs()
    {
        ///Movement
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
        
        if (Input.GetKeyDown(KeyCode.Space) && !isAirbone)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0) * jumpAmount;
            isAirbone = true;
        }
        ///

        ///Mouse Controls
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            var x = Screen.width / 2;
            var y = Screen.height / 2;
            Ray mouseRay = mainCam.ScreenPointToRay(new Vector3(x,y,0));
           

            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit))
            {
                if(isGunActive && GameObject.Find("Captures").transform.childCount > 0)
                {
                    var child = GameObject.Find("Captures").transform.GetChild(0);
                    child.transform.parent = null;
                    child.transform.position = RightFirePoint.transform.position;
                    var direction = (hit.point - child.transform.position).normalized * 100.0f;
                    child.GetComponent<Enemy>().Target = null;

                    //child.GetComponent<Rigidbody>().isKinematic = true;
                    child.GetComponent<Rigidbody>().velocity = direction;
                    UpdateObjectCount(-1);
                }
            }

        }

        ///Skills and Activations
        if(Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("ToggleGun");
            isGunActive = !isGunActive;
        }
        
    }
}
