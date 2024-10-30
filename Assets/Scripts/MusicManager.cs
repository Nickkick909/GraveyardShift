using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] GameObject ambientNoise;

    [SerializeField] AudioSource musicAudioSource;
    bool needToStartAmbient = true;
    Player player;

    [SerializeField] GameObject enjoyTheMusicText;
    [SerializeField] GameObject officeDoorTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicAudioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Player>();

        StartCoroutine(StopPlayingIntoMusic());
    }

    // Update is called once per frame
    void Update()
    {
        //if (musicAudioSource.isPlaying && needToStartAmbient)
        //{
        //    needToStartAmbient = false;

        //    StartCoroutine(StopPlayingIntoMusic());
            
        //}
    }

    IEnumerator StopPlayingIntoMusic()
    {
        Debug.Log("Start waiting for music");
        yield return new WaitForSeconds(105);
        Debug.Log("Stop music");
        ambientNoise.SetActive(true);
        player.SetMovementSpeed(3.5f);

        musicAudioSource.Stop();

        enjoyTheMusicText.SetActive(false);
        officeDoorTrigger.SetActive(true);
    }

    public void PlayNewSong(AudioClip newSong)
    {
        ambientNoise.SetActive(false);
        musicAudioSource.PlayOneShot(newSong);
        StartCoroutine(ReturnToAmbient());
    }

    IEnumerator ReturnToAmbient()
    {
        yield return new WaitForSeconds(musicAudioSource.clip.length);
        musicAudioSource.Stop();

        ambientNoise.SetActive(true);
        enjoyTheMusicText.SetActive(false);
        officeDoorTrigger.SetActive(true);
    }
}
