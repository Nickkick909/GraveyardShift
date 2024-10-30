using System.Collections;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject startGame;
    [SerializeField] Player player;
    [SerializeField] GameObject introText;


    bool introTextShown = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        introTextShown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            startGame.SetActive(false);

            if (introTextShown == false)
            {
                StartCoroutine(ShowIntroText());
            }

        }
    }

    IEnumerator ShowIntroText()
    {
        introTextShown = true;
        yield return new WaitForSeconds(0.5f);

        Debug.Log("Show intro text!");
        introText.SetActive(true);
        bool waitingForInput = true;
        while (waitingForInput)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                waitingForInput = false;

                Debug.Log("Hide intro text");

                introText.SetActive(false);

                player.blockInput = false;
                player.blockLook = false;

                yield return null;
            }

            yield return null;
        }
        
        Destroy(gameObject);
    }
}
