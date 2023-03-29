using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Scene level1;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void loadLevel1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void Quit()
    {
        Application.Quit();
    }
}
