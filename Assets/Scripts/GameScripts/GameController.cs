using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController GC { get; private set; } //sets this script as a singleton, only one of this script must exist and it can be accessed anywere
    [SerializeField]
    private int roundCount;
    [SerializeField]
    private float startDelay;
    [SerializeField]
    GameObject camera3D, camera2D, gameOverCanvas;
    [SerializeField]
    GameObject[] shop, arena;
    [SerializeField]
    Animator cameraAnimation;
    [SerializeField]
    ShopItemSpawner[] items;
    [SerializeField]
    float fadeInSpeed;
    private bool isShop;
    private bool setOnce = false;
    private void Start() {
        if (GC != null && GC != this) Destroy(this);
        else GC = this;

        camera3D.SetActive(true);
        StartCoroutine(StartGameAfterDelay());
    }
    private void Update() {
        if (GUI.gui.GetHealth() <= 0 && !setOnce) {
            GUI.gui.SetHealth(0);
            setOnce = true;
            StartCoroutine(GameOver());
        }
    }
    IEnumerator GameOver() {
        PlayerMovement.pm.dead();
        CanvasGroup cv = gameOverCanvas.GetComponent<CanvasGroup>();
        cv.alpha = 0;
        gameOverCanvas.SetActive(true);
        while (cv.alpha < 1) {
            cv.alpha += fadeInSpeed * Time.deltaTime;
            yield return null;
        }

    }
    IEnumerator StartGameAfterDelay() {
        yield return new WaitForSeconds(startDelay);
        StartNextRound();
    }
    public void StartNextRound() {
        roundCount++;
        RoundLogic.rl.startRound(roundCount);
    }
    public void TransitionStart() { //starts the transtion to/from shop from another script
        cameraAnimation.SetBool("isTransitionOut", true);
        StartCoroutine(Transition());
    }
    private IEnumerator Transition() {//animates the transition
        yield return new WaitForSeconds(cameraAnimation.GetCurrentAnimatorStateInfo(0).length); //curently uses camera moving animation, its jarring and could be somethign else
        foreach (GameObject c in shop) c.SetActive(!isShop); //switches between shop and arena
        foreach (GameObject c in arena) c.SetActive(isShop);
        isShop = !isShop;
        if (!isShop) StartNextRound();
        else SpawnItems();
        cameraAnimation.SetBool("isTransitionOut", false);
    }
    private void SpawnItems() {
        foreach (ShopItemSpawner c in items) {
            c.gameObject.SetActive(true);
            c.SpawnItem();
        }
    }
    public void Refresh() {
        if (GUI.gui.GetMoney() >= 10) {
            GUI.gui.AddMoney(-10);
            foreach (ShopItemSpawner c in items) {
                c.SpawnItem();
            }
        }
    }
}
