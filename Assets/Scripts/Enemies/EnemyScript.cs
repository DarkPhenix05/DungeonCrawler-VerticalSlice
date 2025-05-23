using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyScript : MonoBehaviour
{
    public AudioEnenmy _audioManager;

    public EnemyStatsSO stats;
    public Rigidbody _rb;
    private Vector3 _rbSpeed;

    public int hp;

    public NavMeshAgent agent;
    public Transform agentDestination;

    private Coroutine stopMovementCoroutine;
    public float atkTimer;

    //Evento que se llama cuando un enemigo es derrotado.
    public UnityEvent onKilledEnemyEvent;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _audioManager = GetComponent<AudioEnenmy>();
        hp = stats.maxHP;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _rbSpeed = (_rb.velocity).normalized;
        //Debug.Log(_rbSpeed);

        agent.destination = agentDestination.position;

        _audioManager.PlayWalkSound();
    }

    public void TakeDamage(int _dmg)
    {
        hp -= _dmg;

        if (hp <= 0)
        {
            gameObject.SetActive(false);
            onKilledEnemyEvent?.Invoke();
        }

    }

    private void Attack(Player _player)
    {
        //Attack at certain frequency (needs change of logic)
        //Currently attacks UNTIL timer reaches desired frequency by mantaining contact with player

        atkTimer += Time.deltaTime;
        if (atkTimer >= stats.attackFrequency)
        {
            _player.TakeDamage(stats.attackPower);
            _audioManager.PlayAttackSound();
            atkTimer = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Attack(collision.transform.gameObject.GetComponent<Player>());
            _rb.velocity = Vector3.zero;

            if (stopMovementCoroutine != null)
                StopCoroutine(stopMovementCoroutine);

            if(!gameObject)
                stopMovementCoroutine = StartCoroutine(StopMovement());
        }
    }

    private IEnumerator StopMovement()
    {
        agent.isStopped = true;
        _audioManager.StopWalkSound();
        yield return new WaitForSeconds(3);
        agent.isStopped = false;
    }
}