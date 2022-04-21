using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoint : MonoBehaviour
{
    public bool AreaCleared { get; private set; }

    [SerializeField]
    private EnemyEntry[] m_EnemyEntries;

    private bool m_ActivePoint = false;
    private CameraPathMovement m_CameraPathMovement;
    private int m_EnemiesKilled = 0;

    public void Initialise(CameraPathMovement cameraPathMovement)
    {
        m_CameraPathMovement = cameraPathMovement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraReachedPoint()
    {
        m_ActivePoint = true;
        m_CameraPathMovement.SetCameraMovement(false);
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        foreach (var enemyEntry in m_EnemyEntries)
        {
            yield return new WaitForSeconds(enemyEntry.spawnDelay);

            enemyEntry.enemy.Initialise(this);

            Debug.Log(enemyEntry.enemy.gameObject.name + " has spawned!");
        }
    }

    public void EnemyKilled()
    {
        m_EnemiesKilled++;

        if (m_EnemiesKilled == m_EnemyEntries.Length)
        {
            m_CameraPathMovement.SetCameraMovement(true);
            AreaCleared = true;
            m_ActivePoint = false;
        }
    }
}

[System.Serializable]
public class EnemyEntry
{
    public Enemy enemy;
    public float spawnDelay;
}
