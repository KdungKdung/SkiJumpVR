using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBoardCtrl : MonoBehaviour {

    public Cardboard cardboard;
    public Image LoadingImage;
    private Vector3 screenCenter;
    private GameObject skiPlayer;

	//Use this for initialization
	void Start () {
        screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);  // 스크링 중앙값
        skiPlayer = GameObject.Find("skiplayer");
	}	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(screenCenter); //ray를 쏴보낼 지점을 스크린의 중앙값으로
        RaycastHit hit;//충돌지점정보

        if (Physics.Raycast(ray, out hit, 500f))
        {
            if (hit.collider.gameObject.CompareTag("start")) //비교했을때 일치하면
            {
                LoadingImage.fillAmount += 1.0f / 3 * Time.deltaTime;
                if (LoadingImage.fillAmount == 1)
                {
                    hit.collider.gameObject.SetActive(false);  //오브젝트 제거
                    skiPlayer.SendMessage("changeStartCheck");
                }
            }

            if (hit.collider.gameObject.CompareTag("up"))//up과 일치하면
            {
                //LoadingImage.fillAmount += 1.0f / 3 * Time.deltaTime;
                LoadingImage.fillAmount = 0;
                skiPlayer.SendMessage("changeUpCheck");
            }
            if (hit.collider.gameObject.CompareTag("down"))//down과 일치하면
            {

                //LoadingImage.fillAmount += 1.0f / 3 * Time.deltaTime;
                LoadingImage.fillAmount = 0;
                skiPlayer.SendMessage("changedownCheck");

            }
            if (hit.collider.gameObject.CompareTag("left"))//left와 일치하면
            {

                //LoadingImage.fillAmount += 1.0f / 3 * Time.deltaTime;
                LoadingImage.fillAmount = 0;
                skiPlayer.SendMessage("changeleftCheck");

            }
            if (hit.collider.gameObject.CompareTag("right"))
            {

                //LoadingImage.fillAmount += 1.0f / 3 * Time.deltaTime;
                LoadingImage.fillAmount = 0;
                skiPlayer.SendMessage("changerightCheck");

            }

            if (hit.collider.gameObject.CompareTag("restart")) //비교했을때 일치하면
            {
                LoadingImage.fillAmount += 1.0f / 3 * Time.deltaTime;
                if (LoadingImage.fillAmount == 1)
                {
                    skiPlayer.SendMessage("restartCheck");
                }
            }


        }
        else  //controll cube를 벗어났을때
        {

            LoadingImage.fillAmount = 0;
            skiPlayer.SendMessage("resetCheck");

        }
	}
}
