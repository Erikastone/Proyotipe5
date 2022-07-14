using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rbTarget;
    private float minspeed=12;
    private float maxSpeed=16;
    private float maxTorqua=6;
    private float xRange = 1;
    private float ySpawnPos = -4;
    private GameManager gameManager;

    public int pointValue;
    public ParticleSystem particleSystemBoom;

    void Start()
    {
        rbTarget = GetComponent<Rigidbody>();
        /* rbTarget.AddForce(Vector3.up * Random.Range(12, 16), ForceMode.Impulse);
         //усилие, направленное вверх, умноженное на рандомизированную скорость
         rbTarget.AddTorque(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10,10), ForceMode.Impulse);
         //крутящий момент с рандомизированными значениями xyz
         transform.position = new Vector3(Random.Range(-4, 4), -6);
         // позицию с рандомизированным значением X
        */
        rbTarget.AddForce(RandomForce(), ForceMode.Impulse);
        rbTarget.AddTorque(RandomTorqua(), RandomTorqua(), RandomTorqua(),ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        gameManager =GameObject.Find("Game Manager"). GetComponent<GameManager>();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minspeed, maxSpeed);
    }
    float RandomTorqua()
    {
        return Random.Range(-maxTorqua, maxTorqua);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown()
    {
      
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(particleSystemBoom, transform.position, particleSystemBoom.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
       }
    }

}
