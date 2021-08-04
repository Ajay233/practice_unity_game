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
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("About to start on scene: " + SceneManager.GetActiveScene().buildIndex);
        _scene = SceneManager.GetActiveScene();
        _sceneIndex = _scene.buildIndex;
        _transitionAnimator = gameObject.GetComponent<Animator>();
        if (_transitionAnimator == null)
        {
            Debug.LogError("Unable to get transition manager's animator component");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > _sceneIndex)
        {
            _sceneIndex = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(TransitionIntoScene());
        }
    }

   IEnumerator TransitionIntoScene()
    {
        _transitionAnimator.SetTrigger("OnSceneEnter");
        _transitionAnimator.ResetTrigger("OnSceneLeave");
        yield return new WaitForSeconds(3);
        _transitionAnimator.ResetTrigger("OnSceneEnter");
    }

}
