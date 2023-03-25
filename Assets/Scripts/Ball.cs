using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IDisposable
{
    protected SphereCollider sphereCollider;
    protected Rigidbody rb;
    protected int MinLevel => GameController.Instance.MinPlayerLevel;
    protected int MaxLevel => GameController.Instance.MaxPlayerLevel;
    protected float minScale = 0.3f;
    protected float maxScale = 3f;
    protected virtual int startLevel { get; set; }
    protected int currentLevel;
    [SerializeField] protected Transform sphereObject;
    private readonly Dictionary<int, float> _levelScales = new Dictionary<int, float>()
    {
    };
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void SetScaleLevels()
    {
        var difference = (maxScale - minScale) / (MaxLevel - MinLevel);
        for (int i = MinLevel; i <= MaxLevel; i++)
        {
            _levelScales[i] = minScale + i * difference;
            //Debug.Log($"[{i}] = {_levelScales[i]}");
        }
    }

    private void Start()
    {
        InitScale();
        UpdateColliderRadius();
        TouchController.Instance.InitActions(OnPressLevelUpdate, OnTapStarted, OnTapFinished);
    }

    private void InitScale()
    {
        SetScaleLevels();
        currentLevel = startLevel;
        UpdateObjectScale();
    }    

    protected float GetSphereRadius()
    {
        return sphereObject.transform.localScale.x;
    }

    protected void UpdateColliderRadius()
    {
        sphereCollider.radius = sphereObject.localScale.x / 2f;
    }

    protected virtual void OnPressLevelUpdate()
    {
    }

    protected virtual void OnTapStarted()
    {
    }

    protected virtual void OnTapFinished()
    {
    }

    public void DecreaseLevel()
    {
        if (currentLevel <= MinLevel)
            return;
        currentLevel--;
        if (currentLevel == 0)
        {
            GameController.Instance.PlayVfx(transform.position);
            GameController.Instance.LoseGame();
            Dispose();
            return;
        }
        UpdateObjectScale();
    }

    public void IncreaseScaleByOne()
    {
        if (currentLevel >= MaxLevel)
            return;
        currentLevel++;
        UpdateObjectScale();
    }

    private void UpdateObjectScale()
    {
        sphereObject.transform.localScale = Vector3.one * _levelScales[currentLevel];
    }

    public void Dispose()
    {
        TouchController.Instance.DisposeActions(OnPressLevelUpdate, OnTapStarted, OnTapFinished);
        Destroy(this.gameObject);
    }
}
