using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Singleton
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    [SerializeField] private Transform door;
    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem explosionVFX;

    private void Start()
    {
        ObstacleSpawner.Instance.SpawnObstacles();
    }

    public async void InstantiateObject(GameObject prefab ,int destroyTime = 0)
    {
        Instantiate(prefab);
        if (destroyTime > 0)
        {
            await System.Threading.Tasks.Task.Delay(destroyTime);
            prefab.GetComponent<IDisposable>().Dispose();
        }
    }

    public void PlayVfx(Vector3 position)
    {
        explosionVFX.gameObject.SetActive(true);
        explosionVFX.transform.position = position;
        explosionVFX.Play();
    }

    public static async void DelayAction(Action action, int delay)
    {
        await Task.Delay(delay);
        action.Invoke();
    }

    public Vector3 DestinationPos => door.position;
    public Vector3 StartPos => player.transform.position;
}