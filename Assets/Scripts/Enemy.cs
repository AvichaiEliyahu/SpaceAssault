using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem hitParticles;
    GameObject parentGameObject; // store all explosions
    [SerializeField] int scorePerHit= 5;
    [SerializeField] int health;
    [SerializeField] int damagePerHit = 50;

    ScoreBoard scoreBoard;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("ExplodedEnemies");
        AddRigidbody();
    }

    private void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(name + " has " + health + "health left");
        ProccesHit();
        if(health<0)
            Kill();
    }

    private void Kill()
    {
        ParticleSystem ps = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        ps.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }

    private void ProccesHit()
    {
        ParticleSystem ps = Instantiate(hitParticles, transform.position, Quaternion.identity);
        ps.transform.parent = parentGameObject.transform;
        health -= damagePerHit;
        scoreBoard.IncreaseScore(scorePerHit);
    }
}
