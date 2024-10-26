using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    void Test()
    {
        int x = Random.Range(60, -60);
        int z = Random.Range(60, -60);
        Debug.DrawRay(new Vector3(x, 0, z), Vector3.up, Color.blue, 10.0f);
    }

    private void Update()
    {
        Test();
    }
}
