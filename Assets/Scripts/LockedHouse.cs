using System.Collections;
using TMPro;
using UnityEngine;

public class LockedHouse : MonoBehaviour
{
    [SerializeField] TMP_Text lockedHouseText;

    [TextArea(5, 10)]
    [SerializeField] string houseIsLockedTextPartOne;

    [TextArea(5, 10)]
    [SerializeField] string houseIsLockedTextPartTwo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockedHouseText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            lockedHouseText.gameObject.SetActive(true);
            

            Player player = other.gameObject.GetComponent<Player>();
            player.blockInput = true;


            StartCoroutine(KnockOnDoor(player));

        }
    }

    IEnumerator KnockOnDoor(Player player)
    {

        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                lockedHouseText.text = houseIsLockedTextPartOne;
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        // Play knocking sound and then wait
        //yield return new WaitForSeconds(1f);
        yield return new WaitWhile(() => Input.GetKey(KeyCode.Space));

        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                lockedHouseText.text = houseIsLockedTextPartTwo;

                break;
                
            }

            yield return new WaitForEndOfFrame(); ;
        }

        yield return new WaitWhile(() => Input.GetKey(KeyCode.Space));

        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                lockedHouseText.gameObject.SetActive(false);
                player.blockInput = false;
                break;
            }

            yield return new WaitForEndOfFrame(); ;
        }

        lockedHouseText.text = "Press Space to Search";
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}
