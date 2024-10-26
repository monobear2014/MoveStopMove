using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Player
{
    [SerializeField] NavMeshAgent bot;
    [SerializeField] GameObject round;

    public float visionRange = 15f;
    bool enemyOnVisionRange = false;

    Vector3 target;

    private void Start()
    {
        OnInitPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoundPlayer"))
        {
            round.SetActive(true);
        }
        if (other.CompareTag("Bullet"))
        {
            isDead = true;
            anim.SetTrigger("Dead");
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            onDamageEffect.Play();
        }
        else if (other.CompareTag("PlayerBullet"))
        {
            isDead = true;
            anim.SetTrigger("Dead");
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            ActionManager.OnKilledEnemy?.Invoke();
            onDamageEffect.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RoundPlayer"))
        {
            round.SetActive(false);
        }
    }

    private void Update()
    {
        if (isDead)
        {
            Stop();
            anim.SetTrigger("Dead");
            return;
        }
        CheckEnemyOnVision();
        if (enemyOnVisionRange)
        {
            CheckEnemy();
            if (enemyOnRange)
            {
                Stop();
                anim.SetTrigger("Attack");
            }
            else
            {
                Chasing();
            }
        }
        else
        {
            MoveRandom();
        }
    }

    bool RandomPoint(out Vector3 result)
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * 10f;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    void MoveRandom()
    {
        skin.localEulerAngles = Vector3.zero;
        if (bot.remainingDistance <= bot.stoppingDistance)
        {
            if (RandomPoint(out target))
            {
                bot.SetDestination(target);
                anim.SetTrigger("Running");
                Debug.Log("Move random");
            }
        }
    }

    void Stop()
    {
        bot.velocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(3f, 0f, 0f, 0.2f);
        Gizmos.DrawSphere(transform.position, visionRange);
        Gizmos.DrawSphere(transform.position, attackrange);
    }

    void CheckEnemyOnVision()
    {
        Collider[] colliderss = Physics.OverlapSphere(transform.position, visionRange, layer);
        if (colliderss.Length > 1)
        {
            enemyOnVisionRange = true;
            float distanceMin = 30;
            for (int i = 0; i < colliderss.Length; i++)
            {
                float distance = Vector3.Distance(colliderss[i].transform.position, transform.position);
                if (distance < distanceMin && distance > 0)
                {
                    distanceMin = distance;
                    direction = colliderss[i].transform.position - transform.position;
                    skin.forward = direction.normalized;
                    target = colliderss[i].transform.position;
                }
            }
        }
        else
        {
            enemyOnVisionRange = false;
        }
    }

    void Chasing()
    {
        skin.localEulerAngles = Vector3.zero;
        anim.SetTrigger("Running");
        bot.SetDestination(target);
    }
}
