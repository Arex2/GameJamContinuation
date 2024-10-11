using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher1 : MonoBehaviour
{
    public int sceneToLoad;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //Switch scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }


    //USE IN PLAYER SCRIPTS

/*    //for saving important player values
public void SavePlayer()
{
    BetweenScenesValueSaver.Instance.playerColor = (Color32)GetComponent<SpriteRenderer>().color;
    BetweenScenesValueSaver.Instance.abilityDoubleJump = hasAbilityDoubleJump;
    BetweenScenesValueSaver.Instance.abilityDash = hasAbilityDash;
    BetweenScenesValueSaver.Instance.coinCount = coinCount;
    BetweenScenesValueSaver.Instance.deathCount = deathCount;
    BetweenScenesValueSaver.Instance.keyCount = smallKeyCount;
}

//for loading player values
private void LoadPlayer()
{
    GetComponent<SpriteRenderer>().color = BetweenScenesValueSaver.Instance.playerColor;
    hasAbilityDoubleJump = BetweenScenesValueSaver.Instance.abilityDoubleJump;
    hasAbilityDash = BetweenScenesValueSaver.Instance.abilityDash;
    coinCount = BetweenScenesValueSaver.Instance.coinCount;
    deathCount = BetweenScenesValueSaver.Instance.deathCount;
    smallKeyCount = BetweenScenesValueSaver.Instance.keyCount;

    if (BetweenScenesValueSaver.Instance.switchingBetweenLevelScenes && SceneManager.GetActiveScene().buildIndex == 3)
    {
        transform.position = new Vector3(42.5f, 6.5f, 0);
    }

}
*/
}
