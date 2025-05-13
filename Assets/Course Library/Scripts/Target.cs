using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2; 

    private GameManager gameManager; 

    public int scoreToAdd;


    // Start is called before the first frame update
    void Start()
    {
        // randomized rotation speed multiplied by upwards force(pushes object up and rotates it in game)
        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("Game Manager")
            .GetComponent<GameManager>();
        
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ParticleSystem explosionParticle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Blade")&& gameManager.isGameActive)
        {
        Destroy(gameObject);

        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        
        //updates score by 5 each time an object is destroyed
        gameManager.UpdateScore(scoreToAdd);
        }
        else if (other.CompareTag("Sensor"))
        {
        Destroy(gameObject);
        //calls game over in GameManager if a good item falls down then displays game over text in game
        if(!gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLives(-1);
        }
        }
    }

    






}
