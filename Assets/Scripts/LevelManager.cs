using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Scene level1;
    public void loadLevel1()
    {
        SceneManager.LoadScene(level1.name);
    }
}
