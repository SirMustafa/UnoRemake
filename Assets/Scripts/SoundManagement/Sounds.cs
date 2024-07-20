using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sounds : MonoBehaviour
{
    public static Sounds Soundsinstance;

    [SerializeField] private GameData _gameDataSo;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    private AudioClip _chosenMusic;
    public float MusicVolume { get; private set; }
    public float SfxVolume { get; private set; }


    private void Awake()
    {
        if (Soundsinstance == null)
        {
            Soundsinstance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        MusicVolume = _musicSource.volume;
        SfxVolume = _sfxSource.volume;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;
        if (sceneIndex == 1 || sceneIndex == 2)
        {
            PlayRandomMusic();
        }
    }

    public void FinishPanel()
    {
        PlaySpecificMusic(_gameDataSo.Musics[^1]);
    }

    private void PlayRandomMusic()
    {
        StopAllCoroutines();
        _chosenMusic = _gameDataSo.Musics[Random.Range(0, _gameDataSo.Musics.Count - 1)];
        StartCoroutine(PlayMusicWithFade(_chosenMusic));
    }

    private void PlaySpecificMusic(AudioClip clip)
    {
        StopAllCoroutines();
        StartCoroutine(PlayMusicWithFade(clip));
    }

    public void PlaySoundEffect(GameData.SoundEffects effect)
    {
        AudioClip clip = _gameDataSo.Effects[(int)effect];
        _sfxSource.PlayOneShot(clip);
    }
    public void SetMusicVolume(float amount)
    {
        MusicVolume = amount;
        _musicSource.DOFade(amount, 1f);
    }
    public void SetSfxVolume(float amount)
    {
        SfxVolume = amount;
        _sfxSource.DOFade(amount, 1f);
    }

    private IEnumerator PlayMusicWithFade(AudioClip clip)
    {
        yield return StartCoroutine(FadeOutMusic());
        _musicSource.clip = clip;
        _musicSource.Play();
        yield return StartCoroutine(FadeInMusic());
        yield return new WaitForSeconds(clip.length);
        PlayRandomMusic();
    }

    private IEnumerator FadeOutMusic()
    {
        _musicSource.DOFade(0, 1);
        yield return new WaitForSeconds(1);
    }

    private IEnumerator FadeInMusic()
    {
        _musicSource.DOFade(1, 1);
        yield return new WaitForSeconds(1);
    }
}