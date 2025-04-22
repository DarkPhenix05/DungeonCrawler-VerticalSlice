using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public EnemyStatsSO stats;
    public Rigidbody rb;

    public int hp;

    public NavMeshAgent agent;
    public Transform agentDestination;

    private Coroutine stopMovementCoroutine;
    public float atkTimer;

    private void Awake()
    {
        rb.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        agent.destination = agentDestination.position;
    }

    public void TakeDamage(int _dmg)
    {
        hp -= _dmg;

        if (hp <= 0)
            gameObject.SetActive(false);

    }

    private void Attack(Player _player)
    {
        //Attack at certain frequency (needs change of logic)
        //Currently attacks UNTIL timer reaches desired frequency by mantaining contact with player

        atkTimer += Time.deltaTime;
        if (atkTimer >= stats.attackFrequency)
        {
            _player.TakeDamage(stats.attackPower);
            atkTimer = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Attack(collision.transform.gameObject.GetComponent<Player>());
            rb.velocity = Vector3.zero;

            if (stopMovementCoroutine != null)
                StopCoroutine(stopMovementCoroutine);

            if(!gameObject)
                stopMovementCoroutine = StartCoroutine(StopMovement());
        }
    }

    private IEnumerator StopMovement()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(3);
        agent.isStopped = false;
    }
}