using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile 
{
    void SetDamage(int damage);
    void Fire(Transform target);
}
