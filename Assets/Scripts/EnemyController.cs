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

    public int health = 100;
    private float gravityVelocity = 0f;
    private bool freeze = false;

    private GameObject player;
    private GameManager manager;
    private NavMeshAgent agent;

    public event EnemyKilled EnemyKilled;

    // Start is called before the first frame update
    void Start () {
        player = GameObject.Find("Player");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update () {
        if (!freeze) {
            FindPlayer();
        }
    }

    private IEnumerator Freeze () {
        freeze = true;

        yield return new WaitForSeconds(attackTime);

        freeze = false;
    }

    void FindPlayer () {
        agent.SetDestination(player.transform.position);
    }

    public void ReduceHealth (int weaponDamage) {
        // if (onHit != null)
        health -= weaponDamage;
        if (health <= 0) {
            if (EnemyKilled != null)
                EnemyKilled();
            
            gameObject.SetActive(false);
            health = 100;
        }
    }
}