using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Sprite Sprite { get; protected set; }

    public Attack()
    {

    }

    static public void Destroy(Attack attack)
    {
        Object.Destroy(attack);
    }
}
