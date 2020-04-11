using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
    void onHit(int damage);
    void onHit();
}
