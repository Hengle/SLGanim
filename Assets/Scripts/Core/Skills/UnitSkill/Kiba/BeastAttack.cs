﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastAttack : AttackSkill {

    public override void SetLevel(int level)
    {
        damageFactor = 20 + level * 5;
    }

    protected override void Confirm()
    {
        base.Confirm();
        animator.MatchTarget(focus, character.rotation, AvatarTarget.Root, new MatchTargetWeightMask(Vector3.forward, 0f), 0.21f, 0.37f);
    }

    public override void Effect()
    {
        base.Effect();
        RoundManager.GetInstance().Invoke(() => { render.SetActive(false); }, 1.35f);
        RoundManager.GetInstance().Invoke(() => { render.SetActive(true); }, 1.5f);
    }
}