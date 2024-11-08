using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGame : MonoBehaviour
{
    [SerializeField] TriggerTextPrompts storyTextPrompts;
    [SerializeField] MusicManager musicManager;
    [SerializeField] AudioClip endOfGameMusic;
    [SerializeField] GameObject titleScreen;
    [SerializeField] AudioSource ghostSFX;
    [SerializeField] AudioSource ghostHeartbeat;
    [SerializeField] AudioSource ambientSound;

    Player player;
    bool startedCoroutine = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (storyTextPrompts.finishedTextPrompts)
        {
            
            //musicManager.gameObject.SetActive(true);
            
            titleScreen.SetActive(true);
            if (!startedCoroutine)
            {
                startedCoroutine = true;
                StartCoroutine(WaitToRestartGame());
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            ghostSFX.enabled = false;
            ghostHeartbeat.enabled = false;
            ambientSound.enabled = false;

            musicManager.PlayNewSong(endOfGameMusic);

            if (!player)
            {
                player = FindAnyObjectByType<Player>();
            }

            player.blockInput = true;
            player.blockLook = true;

            
        }
    }

    IEnumerator WaitToRestartGame()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(0);
    }
}
