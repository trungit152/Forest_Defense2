using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocTurret : Turrets
{

    private ThrowObjects _throwObjects;
    public ThrowObjects ThrowObjects
    {
        get
        {
            if (_throwObjects == null)
            {
                _throwObjects = GetComponent<ThrowObjects>();
            }
            return _throwObjects;
        }
        set
        {
            _throwObjects = value;
        }
    }
    private void Awake()
    {
        SetBoneSetting();
    }
    public override void Shoot()
    {
        ChangeVisual();
        ThrowObjects.Shoot(_spawnPos != null ? _spawnPos.position : transform.position, _range, 1);
    }

    protected override void ShootCount()
    {
        int amount = 1;
        if (FeelingTools.RandomChance(_multiShootPercent*100))
        {
            amount = _bulletAmount;
        }
        if (_cdRemaining <= 0)
        {
            StartCoroutine(ShootAnimCoroutine(amount));
            _cdRemaining = _coolDown;
        }
        else
        {
            _cdRemaining -= Time.fixedDeltaTime * GameStat.gameTimeScale;
        }
    }

    private IEnumerator ShootAnimCoroutine(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            TurretSpineController.SetShootAnimation();
            yield return new WaitForSeconds(0.3f);
            _cdRemaining = _coolDown;
        }
    }

    protected override void CheckSpineVisual()
    {
        if (_nearestEnemy != null)
        {
            Vector3 direction = _nearestEnemy[0].transform.position - transform.position;
            Vector2 offset = Vector2.zero;
            if (_isUp)
            {
                offset = new Vector2(0, 2);
            }
            else if (_nearestEnemy[0].transform.position.y < 0.5f)
            {
                offset = new Vector2(0, 1f);
            }
            else
            {
                offset = new Vector2(0, 4);
            }
            if (direction.x > 0 && direction.y < 1)
            {
                ShowDownSpine();
                FeelingTools.FlipY(transform, 180);
                _downBoneTarget.SetLocalPosition(MyMath.GetSymmetricOY(direction) + offset);
            }
            else if (direction.x < 0 && direction.y < 1)
            {
                ShowDownSpine();
                FeelingTools.FlipY(transform, 0);
                _downBoneTarget.SetLocalPosition((Vector2)direction + offset);
            }
            else if (direction.x < 0 && direction.y > 1)
            {
                ShowUpSpine();
                FeelingTools.FlipY(transform, 0);
                _upBoneTarget.SetLocalPosition((Vector2)direction + offset);
            }
            else if (direction.x > 0 && direction.y > 1)
            {
                ShowUpSpine();
                FeelingTools.FlipY(transform, 180);
                _upBoneTarget.SetLocalPosition(MyMath.GetSymmetricOY(direction) + offset);
            }
        }
    }
}
