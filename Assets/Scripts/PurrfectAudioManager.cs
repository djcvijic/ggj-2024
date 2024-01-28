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

    private int? _currentState;

    public void StartMainMenuMusic()
    {
        StopAudio(levelBase);
        foreach (var state in levelStates)
        {
            StopAudio(state);
        }

        PlayAudio(mainMenuStart);
        StartCoroutine(MainMenuMusicLoop());
    }

    private IEnumerator MainMenuMusicLoop()
    {
        yield return new WaitForSeconds(mainMenuStart.Variants[0].length);

        StopAudio(mainMenuStart);
        PlayAudio(mainMenuLoop);
    }

    public void StartLevelMusic()
    {
        StopAudio(mainMenuStart);
        StopAudio(mainMenuLoop);

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

        if (_currentState.HasValue) FadeAudio(levelStates[_currentState.Value - 1], 0, musicFadeDuration);
        FadeAudio(levelStates[state - 1], 1, musicFadeDuration);
        _currentState = state;
    }
}