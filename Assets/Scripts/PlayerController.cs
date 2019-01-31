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
    [SerializeField]
    private GameObject RightFirePoint;
    [SerializeField]
    private GameObject LeftFirePoint;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private float shieldFormDelay;
    [SerializeField]
    private GameObject hammerHead;

    [SerializeField]
    private float dodgeAmount;

    private Camera mainCam;
    private Animator animator;
    private Text objectCountText;


    private int objectCount = 0;
    
    private bool isAirbone = false;
    private bool isGunActive = false;
    private bool isShieldActive = false;
    private bool isHammerActive = false;
    

    private GameObject captures;
    private GameObject shield;
   


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Object"))
        {
            collision.collider.GetComponent<ObjectControl>().Target = gameObject;
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
        captures = GameObject.Find("Captures");
        shield = GameObject.Find("Shield");
    }

    // Update is called once per frame
    void Update()
    {
        RotateBasedOnCamera();
        CheckInputs();
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
        MovementControls();
        ///

        ///Mouse Controls
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            var x = Screen.width / 2;
            var y = Screen.height / 2;
            Ray mouseRay = mainCam.ScreenPointToRay(new Vector3(x, y, 0));


            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit))
            {
                GunControls(hit);
            }

        }

        ///Skills and Activations
        SkillAndToolControls();

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(Input.GetKey(KeyCode.A))
            {
                GetComponent<Rigidbody>().velocity = -1*transform.right*dodgeAmount;
            }
            if(Input.GetKey(KeyCode.D))
            {
                GetComponent<Rigidbody>().velocity = transform.right*dodgeAmount;
            }
            if(Input.GetKey(KeyCode.W))
            {
                GetComponent<Rigidbody>().velocity = transform.forward*dodgeAmount;
            }
             if(Input.GetKey(KeyCode.S))
            {
                GetComponent<Rigidbody>().velocity = -1*transform.forward*dodgeAmount;
            }
        }

    }

    private void SkillAndToolControls()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isShieldActive && !isHammerActive)
        {
            animator.SetTrigger("ToggleGun");
            isGunActive = !isGunActive;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && CanActivateTool() && objectCount >= 9)
        {
            StartCoroutine(FormShield());
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) && isShieldActive)
        {
            isShieldActive = false;

            for(int i = 0; i < 9; i++)
            {
                var child = shield.transform.GetChild(0);

                child.parent = captures.transform;
                child.localScale = new Vector3(2, 2, 2);
                child.gameObject.GetComponent<ObjectControl>().Target = gameObject;
                child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
               
            }
  
        }
        if(Input.GetKeyDown(KeyCode.E) && CanActivateTool() && objectCount >= 9)
        {
            isHammerActive = true;
            animator.SetTrigger("ToggleHammer");
            StartCoroutine(FormHammer());
        }
        if(Input.GetKeyDown(KeyCode.R) && isHammerActive)
        {
            isHammerActive = false;
            animator.SetTrigger("ToggleHammer");

            for(int i = 0; i < 9; i++)
            {
                var child = hammerHead.transform.GetChild(0);

                child.parent = captures.transform;
                child.localScale = new Vector3(2, 2, 2);
                child.gameObject.GetComponent<ObjectControl>().Target = gameObject;
                child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && isHammerActive)
        {
            int currentCount = animator.GetInteger("SlamCount");
            currentCount = (currentCount + 1)%4;
            animator.SetInteger("SlamCount",currentCount);
        }
    }

    private void GunControls(RaycastHit hit)
    {

        if (isGunActive && captures.transform.childCount > 0)
        {
            var child = captures.transform.GetChild(0);
            child.transform.parent = null;
            
            if(UnityEngine.Random.value < 0.5f)
            {
                child.transform.position = RightFirePoint.transform.position;

            }
            else
            {
                child.transform.position = LeftFirePoint.transform.position;

            }

            var direction = (hit.point - child.transform.position).normalized * projectileSpeed;
            child.GetComponent<ObjectControl>().Target = null;
            child.GetComponent<Rigidbody>().velocity = direction;
            child.tag = "Object";
            child.GetComponent<MeshRenderer>().material.color = Color.white;
            UpdateObjectCount(-1);
        }
    }

    private void MovementControls()
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

        if (Input.GetKeyDown(KeyCode.Space) && !isAirbone)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0) * jumpAmount;
            isAirbone = true;
        }
    }

    private IEnumerator FormShield()
    {
        isShieldActive = true;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var child = captures.transform.GetChild(0);
                child.transform.parent = shield.transform;
                child.GetComponent<Rigidbody>().isKinematic = true;
                child.GetComponent<ObjectControl>().Target = null;

                child.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

                child.transform.localPosition = new Vector3(1.5f - i, 2.0f - j, 0.0f);

                yield return new WaitForSeconds(shieldFormDelay);
            }
        }

        yield return null;
    }

     private IEnumerator FormHammer()
    {
        for (int i = 0; i < 9; i++)
        {
           
            var child = captures.transform.GetChild(0);
            child.transform.parent = hammerHead.transform;
            child.GetComponent<Rigidbody>().isKinematic = true;
            child.GetComponent<ObjectControl>().Target = null;

            child.transform.localScale = new Vector3(0.35f, 3.33f, 3.33f);
            child.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));

            child.transform.localPosition = new Vector3(UnityEngine.Random.Range(-0.3f,0.5f),UnityEngine.Random.Range(-3f,4f), UnityEngine.Random.Range(-3f,3f));

            yield return new WaitForSeconds(shieldFormDelay);
            
        }

        yield return null;
    }

    private bool CanActivateTool()
    {
        return (!isGunActive && !isShieldActive && !isHammerActive);  
    }

    public void ResetSlamCount()
    {
        animator.SetInteger("SlamCount",0);
    }
}
