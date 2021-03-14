using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameObject gameManager;
    private Rigidbody targetRB;
    private int uVelocity;
    private int tVelocity;
    private int xRange = 4;
    private int ySpawnPos = -6;
    private Vector3 torque;


    public int uVelocityMin = 12;
    public int uVelocityMax = 18;
    public int torqueRange = 10;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        gameManager.GetComponent<GameManager>().lastPos = xRange;

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
        while (gameManager.GetComponent<GameManager>().lastPos == range)
        {
            range = Random.Range(-xRange, xRange);
        }

        gameManager.GetComponent<GameManager>().lastPos = range;
        return range;
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
