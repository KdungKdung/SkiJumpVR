using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoSystem : MonoBehaviour {

    public float spdist;
    public float spspeed;
    public TextMesh infoText;

    // Use this for initialization
    void Start () {
        infoText = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        spspeed = GameObject.Find("skiplayer").GetComponent<skiplayerMoveMent>().speed;
        spdist = GameObject.Find("skiplayer").GetComponent<skiplayerMoveMent>().distance;
       // Debug.Log(spdist);

        this.infoText.text = "거 리 : " + string.Format("{0:F2}",spdist) + " m\n속 도 : " + string.Format("{0:F2}", spspeed) + " m/s" ;  //소수점 뒷자리 포맷팅

    }
}
