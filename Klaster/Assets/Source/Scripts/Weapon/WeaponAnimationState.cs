using StateMachine;

public class WeaponAnimationState : State
{
    private readonly WeaponAnimationController _weaponAnimationController;
    private readonly AnimationsType _animationsType;
    private readonly bool _isTrigger;

    public WeaponAnimationState(WeaponAnimationController weaponAnimationController, AnimationsType animationsType,
        bool isTrigger = false)
    {
        _weaponAnimationController = weaponAnimationController;
        _animationsType = animationsType;
        _isTrigger = isTrigger;
    }

    public override void OnStateEnter()
    {
        if (_isTrigger)
        {
            _weaponAnimationController.Play(AnimationsType.Shot, 0);
        }
        else
        {
            _weaponAnimationController.SetBool(_animationsType, true);
        }
    }

    public override void OnStateExit()
    {
        if (_isTrigger == false)
        {
            _weaponAnimationController.SetBool(_animationsType, false);
        }
    }
}