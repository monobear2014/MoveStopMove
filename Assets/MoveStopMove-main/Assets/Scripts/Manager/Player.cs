using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] protected Weapon weapon;
    [SerializeField] protected Transform skin;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected Transform wpSkinPoint;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float attackrange;
    [SerializeField] protected ParticleSystem onDamageEffect;

    float distanceMin;
    protected Vector3 direction;
    public LayerMask layer;

    protected bool enemyOnRange = false;
    protected bool isMoving = false;
    protected bool isDead = false;

    protected void Attack()
    {
        wpSkinPoint.gameObject.SetActive(false);
        weapon.ThrowWeapon(attackPoint, skin);
    }

    protected void ResetAttack()
    {
        wpSkinPoint.gameObject.SetActive(true);
        anim.SetTrigger("Idle");
    }

    void OnDead()
    {
        Destroy(gameObject);
    }

    public void CheckEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackrange, layer);
        if (colliders.Length > 1)
        {
            enemyOnRange = true;
            distanceMin = 100;
            for (int i = 0; i < colliders.Length; i++)
            {
                float distance = Vector3.Distance(colliders[i].transform.position, transform.position);
                if (distance < distanceMin && distance > 0)
                {
                    distanceMin = distance;
                    direction = colliders[i].transform.position - transform.position;
                    skin.forward = direction.normalized;
                }
            }
        }
        else
        {
            enemyOnRange = false;
        }
    }

    protected void OnInitPlayer()
    {
        weapon.SpawnWeaponSkin(wpSkinPoint);
    }

    //protected void ChangeAnim(string animName)
    //{
    //    if(currentAnimName != animName)
    //    {
    //        anim.ResetTrigger(animName);
    //        currentAnimName = animName;
    //        anim.SetTrigger(currentAnimName);
    //    }
    //}
}
