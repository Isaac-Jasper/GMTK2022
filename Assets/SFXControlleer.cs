using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControlleer : MonoBehaviour
{
    public static SFXControlleer sfx { get; private set; }
    [SerializeField] private AudioSource[] SFXSource;

    private void Start() {
        if (sfx != null && sfx != this) Destroy(this); //makes sure only one isntance of this script exists
        else sfx = this;
    }
    public void PlaySound(string name) {
        foreach (AudioSource c in SFXSource) {
            if (c.name.Equals(name)) {
                c.Play();
            }
        }
    }
}
