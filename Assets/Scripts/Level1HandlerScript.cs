using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1HandlerScript : MonoBehaviour
{
    private int current_wave;
    private int total_waves;

    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;

    private bool aWaveInProgress;

    public int enemiesLeft;

    private Vector3 [] spawnPoints = {
        new Vector3(16.2999992f,4.29400015f,-68.0999985f),
        new Vector3(11.3999996f, 4.29400015f, -36.7000008f), 
        new Vector3(-39f, 4.29400015f, -44.5f), 
        new Vector3(-43.0999985f, 4.29400015f, -18f), 
        new Vector3(-50f, 4.29400015f, 26.5f), 
        new Vector3(41.5f, 4.29400015f, 40.7000008f), 
        new Vector3(53.0999985f, 4.29400015f, -33.9000015f), 
        new Vector3(16.2999992f, 4.29400015f, -68.0999985f), 
        new Vector3(11.3999996f, 4.29400015f, -36.7000008f), 
        new Vector3(-39f, 4.29400015f, -44.5f), 
        new Vector3(17.7000008f, 4.29400015f, -19.7999992f), 
        new Vector3(10.6999998f, 4.29400015f, 25.1000004f), 
        new Vector3(-21.1000004f, 4.29400015f, 59.2999992f), 
        new Vector3(-43.0999985f, 4.29400015f, -18f), 
        new Vector3(-50f, 4.29400015f, 26.5f), 
        new Vector3(41.5f, 4.29400015f, 40.7000008f), 
        new Vector3(-55.9000015f, 4.29400015f, 53.9000015f), 
        new Vector3(13.3000002f, 4.29400015f, 106.099998f), 
    };

    void Start()
    {
        current_wave = 0;
        total_waves = 5;
        spawnEnemies(3);
    }

    void spawnEnemies(int total)
    {
        aWaveInProgress = true;
        enemiesLeft = total;
        for (int i = 0; i < (total / 3); i++)
        {
            Instantiate(enemy1Prefab, spawnPoints[i], Quaternion.identity);
            Instantiate(enemy2Prefab, spawnPoints[i+1], Quaternion.identity);
            Instantiate(enemy3Prefab, spawnPoints[i+2], Quaternion.identity);
        }
    }

    void Update()
    {
        if (!aWaveInProgress)
        {
            switch(current_wave)
            {
                case 1:
                    spawnEnemies(6);
                break;
                case 2:
                    spawnEnemies(9);
                break;
                case 3:
                    spawnEnemies(12);
                break;
                case 4:
                    spawnEnemies(15);
                break;
                case 5:
                    spawnEnemies(18);
                break;
            }
        }

        if (enemiesLeft == 0) {
            aWaveInProgress = false;
            current_wave += 1;

            if (current_wave >= total_waves)
            {
                SceneManager.LoadScene(0);
            }
        }
        
    }
}
