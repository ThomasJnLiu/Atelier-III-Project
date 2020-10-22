using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;
    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    [SerializeField] float verticalLookRotation;
    [SerializeField] bool grounded = true;

    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    Rigidbody rb;

    PhotonView PV;

    public Animator anim;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!PV.IsMine){
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }else{
            gameObject.tag = "Player";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!PV.IsMine){
            return;
        }
        // Only Look with cursor if cursor is locked
        if(Cursor.lockState == CursorLockMode.Locked){
            Look();
        }

        Vector3 moveDir = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);

        if(Input.GetKeyDown(KeyCode.Space) && grounded){
            rb.AddForce(transform.up * jumpForce);
            //PhotonNetwork.Instantiate("Cube", transform.position, transform.rotation, 0);
        }


        // Toggle cursor lock 
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Cursor.lockState == CursorLockMode.Locked){
            Cursor.lockState = CursorLockMode.None;
            }else{
                Cursor.lockState = CursorLockMode.Locked;
            }
            
        }
    }

    void Look(){
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void FixedUpdate(){
        if(!PV.IsMine){
            return;
        }
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        
            if (Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical")!=0) 
            {
                anim.SetBool("idle", false);
                
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetFloat("movementSpeed",3);
            }
            else
            {
                anim.SetFloat("movementSpeed", 1);
            }
            }
            else
            {
                anim.SetBool("idle", true);
            }
            
        
    }

    public void SetGroundedState(bool _grounded){
        grounded = _grounded;
    }
}
