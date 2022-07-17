using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplaceSide : MonoBehaviour
{
    public static ReplaceSide rp { get; private set; }

    [SerializeField]
    GameObject[] allGoldFaces, allBloodFaces;
    [SerializeField]
    Sprite[] allGoldImages, allBloodImages;
    [SerializeField]
    GameObject[] dieGoldFaces, dieBloodFaces; //0 top, 1 bottom, 2 left, 3 right, 4 front, 5 back
    [SerializeField]
    GameObject goldUI, bloodUI, canvas;

    int toReplace;
    bool isBlood;
    private void Start() {
        if (rp != null && rp != this) Destroy(this);
        else rp = this;
    }
    public void ReplaceStart(int replace, bool isBlood) {
        canvas.SetActive(true);
        toReplace = replace;
        this.isBlood = isBlood;
        goldUI.SetActive(!isBlood);
        bloodUI.SetActive(isBlood);
    }
    public void ReplaceMe(int index) {
        if (isBlood) {
            Transform p = dieBloodFaces[index].transform.parent;
            GameObject t = Instantiate(allBloodFaces[toReplace], p);
            Destroy(dieBloodFaces[index]);
            dieBloodFaces[index] = t;
            dieBloodFaces[index].name = t.name.Substring(0, t.name.Length - 7);
        } else {
            Transform p = dieGoldFaces[index].transform.parent;
            GameObject t = Instantiate(allGoldFaces[toReplace], p);
            Destroy(dieGoldFaces[index]);
            dieGoldFaces[index] = t;
            dieGoldFaces[index].name = t.name.Substring(0, t.name.Length - 7);
        }
        canvas.SetActive(false);
    }
    public void ReplaceMyImage(Image image) {
        if (isBlood) {
            image.sprite = allBloodImages[toReplace];
        } else {
            image.sprite = allGoldImages[toReplace];

        }
    }
}
