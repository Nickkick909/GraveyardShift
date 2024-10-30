using UnityEngine;

public class EscapedZombie : MonoBehaviour
{
    [SerializeField] GameObject zombie;
    [SerializeField] Collider deathBox;
    [SerializeField] MusicManager musicManager;
    [SerializeField] AudioClip newSong;
    [SerializeField] AudioSource ghostAppearAudioSource;
    [SerializeField] AudioClip ghostJumpScare;

    private void OnTriggerEnter(Collider other)
    {
        ghostAppearAudioSource.PlayOneShot(ghostJumpScare);

        musicManager.PlayNewSong(newSong);
        zombie.GetComponent<ZombieAnimation>().stopFollowingPlayer = true;
        zombie.GetComponent<Animator>().SetBool("Dead", true);
        zombie.GetComponentInChildren<AudioSource>().enabled = false;
        deathBox.enabled = false;
    }
}
