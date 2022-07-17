using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController GC { get; private set; } //sets this script as a singleton, only one of this script must exist and it can be accessed anywere
    [SerializeField]
    private int roundCount;
    [SerializeField]
    private float startDelay;
    [SerializeField]
    GameObject camera3D, camera2D;
    [SerializeField]
    GameObject[] shop, arena;
    [SerializeField]
    Animator cameraAnimation;
    private bool isShop;
    private void Start() {
        if (GC != null && GC != this) Destroy(this);
        else GC = this;

        camera3D.SetActive(true);
        StartCoroutine(StartGameAfterDelay());
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
        cameraAnimation.SetBool("isTransitionOut", false);
    }
}
