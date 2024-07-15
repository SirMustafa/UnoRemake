using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sounds : MonoBehaviour
{
    [SerializeField] GameData gameDataSo;
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SfxSource;
    public static Sounds Soundsinstance;
    private AudioClip chosenMusic;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Soundsinstance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(1);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;
        if (sceneIndex == 1 || sceneIndex == 2)
        {
            PlayRandomMusic();
        }
    }

    public void FinishPanel()
    {
        PlaySpecificMusic(gameDataSo.Musics[^1]);
    }

    private void PlayRandomMusic()
    {
        StopAllCoroutines();
        chosenMusic = gameDataSo.Musics[Random.Range(0, gameDataSo.Musics.Count)];
        StartCoroutine(PlayMusicWithFade(chosenMusic));
    }

    private void PlaySpecificMusic(AudioClip clip)
    {
        StopAllCoroutines();
        StartCoroutine(PlayMusicWithFade(clip));
    }

    public void PlaySoundEffect(GameData.SoundEffects effect)
    {
        AudioClip clip = gameDataSo.Effects[(int)effect];
        SfxSource.PlayOneShot(clip);
    }

    IEnumerator PlayMusicWithFade(AudioClip clip)
    {
        yield return StartCoroutine(FadeOutMusic());
        MusicSource.clip = clip;
        MusicSource.Play();
        yield return StartCoroutine(FadeInMusic());
        yield return new WaitForSeconds(clip.length);
        PlayRandomMusic();
    }

    IEnumerator FadeOutMusic()
    {
        MusicSource.DOFade(0, 1);
        yield return new WaitForSeconds(1);
    }

    IEnumerator FadeInMusic()
    {
        MusicSource.DOFade(1, 1);
        yield return new WaitForSeconds(1);
    }
}