using System.Collections;
using TMPro;
using UnityEngine;

public class StartOfKillerSection : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TMP_Text storyText;

    [TextArea(5, 10)]
    [SerializeField] string textPrompt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            storyText.text = textPrompt;
            storyText.gameObject.SetActive(true);
            player.DisableFlashLight();
            StartCoroutine(RemoveTextAfterSeconds());
        }
    }

    IEnumerator RemoveTextAfterSeconds()
    {
        yield return new WaitForSeconds(3);
        storyText.text = "";
        storyText.gameObject.SetActive(false);
    }
}
