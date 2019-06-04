using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class detectHit : MonoBehaviour
{

    public Slider healthbar;
    public GameObject ScreenFlash;

    public string opponent;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag != opponent) return;

               StartCoroutine(PlayerDamage());

    }

  
    IEnumerator PlayerDamage()
    {
        ScreenFlash.SetActive(true);
        healthbar.value -= 50;
        GlobalHealth.PlayerHealth -= 50;
        yield return new WaitForSeconds(.05f);
        ScreenFlash.SetActive(false);
    }
}
