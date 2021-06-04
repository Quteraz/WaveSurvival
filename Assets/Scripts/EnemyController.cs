using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public delegate void EnemyKilled();
public class EnemyController : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] float gravity;
    [SerializeField] float attackTime = 1;
    [SerializeField] int attackDamage = 1;

    [SerializeField] int startHealth = 100;
    private float gravityVelocity = 0f;
    private bool freeze = false;

    private GameObject player;
    private GameManager manager;
    private NavMeshAgent agent;

    public event EnemyKilled EnemyKilled;
    private int health;

    // Start is called before the first frame update
    void Start () {
        health = startHealth;
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update () {
        if (!freeze) {
            FindPlayer();
        }
    }

    void FindPlayer () {
        agent.SetDestination(player.transform.position);
    }

    public void ReduceHealth (int weaponDamage) {
        // if (onHit != null)
        health -= weaponDamage;
        // Debug.Log("EnemyHealth: " + health);
        if (health <= 0) {
            if (EnemyKilled != null)
                EnemyKilled();
            
            gameObject.SetActive(false);
            health = startHealth;
        }
    }

    IEnumerator Attacking (Collider other) {
        while (freeze) {
            other.GetComponent<PlayerController>().ReduceHealth(attackDamage);
            yield return new WaitForSeconds(attackTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            agent.isStopped = true;
            freeze = true;
            StartCoroutine(Attacking(other));
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            freeze = false;
            agent.isStopped = false;
        }
    }
}