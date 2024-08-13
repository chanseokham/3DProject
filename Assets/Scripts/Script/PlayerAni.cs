using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    Idle,
    Walk,
    SwordAttack,
    BowAttack,
    SpearAttack,
    AttackIdle,
    Die
}


public class PlayerAni : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }
    public void ChangeAni(PlayerState aniNumber)
    {
        anim.SetInteger("aniName",(int)aniNumber);
    }
}
