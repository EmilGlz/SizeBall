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

    public Door door;
    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem explosionVFX;
    public Action<int> OnLevelUpdated;
    public float DoorWinDistance => 5f;
    public Vector3 DestinationPos => door.transform.position;
    public Vector3 StartPos => player.transform.position;
    public int MaxPlayerLevel => 50;
    public int MinPlayerLevel => 0;
    public int CurrentLevel {
        get => PlayerPrefs.GetInt("Level");
        set => PlayerPrefs.SetInt("Level", value);
    }
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

    public static async void DelayActionForOneFrame(Action action)
    {
        await Task.Yield();
        action.Invoke();
    }

    public void WinGame()
    {
        // show win screen UI
        UIManager.Instance.ShowWinScreen();
    }
}