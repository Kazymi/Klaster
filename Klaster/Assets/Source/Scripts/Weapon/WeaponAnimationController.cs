using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponAnimationController
{
    private readonly Animator animator;
    private readonly Dictionary<AnimationsType, int> hashes = new Dictionary<AnimationsType, int>();

    public WeaponAnimationController(Animator animator)
    {
        this.animator = animator;
        foreach (AnimationsType animationsType in Enum.GetValues(typeof(AnimationsType)))
        {
            hashes.Add(animationsType, Animator.StringToHash(animationsType.ToString()));
        }
    }

    public bool IsAnimationPlaying(AnimationsType animationsType)
    {
        var returnValue = animator.GetCurrentAnimatorStateInfo(0).fullPathHash == hashes[animationsType];
        return returnValue;
    }

    public bool CheckAnimationPlayingAndTime(AnimationsType animationsType, float normalizeTime)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != hashes[animationsType])
        {
            return false;
        }
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > normalizeTime;
    }

    public void SetFloat(AnimationsType animationsType, float value)
    {
        animator.SetFloat(hashes[animationsType], value);
    }
    
    public void Play(AnimationsType animationsType, int layerIndex = 0)
    {
        animator.Play(hashes[animationsType], layerIndex);
    }

    public void SetBool(AnimationsType animationsType, bool value)
    {
        animator.SetBool(hashes[animationsType], value);
    }
}