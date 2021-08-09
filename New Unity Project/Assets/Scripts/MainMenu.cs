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
    private TransitionManager _transitionManager;
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

        _transitionManager = GameObject.Find("TransitionManager").GetComponent <TransitionManager>();
        if (_transitionManager == null)
        {
            Debug.LogError("Unable to find TransitionManager");
        }

    }

    public void SinglePlayer()
    {
        StartCoroutine(_transitionManager.TransitionToNextScene(1));
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
}
