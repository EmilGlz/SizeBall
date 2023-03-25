using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    #region Singleton
    private static ObstacleSpawner _instance;
    public static ObstacleSpawner Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    [SerializeField] private GameObject obstaclePrefab;
    private readonly float pathRadius = 3f;
    private readonly float startDelayDistance = 5f;
    private readonly float spawnRatio = 1f;
    private float EndDelayDistance => GameController.Instance.DoorWinDistance;

    [SerializeField] List<GameObject> obstacles;
    public void SpawnObstacles()
    {
        Vector3 destinationPos = GameController.Instance.DestinationPos - (GameController.Instance.DestinationPos - GameController.Instance.StartPos).normalized * EndDelayDistance;
        Vector3 startPos = GameController.Instance.StartPos - (GameController.Instance.StartPos - GameController.Instance.DestinationPos).normalized * startDelayDistance;
        int rockCount = (int)((destinationPos - startPos).magnitude * spawnRatio);
        obstacles = new List<GameObject>(rockCount);
        for (int i = 0; i < rockCount; i++)
        {
            Vector3 woodpos = new Vector3(Random.Range(pathRadius, -pathRadius), 0.1f, Random.Range(startPos.z, destinationPos.z));
            var obj = Instantiate(obstaclePrefab, woodpos, Quaternion.Euler(Random.Range(0, 360), 0, 0));
            obstacles.Add(obj);
        }
    }

    public void DestroyObstaclesByRadius(Vector3 bulletPos, float radius)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (Vector3.Distance(bulletPos, obstacles[i].transform.position) < radius)
            {
                var obj = obstacles[i];
                GameController.Instance.PlayVfx(obstacles[i].transform.position);
                obstacles.RemoveAt(i);
                Destroy(obj);
                i--;
            }
        }
    }
}
