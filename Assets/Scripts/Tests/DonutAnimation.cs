using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutAnimation : MonoBehaviour
{
    public Animator anim;
    public DonutBehaviour donutBehaviour;

    public void StartRoll()
    {
        anim.SetTrigger("StartAttack");
        donutBehaviour.rolling = true;
        donutBehaviour.lookingAtPlayer = false;
    }

    public void StopRoll()
    {
        anim.SetTrigger("StopAttack");
        donutBehaviour.rolling = false;
        donutBehaviour.isCooldown = true;
        donutBehaviour.state = 1;
        //donutBehaviour.canWalk = true;
    }
}
