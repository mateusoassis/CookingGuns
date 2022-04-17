using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeJujubaAnimator : MonoBehaviour
{
    public Animator anim;
    public ChargeJujubaBehaviour chargeJujubaBehaviour;

    public void StartRoll()
    {
        anim.SetTrigger("StartAttack");
        chargeJujubaBehaviour.rolling = true;
        chargeJujubaBehaviour.lookingAtPlayer = false;
    }

    public void StopRoll()
    {
        anim.SetTrigger("StopAttack");
        chargeJujubaBehaviour.rolling = false;
        chargeJujubaBehaviour.isCooldown = true;
        chargeJujubaBehaviour.state = 1;
    }
}
