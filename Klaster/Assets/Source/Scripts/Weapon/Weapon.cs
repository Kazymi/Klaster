using System.Collections;
using DG.Tweening;
using StateMachine;
using StateMachine.Conditions;
using UnityEngine;
using StateMachine = StateMachine.StateMachine;

public class Weapon : Item
{
    [SerializeField] private float fireRate;
    [SerializeField] private Animator animator;

    [Header("Scope controller")] [SerializeField]
    private Transform hands;

    [SerializeField] private Transform scopePosition;


    private WeaponAnimationController weaponAnimationController;
    private global::StateMachine.StateMachine stateMachine;
    private InputSystem inputSystem;

    private bool isScope;
    private bool isFreeze;

    private Vector3 startPositionHands;

    private void Awake()
    {
        startPositionHands = hands.localPosition;
        inputSystem = FindObjectOfType<InputSystem>();
        weaponAnimationController = new WeaponAnimationController(animator);
    }

    protected override IEnumerator Show()
    {
        weaponAnimationController.Play(AnimationsType.Show);
        weaponAnimationController.SetBool(AnimationsType.Show, true);
        while (weaponAnimationController.CheckAnimationPlayingAndTime(AnimationsType.Show, 0.7f) == false)
        {
            yield return null;
            if (weaponAnimationController.CheckAnimationPlayingAndTime(AnimationsType.Show, 0.7f))
            {
                weaponAnimationController.SetBool(AnimationsType.Show, false);
                InitializeWeaponStateMachine();
            }
        }

        IsShow = true;
    }

    private void Update()
    {
        stateMachine?.Tick();
        ScopeCheck();
    }

    private void ScopeCheck()
    {
        if (inputSystem.IsScope())
        {
            if (isScope == false)
            {
                isScope = true;
                weaponAnimationController.SetFloat(AnimationsType.Scope, 0.5f);
                hands.DOKill();
                hands.DOLocalMove(scopePosition.localPosition, 0.5f);
            }
        }
        else
        {
            if (isScope)
            {
                weaponAnimationController.SetFloat(AnimationsType.Scope, 0f);
                isScope = false;
                hands.DOKill();
                hands.DOLocalMove(startPositionHands, 0.5f);
            }
        }

        isFreeze = inputSystem.IsFreeze();
        if (isFreeze && isScope)
        {
            weaponAnimationController.SetFloat(AnimationsType.Scope, 1f);
        }
        else
        {
            if (isScope)
            {
                weaponAnimationController.SetFloat(AnimationsType.Scope, 0.5f);
            }
        }
    }

    protected override IEnumerator Disable()
    {
        weaponAnimationController.Play(AnimationsType.Disable);
        weaponAnimationController.SetBool(AnimationsType.Disable, true);
        while (ReadyToDisable == false)
        {
            yield return null;
            ReadyToDisable = weaponAnimationController.CheckAnimationPlayingAndTime(AnimationsType.Disable, 0.7f);
            if (ReadyToDisable)
            {
                weaponAnimationController.SetBool(AnimationsType.Disable, false);
            }
        }
    }

    private void InitializeWeaponStateMachine()
    {
        var idleState = new WeaponAnimationState(weaponAnimationController, AnimationsType.Idle);
        var runState = new WeaponAnimationState(weaponAnimationController, AnimationsType.Run);
        var reloadState = new WeaponAnimationState(weaponAnimationController, AnimationsType.Reload);
        var shotState = new WeaponAnimationState(weaponAnimationController, AnimationsType.Shot, true);
        var reviewState = new WeaponAnimationState(weaponAnimationController, AnimationsType.Review);

        idleState.AddTransition(new StateTransition(runState, new FuncCondition(() => inputSystem.IsRun())));
        idleState.AddTransition(new StateTransition(reloadState, new FuncCondition(() => inputSystem.IsReload())));
        idleState.AddTransition(new StateTransition(reviewState, new FuncCondition(() => inputSystem.IsReview())));
        idleState.AddTransition(new StateTransition(shotState, new FuncCondition(() => inputSystem.IsShot())));

        runState.AddTransition(new StateTransition(idleState, new FuncCondition(() => inputSystem.IsRun() == false)));

        shotState.AddTransition(new StateTransition(idleState, new TemporaryCondition(fireRate)));

        reviewState.AddTransition(new StateTransition(idleState,
            new FuncCondition(() =>
                weaponAnimationController.CheckAnimationPlayingAndTime(AnimationsType.Review, 0.8f))));
        reviewState.AddTransition(new StateTransition(shotState, new FuncCondition(() => inputSystem.IsShot())));
        reviewState.AddTransition(new StateTransition(reloadState, new FuncCondition(() => inputSystem.IsReload())));
        reviewState.AddTransition(new StateTransition(runState, new FuncCondition(() => inputSystem.IsRun())));

        reloadState.AddTransition(new StateTransition(idleState,
            new FuncCondition(() =>
                weaponAnimationController.CheckAnimationPlayingAndTime(AnimationsType.Reload, 0.8f))));

        stateMachine = new global::StateMachine.StateMachine(idleState);
    }
}