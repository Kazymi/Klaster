using System.Collections;
using UnityEngine;

public class Weapon : Item
{
    [SerializeField] private Animator animator;

    private WeaponAnimationController weaponAnimationController;

    private void Awake()
    {
        weaponAnimationController = new WeaponAnimationController(animator);
    }

    protected override IEnumerator Show()
    {
        weaponAnimationController.Play(AnimationsType.Show);
        while (weaponAnimationController.CheckAnimationPlayingAndTime(AnimationsType.Show, 0.7f) == false)
        {
            yield return null;
        }

        IsShow = true;
    }

    protected override IEnumerator Disable()
    {
        weaponAnimationController.Play(AnimationsType.Disable);
        while (ReadyToDisable == false)
        {
            yield return null;
            ReadyToDisable = weaponAnimationController.CheckAnimationPlayingAndTime(AnimationsType.Disable, 0.7f);
        }
    }
}