using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    private AudioSource _audioSource;
    private AudioSource _canvasAudio;
    private Animator _transitionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Unable to find Audio Source");
        }

        _canvasAudio = GameObject.Find("Canvas").GetComponent<AudioSource>();
        if (_canvasAudio == null)
        {
            Debug.LogError("Unable to find canvas Audio Source");
        }

        _transitionAnimator = GameObject.Find("TransitionManager").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SinglePlayer()
    {
        StartCoroutine(GoToNextScene());
    }

    public void TempButton()
    {
        StartCoroutine(GoToNextSceneV2());
    }

    public void MultiPlayer()
    {
        _audioSource.clip = _audioClip;
        _canvasAudio.Pause();
        StartCoroutine(playAudio());
    }

    IEnumerator playAudio()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(3.2f);
        _canvasAudio.Play();
    }

    IEnumerator GoToNextScene()
    {
        _transitionAnimator.SetTrigger("OnSceneLeave");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }

    IEnumerator GoToNextSceneV2()
    {
        _transitionAnimator.SetTrigger("OnSceneLeave");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(3);
    }
}
