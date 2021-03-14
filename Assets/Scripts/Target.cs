using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager gameManagerScript;
    private Rigidbody targetRB;
    private int uVelocity;
    private int tVelocity;
    private int xRange = 4;
    private int ySpawnPos = -6;
    private Vector3 torque;
    private bool isValid = false;


    public int uVelocityMin = 12;
    public int uVelocityMax = 18;
    public int torqueRange = 10;
    public int pointValue = 5;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManagerScript.lastPos = xRange;

        // Get current object RB
        targetRB = GetComponent<Rigidbody>();

        // Add random torque
        RandomTorque();

        // Add randomized upward force
        RandomVelocity();

        // Randomize spawn position
        RandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomTorque()
    {
        // Generate randomized x, y, z torque velocity in a Vector 3
        torque = new Vector3(TorqueRange(tVelocity), TorqueRange(tVelocity), TorqueRange(tVelocity));

        // Add torque impulse to RB to spin object
        targetRB.AddTorque(torque, ForceMode.Impulse);
    }

    void RandomVelocity()
    {
        targetRB.AddForce(Vector3.up * VelocityRange(uVelocity), ForceMode.Impulse);
    }

    void RandomPos()
    {
        transform.position = new Vector3(RandomX(xRange), ySpawnPos);
    }

    int TorqueRange(int velocity)
    {
        velocity = Random.Range(-torqueRange, torqueRange);

        return velocity;
    }

    int VelocityRange(int velocity)
    {
        velocity = Random.Range(uVelocityMin, uVelocityMax);

        return velocity;
    }

    int RandomX(int range)
    {
        while (gameManagerScript.lastPos == range)
        {
            range = Random.Range(-xRange, xRange);
        }

        gameManagerScript.GetComponent<GameManager>().lastPos = range;
        return range;
    }

    private void OnMouseDown()
    {
        if (!gameManagerScript.gameOver)
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);

            if (gameObject.name != "Bad 1(Clone)")
            {
                gameManagerScript.UpdateScore(pointValue);
            }else
            {
                gameManagerScript.UpdateLives(1);
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Sensor Top")
        {
            isValid = true;
        }

        if (other.gameObject.name == "Sensor" && isValid)
        {
            Destroy(gameObject);
            if (gameObject.name != "Bad 1(Clone)")
            {
                gameManagerScript.UpdateLives(1);
            }
            Debug.Log(gameObject.name);
        }

        if (other.gameObject.name == "Sensor Bottom")
        {
            Destroy(gameObject);
        }


    }
}
