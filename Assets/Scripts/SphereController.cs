using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SphereController : MonoBehaviour
{
    Rigidbody rb;
    float xInput;
    float zInput;
    public float moveSpeed;
    public float jumpForce;
   // public float maxX;
    //public float maxZ;
    public Transform spawnPoint;
    private int count;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public AudioClip coinSound;
    public AudioClip spikeSound;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Call the Jump method to make the game object jump
            Jump();
        }
    }

    private void FixedUpdate() 
    {
        float xSpeed = xInput * moveSpeed; // defines the speed in x direction
        float zSpeed = zInput * moveSpeed; // defines the speed in z direction

        rb.velocity = new Vector3(xSpeed,  rb.velocity.y, zSpeed); // sets the velocity of the sphere to the defined speeds (no y-axis movement)
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            count++;
            countText.text = "Score - " + count.ToString();
            GetComponent<AudioSource>().PlayOneShot(coinSound);


            if (count >= 15)
            {
                winText.gameObject.SetActive(true);
            }

            else
            {
                loseText.gameObject.SetActive(true);

            }
        }

        if (collision.gameObject.tag == "Spike")
        {
            Destroy(collision.gameObject);
            count--;
            countText.text = "Score - " + count.ToString();
            GetComponent<AudioSource>().PlayOneShot(spikeSound);

        }


    }

    private void Jump()
    {
        // Check if the Rigidbody component is not null
        if (rb != null)
        {
            // Apply an upward force to make the game object jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    /*void Spawnball()
    {
        float randomX = Random.Range(-maxX, maxX); // Comment: Generate a random X position
        float randomZ = Random.Range(-maxZ, maxZ); // Comment: Generate a random Z position

        Vector3 randomSpawnPos = new Vector3(randomX, 10f, randomZ); // Comment: Create a random spawn position

        Instantiate(ball, randomSpawnPos, Quaternion.identity); // Comment: Instantiate the ball at the random position
    }*/
}
