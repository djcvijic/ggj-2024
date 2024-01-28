using System.Collections;
using Audio;
using UnityEngine;

public class PurrfectAudioManager : AudioManager
{
    public new static PurrfectAudioManager Instance => (PurrfectAudioManager)AudioManager.Instance;

    [Header("MainMenuMusic")]
    [SerializeField] private AudioClipSettings mainMenuStart;
    [SerializeField] private AudioClipSettings mainMenuLoop;

    [Header("LevelMusic")]
    [SerializeField] private float musicFadeDuration = 1;
    [SerializeField] private AudioClipSettings levelBase;
    [SerializeField] private AudioClipSettings[] levelStates;

    private int _currentState;
    private bool _mainMenuMusicPlaying;
    private Coroutine _startMainMenuLoopCoroutine;

    public void StartMainMenuMusic()
    {
        StopAudio(levelBase);
        foreach (var state in levelStates)
        {
            StopAudio(state);
        }

        if (!_mainMenuMusicPlaying)
        {
            _mainMenuMusicPlaying = true;
            PlayAudio(mainMenuStart);
            _startMainMenuLoopCoroutine = StartCoroutine(StartMainMenuLoop());
        }
    }

    private IEnumerator StartMainMenuLoop()
    {
        yield return new WaitForSeconds(mainMenuStart.Variants[0].length);

        StopAudio(mainMenuStart);
        PlayAudio(mainMenuLoop);
    }

    public void StartLevelMusic()
    {
        StopAudio(mainMenuStart);
        StopAudio(mainMenuLoop);
        _mainMenuMusicPlaying = false;

        if (_startMainMenuLoopCoroutine != null)
        {
            StopCoroutine(_startMainMenuLoopCoroutine);
            _startMainMenuLoopCoroutine = null;
        }

        PlayAudio(levelBase);
        foreach (var state in levelStates)
        {
            PlayAudio(state);
        }

        _currentState = 1;
    }

    public void FadeToState(int state)
    {
        if (state == _currentState) return;

        FadeAudio(levelStates[_currentState - 1], 0, musicFadeDuration);
        FadeAudio(levelStates[state - 1], 1, musicFadeDuration);
        _currentState = state;
    }
}