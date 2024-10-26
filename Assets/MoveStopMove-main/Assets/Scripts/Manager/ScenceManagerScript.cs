using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceManagerScript : MonoBehaviour
{
    public void LoadScence(string scenceName)
    {
        SceneManager.LoadScene(scenceName);
    }
}
