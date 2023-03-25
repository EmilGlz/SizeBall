using System.Collections.Generic;
using UnityEngine;

public class Bullet : Ball
{
    [SerializeField] float speed;
    protected override int startLevel { get => MinLevel; }
    private Transform _destination;
    private bool _isMoving;

    private readonly float maxDamageRadius = 10f;
    private readonly Dictionary<int, float> _levelDamageRadiuses = new Dictionary<int, float>()
    {
    };
    private float currentDamageRadius => _levelDamageRadiuses[currentLevel];

    private void SetDamageRadiuses()
    {
        var difference = maxDamageRadius / (MaxLevel - MinLevel);
        for (int i = MinLevel; i <= MaxLevel; i++)
        {
            _levelDamageRadiuses[i] = minScale + i * difference;
        }
    }

    protected override void OnPressLevelUpdate()
    {
        if (_isMoving)
            return;
        IncreaseScaleByOne();
        UpdateColliderRadius();
    }

    public void Init(Transform destination)
    {
        _destination = destination;
        SetDamageRadiuses();
    }

    public void StartMoving()
    {
        _isMoving = true;
    }

    private void Update()
    {
        if (_isMoving)
        {
            var direction = (_destination.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            _isMoving = false;
            GameController.Instance.PlayVfx(other.transform.position);
            //Destroy(other.gameObject);
            ObstacleSpawner.Instance.DestroyObstaclesByRadius(transform.position, currentDamageRadius);
            Dispose();
        }
        else if (other.CompareTag("Door"))
        {
            GameController.Instance.door.OpenAnimation();
            GameController.Instance.WinGame();
        }
    }
}
