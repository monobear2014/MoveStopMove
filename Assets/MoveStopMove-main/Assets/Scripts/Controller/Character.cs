using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Character : Player
{
    [SerializeField] DynamicJoystick joystick;
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] float speed;
    [SerializeField] ParticleSystem upsizeEffect;
    [SerializeField] SphereCollider round;

    float xMovement;
    float zMovement;
    int currentPoint;

    void Move()
    {
        transform.position += new Vector3(xMovement, 0f, zMovement) * speed * Time.deltaTime;
    }

    void CheckJoyStick()
    {
        xMovement = joystick.Horizontal;
        zMovement = joystick.Vertical;
        if (Mathf.Abs(xMovement + zMovement) >= 0.01f)
        {
            isMoving = true;
            anim.SetTrigger("Running");
            wpSkinPoint.gameObject.SetActive(true);
        } else
        {
            isMoving = false;
            if(!enemyOnRange)
            {
                anim.SetTrigger("Idle");
            }
        }
    }

    void RotateSkin()
    {
        skin.rotation = Quaternion.Euler(skin.rotation.x, Mathf.Atan2(joystick.Direction.x, joystick.Direction.y)*Mathf.Rad2Deg, skin.rotation.z);
    }

    void Update()
    {
        if(isDead)
        {
            anim.SetTrigger("Dead");
            return;
        }
        CheckJoyStick();
        if (isMoving)
        {
            Move();
            RotateSkin();
        }
        else
        {
            CheckEnemy();
        }

        if (enemyOnRange && !isMoving)
        {
            anim.SetTrigger("Attack");
        }
    }

    void AddPoint()
    {
        currentPoint++;
        pointText.SetText(currentPoint.ToString());
        if (currentPoint > 0 && currentPoint % 2 == 0)
        {
            UpSize();
        }
        else
        {
            return;
        }
        UpSize();
    }

    private void Start()
    {
        OnInitPlayer();
        ActionManager.OnKilledEnemy += AddPoint;
    }

    private void OnDestroy()
    {
        ActionManager.OnKilledEnemy -= AddPoint;
    }

    void UpSize()
    {
        upsizeEffect.Play();
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        attackrange = attackrange + attackrange*0.1f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.4f);
        Gizmos.DrawSphere(transform.position, attackrange);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            isDead = true;
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            round.gameObject.SetActive(false);
            onDamageEffect.Play();
            ActionManager.OnGameOver?.Invoke();
        }
    }
}
