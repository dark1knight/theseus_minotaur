using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SceneStatesControl : MonoBehaviour // deals with victories or defeats
{
    static public SceneStatesControl instance;
    public string state = "";
    public TMP_Text TextMesh;
    void Awake()
    {
        if (instance == null) {
            instance = this;
        }

    }
    public void victory(){
        TextMesh.text = "You Managed to escape!" + "\n" + "Press Space to advance";
        Theseus_behviour.Theseus.paused = true;
        state = "victory";
    }
    public void defeat() {
        TextMesh.text = "The Minotaur Caught you!" + "\n" + "Press R to restart";
        Theseus_behviour.Theseus.paused = true;
        state = "defeat";
    }
}
