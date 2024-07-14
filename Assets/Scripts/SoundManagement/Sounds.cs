using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] GameData gameDataSo;
    AudioSource MusicSource;
    int musicListCount;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
        MusicSource = GetComponent<AudioSource>();
        musicListCount = gameDataSo.Musics.Count;
    }
    private void Start()
    {
        PlaySomeMusic(gameDataSo.Musics[Random.Range(0,musicListCount)]);
    }
    IEnumerator PlaySomeMusic(AudioClip myMusic)
    {
        MusicSource.clip = myMusic;
        MusicSource.Play();
        yield return new WaitForSeconds(myMusic.length);
    }
}