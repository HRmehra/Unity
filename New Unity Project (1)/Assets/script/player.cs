using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private bool jumpkeywaspressed;
    private float Horizontalinput;
    private Rigidbody rigidbodycomponent;
    [SerializeField] public Transform groundcheckTranform = null ;
    [SerializeField] private LayerMask playermask;
    private int superJumpremaining;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbodycomponent = GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           jumpkeywaspressed = true;
        }

        Horizontalinput = Input.GetAxis("Horizontal");
    }
    //called once every physx update
    private void FixedUpdate()
    {
        rigidbodycomponent.velocity = new Vector3(Horizontalinput , rigidbodycomponent.velocity.y , 0);

        
       if (Physics.OverlapSphere(groundcheckTranform.position,0.1f, playermask).Length==0)
       {
           return;
       }
        
        if (jumpkeywaspressed)
        {
            float jumpPower = 5f;
            if(superJumpremaining >0)
            {
                jumpPower *= 2;
                superJumpremaining--;
            }
            rigidbodycomponent.AddForce(Vector3.up *7,ForceMode.VelocityChange);
            jumpkeywaspressed =false ;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpremaining++;
        }
    }
   
}
