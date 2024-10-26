using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : MonoBehaviour
{
    [SerializeField] GameObject weaponRotate;
    [SerializeField] float speed;

    private void Start()
    {
        Invoke(nameof(OnDeSpawn), 1.5f);
    }

    void Update()
    {
        weaponRotate.transform.Rotate(0, 0, 8);
        transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
    }

    void OnDeSpawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            OnDeSpawn();
        }
    }
}
