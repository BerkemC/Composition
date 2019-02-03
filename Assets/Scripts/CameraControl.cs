using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeedHorizontal;
    [SerializeField]
    private float rotationSpeedVertical;
    [SerializeField]
    private float maxVerticalRotation;
    [SerializeField]
    private float minVerticalRotation;
    [SerializeField]
    private float maxHorizontalRotation;
    [SerializeField]
    private float minHorizontalRotation;


    private GameObject target;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        offset = target.transform.position - transform.position;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        RotateCamera();
    }


    private void FollowTarget()
    {
        var pos = transform.position;
        pos = target.transform.position - offset;
        transform.position = pos;
    }

    private void RotateCamera()
    {
        var horizontal = Input.GetAxis("Mouse X");
        var vertical = Input.GetAxis("Mouse Y");

        var rot = transform.localEulerAngles;

        rot.y += horizontal * Time.deltaTime * rotationSpeedHorizontal;
        rot.x -= vertical * Time.deltaTime * rotationSpeedVertical;

        if(rot.x > 180) { rot.x -= 360; }

        rot.x = Mathf.Clamp(rot.x, minVerticalRotation, maxVerticalRotation);
        
        transform.localRotation = Quaternion.Euler(rot);

    }
}
