using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTransition : MonoBehaviour
{
    private Animator _transitionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _transitionAnimator = gameObject.GetComponent<Animator>();
        if (_transitionAnimator == null)
        {
            Debug.LogError("Unable to fins transition Animator");
        }
    }

    IEnumerator PlayTransitionRoutine()
    {
        _transitionAnimator.SetTrigger("OnSceneLeave");
        yield return new WaitForSeconds(2);
        _transitionAnimator.ResetTrigger("OnSceneLeave");
        _transitionAnimator.SetTrigger("OnSceneEnter");
        yield return new WaitForSeconds(1);
        _transitionAnimator.ResetTrigger("OnSceneEnter");
    }

    public void PlayTransition()
    {
        StartCoroutine(PlayTransitionRoutine());
    }
}
