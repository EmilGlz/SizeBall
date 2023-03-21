using System.Collections.Generic;
using UnityEngine;

public class Player : Ball
{
    [SerializeField] Transform door;
    [SerializeField] Transform bulletSpawnTransform;
    [SerializeField] private GameObject bulletPrefab;
    [Space]
    [SerializeField] private Bullet _bullet;
    bool CanGenerateBullet => currentLevel > minLevel;
    protected override int startLevel { get => maxLevel; }

    protected override void OnPressLevelUpdate()
    {
        if (!CanGenerateBullet)
        {
            GameController.Instance.PlayVfx(transform.position);
            Dispose();
        }
        DecreaseScaleByOne();
        UpdateColliderRadius();
    }

    protected override void OnTapStarted()
    {
        if (!CanGenerateBullet)
        {
            GameController.Instance.PlayVfx(transform.position);
            Dispose();
        }
        if (_bullet != null)
            return;
        // create bullet
        var bullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, bulletSpawnTransform.rotation);
        _bullet = bullet.GetComponent<Bullet>();
        _bullet.Init(door);
    }

    protected override void OnTapFinished()
    {
        if (_bullet == null)
            return;
        // send bullet
        GameController.DelayAction(() => { 
            if(_bullet != null)
                _bullet.StartMoving();
            _bullet = null;
        }, 500);
    }
}
