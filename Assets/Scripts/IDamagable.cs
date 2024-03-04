using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal);

}
