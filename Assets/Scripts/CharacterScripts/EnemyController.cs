using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public delegate void EnemyKilled();
public class EnemyController : MonoBehaviour {
    [SerializeField] float[] speed = new float[3] {1f, 1.2f, 1.5f};
    [SerializeField] float gravity;
    [SerializeField] float[] attackTime = new float[3] {1.5f, 1f, 0.5f};
    [SerializeField] int[] attackDamage = new int[3] {1, 2, 4};

    [SerializeField] int[] startHealth = new int[3] {100, 125, 200};
    private float gravityVelocity = 0f;
    private bool freeze = false;

    private GameObject player;
    private int dificulty = 1;
    private NavMeshAgent agent;

    public event EnemyKilled EnemyKilled;
    private int health;

    // Start is called before the first frame update
    void Start () {
        
        if (MainManager.Instance != null) {
            dificulty = MainManager.Instance.dificulty;
        }

        health = startHealth[dificulty];
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
            health = startHealth[dificulty];
        }
    }

    IEnumerator Attacking (Collider other) {
        while (freeze) {
            other.GetComponent<PlayerController>().ReduceHealth(attackDamage[dificulty]);
            yield return new WaitForSeconds(attackTime[dificulty]);
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