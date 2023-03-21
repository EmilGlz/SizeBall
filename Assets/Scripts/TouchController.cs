using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    #region Singleton
    private static TouchController _instance;
    public static TouchController Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    [SerializeField] private float pressingTime = 0f;
    [SerializeField] private bool isPressing = false;
    [SerializeField] private Action OnPressLevelUpdate;
    [SerializeField] private Action OnTapStarted;
    [SerializeField] private Action OnTapFinished;
    private float updateLevelOnPressIteration = 0.05f;

    public void InitActions(Action onPressing, Action onTapStarted, Action onTapFinished)
    {
        OnPressLevelUpdate += onPressing;
        OnTapStarted += onTapStarted;
        OnTapFinished += onTapFinished;
    }

    public void DisposeActions(Action onPressing, Action onTapStarted, Action onTapFinished)
    {
        OnPressLevelUpdate -= onPressing;
        OnTapStarted -= onTapStarted;
        OnTapFinished -= onTapFinished;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTapStarted?.Invoke();
        isPressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnTapFinished?.Invoke();
        isPressing = false;
    }

    private void Update()
    {
        if (isPressing)
        {
            pressingTime += Time.deltaTime;
            if (pressingTime >= updateLevelOnPressIteration)
            {
                OnPressLevelUpdate?.Invoke();
                pressingTime = 0f;
            }
        }
        else
        {
            pressingTime = 0f;
        }
    }
}
