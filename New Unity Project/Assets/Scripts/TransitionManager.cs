using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    private int _sceneIndex;
    private Scene _scene;
    private Animator _transitionAnimator;
    private static TransitionManager _transitionManager;

    private void Awake()
    {
        if (_transitionManager == null)
        {
            _transitionManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _transitionAnimator = gameObject.GetComponent<Animator>();
        if (_transitionAnimator == null)
        {
            Debug.LogError("Unable to get transition manager's animator component");
        }
    }

   public IEnumerator TransitionToNextScene(int sceneIndex)
    {
        _transitionAnimator.SetTrigger("OnSceneLeave");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneIndex);
        _transitionAnimator.SetTrigger("OnSceneEnter");
        _transitionAnimator.ResetTrigger("OnSceneLeave");
        yield return new WaitForSeconds(3);
        _transitionAnimator.ResetTrigger("OnSceneEnter");
    }

}
