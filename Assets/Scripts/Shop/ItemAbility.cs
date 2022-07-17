using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAbility : MonoBehaviour
{
    public void ActivateItem(string name) {
        switch (name) {
            case "blood1":
            case "blood2":
            case "blood3":
            case "blood4":
            case "blood5":
            case "blood6":
            case "blood7":
            case "blood8":
            case "blood9":
            case "bloodc":
                Blood(name.Substring(name.Length - 1));
                break;
            case "gold1":
            case "gold2":
            case "gold3":
            case "gold4":
            case "gold5":
            case "gold6":
            case "gold7":
            case "gold8":
            case "gold9":
            case "goldc":
                Gold(name.Substring(name.Length - 1));
                break;
            case "ammo":
                Ammo();
                break;
            case "boots":
                Boots();
                break;
            case "cookieJar":
                CookieJar();
                break;
            case "diceStack":
                DiceStack();
                break;
        }
    }
    private void DiceStack() {
        
    }
    private void Boots() {

    }
    private void Ammo() {

    }
    private void CookieJar() {

    }
    private void Gold(string type) {
        int index;
        if (type.Equals("c")) index = 9;
        else index = int.Parse(type) - 1;
        ReplaceSide.rp.ReplaceStart(index, false);
    }
    private void Blood(string type) {
        int index;
        if (type.Equals("c")) index = 9;
        else index = int.Parse(type) - 1;
        ReplaceSide.rp.ReplaceStart(index, true);
    }
}