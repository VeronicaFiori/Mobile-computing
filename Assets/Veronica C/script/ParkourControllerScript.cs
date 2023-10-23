using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public EnvironmentChecker environmentChecker;
    bool playerInAction;
    public Animator animator;
    public PlayerController playerController;

    [Header("Parkour Action Area")]
    public List<NewParkourActions> NewParkourActions;

     private void Update()
    {
        if(Input.GetButton("Jump") && !playerInAction)
        {
            var hitData = environmentChecker.CheckObstacle();

            if (hitData.hitFound)
            {
                foreach(var action in NewParkourActions)
                {
                    if(action.CheckIfAvailable(hitData,transform))
                    {
                        StartCoroutine(PerformParkourAction(action));
                        break;
                    }
                }
            }
        }
    }

    IEnumerator PerformParkourAction(NewParkourActions action)
    {
        playerInAction = true;
        playerController.SetControl(false);

        animator.CrossFade(action.AnimationName, 0.2f);
        yield return null;

        var animationState = animator.GetNextAnimatorStateInfo(0);
        if (!animationState.IsName(action.AnimationName))
            Debug.Log("Animations Name is incorrect");

        yield return new WaitForSeconds(animationState.length);

        playerController.SetControl(true);
        playerInAction = false;
    }
}
