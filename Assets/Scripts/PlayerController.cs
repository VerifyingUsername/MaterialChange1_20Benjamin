using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isOnGround;
    float jumpForce = 10.0f;
    float gravityModifier = 2.0f;
    float speed = 10.0f;
    float planeLimit = 20.0f;

    Rigidbody playerRb;
    Renderer playerRdr;

    public Material[] playerMtrs;

    // Start is called before the first frame update
    void Start()
    {
        isOnGround = true;
        Physics.gravity *= gravityModifier;

        playerRb = GetComponent<Rigidbody>();
        playerRdr = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        // Foward & backward border
        if (transform.position.z < -planeLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -planeLimit);
            playerRdr.material.color = playerMtrs[2].color;
        }
        else if (transform.position.z > planeLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, planeLimit);
            playerRdr.material.color = playerMtrs[3].color;
        }
        
        // Right & left border
        if (transform.position.x < -planeLimit)
        {
            transform.position = new Vector3(-planeLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[4].color;
        }
        else if (transform.position.x > planeLimit)
        {
            transform.position = new Vector3(planeLimit,transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[5].color;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Listen for collision with the GamePlane TAG
        if (collision.gameObject.CompareTag("GamePlane"))
        {
            isOnGround = true;

            playerRdr.material.color = playerMtrs[1].color;
        }
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            playerRdr.material.color = playerMtrs[0].color;
        }       
    }
}
