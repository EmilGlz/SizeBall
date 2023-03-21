using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IDisposable
{
    protected SphereCollider collider;
    protected Rigidbody rb;
    protected int minLevel = 0;
    protected int maxLevel = 50;
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
        collider = GetComponent<SphereCollider>();
    }
    private void SetScaleLevels()
    {
        var difference = (maxScale - minScale) / (maxLevel - minLevel);
        for (int i = minLevel; i <= maxLevel; i++)
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
        collider.radius = sphereObject.localScale.x / 2f;
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

    public void DecreaseScaleByOne()
    {
        if (currentLevel <= minLevel)
            return;
        currentLevel--;
        UpdateObjectScale();
    }

    public void IncreaseScaleByOne()
    {
        if (currentLevel >= maxLevel)
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
