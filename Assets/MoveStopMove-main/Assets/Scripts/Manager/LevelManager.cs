using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    int numBotE;
    int numBotM;
    int numBotH;
    [SerializeField] GameObject botE;
    [SerializeField] GameObject botM;
    [SerializeField] GameObject botH;
    const int numBotInLevel = 25;
    int totalBot = 100;
    float minX, maxX, minZ, maxZ;
    [SerializeField] NavMeshData mapData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        OnInit();
        ActionManager.OnBotEDead += SpawnBotE;
        ActionManager.OnBotMDead += SpawnBotM;
        ActionManager.OnBotHDead += SpawnBotH;
    }

    public int GetTotalBot()
    {
        return totalBot;
    }

    void RandomNumBot()
    {
        numBotE = Random.Range(13, 18);
        numBotM = Random.Range(numBotInLevel - numBotE - 7, numBotInLevel - numBotE - 2);
        numBotH = numBotInLevel - numBotE - numBotM;
    }

    void OnInit()
    {
        RandomNumBot();
        minX = mapData.sourceBounds.min.x + 20;
        minZ = mapData.sourceBounds.min.z + 20;
        maxX = mapData.sourceBounds.max.x - 20;
        maxZ = mapData.sourceBounds.max.z - 20;
        SpawnBot();
        totalBot = totalBot + numBotInLevel;
    }

    void SpawnBot()
    {
        for(int i = 1; i <= numBotE; i++)
        {
            SpawnBotE();
        }

        for (int i = 1; i <= numBotM; i++)
        {
            SpawnBotM();
        }

        for (int i = 1; i <= numBotH; i++)
        {
            SpawnBotH();
        }
    }

    void SpawnBotE()
    {
        totalBot--;
        if (totalBot == 0)
        {
            return;
        }
        Instantiate(botE, new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ)), Quaternion.identity);
    }

    void SpawnBotM()
    {
        totalBot--;
        if (totalBot == 0)
        {
            return;
        }
        Instantiate(botM, new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ)), Quaternion.identity);
    }

    void SpawnBotH()
    {
        totalBot--;
        if (totalBot == 0)
        {
            return;
        }
        Instantiate(botH, new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ)), Quaternion.identity);
    }

    private void OnDestroy()
    {
        ActionManager.OnBotEDead -= SpawnBotE;
        ActionManager.OnBotMDead -= SpawnBotM;
        ActionManager.OnBotHDead -= SpawnBotH;
    }
}
