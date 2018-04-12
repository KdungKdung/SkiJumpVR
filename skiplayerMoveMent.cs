using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class skiplayerMoveMent : MonoBehaviour {

 
    int startCheck = 0;
    int upCheck = 0;
    int downCheck = 0;
    int leftCheck = 0;
    int rightCheck = 0;

    int penalty = 0;  //점프대의 가속부분에서 패널티를 주는 전역변수

    private GameObject skiPlayer;  //플레이어 오브젝트
    private GameObject End;        //점프대 끝 오브젝트      
    private GameObject ski;        //스키 오브젝트

    private GameObject plane;        //스키 오브젝트

    public AudioSource breath;
    public AudioSource planesound;      //pubilc으로 선언시 프리팹?생성댐 신기ㅋ
    public AudioSource jump;      //pubilc으로 선언시 프리팹?생성댐 신기ㅋ
    public AudioSource end;      //pubilc으로 선언시 프리팹?생성댐 신기ㅋ



    public float distance = 0;             //객체와 점프대 거리
    public float speed = 0;                //객체 스피드         //infoSystem에서 접근하기위해 pubilc(기본값은 private)




    float jumpforce = 0;            //점프시 힘 계산

    // Use this for initialization
    void Start () {

    }

// Update is called once per frame
void Update () {

        speed = GetComponent<Rigidbody>().velocity.magnitude;

       // GameObject.Find("endlogo").transform.Translate(new Vector3(0, -55, 0) * Time.deltaTime);
        if (startCheck == 1) //시작할때부터 실행되는 함수
        {
            plane = GameObject.Find("plane");
            plane.transform.Translate(new Vector3(0, 20, 100)*Time.deltaTime);

            penalty += 1;  //기본적으로 지체시간마다 패널티는 +1씩들어간다
            GetComponent<Rigidbody>().AddForce(new Vector3(0, -10, 17)/5, ForceMode.Acceleration);
            //transform.Translate(new Vector3(0, gravity, 17) * Time.deltaTime/300 * addSpeed);

            //controll 부분

            if (downCheck == 1)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, -3, 4)/50, ForceMode.Impulse);
                penalty -= 1;
            }
            if (leftCheck == 1)
            {
              //  transform.Translate(new Vector3(-3, 0, 0) * Time.deltaTime);
                GetComponent<Rigidbody>().AddForce(new Vector3(-3, -1, 1)/50, ForceMode.Impulse);
                penalty += 1;
            }
            if (rightCheck == 1)
            {
               // transform.Translate(new Vector3(3, 0, 0) * Time.deltaTime);
                GetComponent<Rigidbody>().AddForce(new Vector3(3, -1, 1)/50, ForceMode.Impulse);
                penalty += 1;
            }


        }

        if (startCheck == 2) //점프이후부터 실행되는 함수
        {
            plane = GameObject.Find("plane");
            plane.transform.Translate(new Vector3(0, 55, 200) * Time.deltaTime);



            skiPlayer = GameObject.Find("skiplayer");
            End = GameObject.Find("End");
            distance = skiPlayer.transform.position.z - End.transform.position.z;  //점프이후 부터 거리계산 시작

            //controll 부분
            if (upCheck == 1)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 3, 5) / 45, ForceMode.Impulse);
            }
            if (downCheck == 1)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, -3, -1) / 45, ForceMode.Impulse);
            }
            if (leftCheck == 1)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(-8, 0, -3) / 45, ForceMode.Impulse);
            }
            if (rightCheck == 1)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(8, 0, -3) / 45, ForceMode.Impulse);
            }
 
        }


        if (startCheck == 3) //점프이후 멈추면 실행되는 부분
        {
            plane = GameObject.Find("plane");
            plane.transform.Translate(new Vector3(0, -60, 200) * Time.deltaTime);

            if (speed == 0)     //캐릭터가 멈추면
            {
                GameObject.Find("skiplayer").transform.FindChild("restartbox").gameObject.SetActive(true);  //최상위 오브젝트 -> 하위오브젝트로해야 리소스덜먹음 참고
            }
        }


    }

    void changeStartCheck() //chanrStartCheck메세지를 받았을 때
    {
        startCheck = 1;
        planesound.Play();
        breath.GetComponent<AudioSource>().volume = 0;      //숨소리끄기
    }
    void changeUpCheck()
    {
        upCheck = 1;
        downCheck = 0;
        leftCheck = 0;
        rightCheck = 0;
    }
    void changedownCheck()
    {
        upCheck = 0;
        downCheck = 1;
        leftCheck = 0;
        rightCheck = 0;
    }
    void changeleftCheck()
    {
        upCheck = 0;
        downCheck = 0;
        leftCheck = 1;
        rightCheck = 0;
    }
    void changerightCheck()
    {
        upCheck = 0;
        downCheck = 0;
        leftCheck = 0;
        rightCheck = 1;
    }

    void resetCheck()  //모든 controll변수 초기화
    {
        upCheck = 0;
        downCheck = 0;
        leftCheck = 0;
        rightCheck = 0;
    }


   void restartCheck()  //게임재시작함수
    {
        SceneManager.LoadScene("mainscene"); //재시작함수
    }


    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "End")
        {
            jump.Play();
            startCheck = 2;

            jumpforce = speed - (penalty / 40);
            //Debug.Log(jumpforce);
            if (jumpforce <= 0)
            {
                jumpforce = 5;
            }
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 25, jumpforce) / 3, ForceMode.VelocityChange);  //패널티를 적용하여 점프동작을 해준다


            GameObject.Find("basecube").gameObject.SetActive(false);           //제거
            GameObject.Find("basecubeMain").gameObject.SetActive(false);       //제거



           
        }
        if (coll.collider.tag == "Ground")//땅에 닿았을때 점수 출력등
        {

            if(startCheck == 2) {
                jump.Play();
                startCheck = 3;
                end.Play();
                ski = GameObject.Find("ski");
                ski.transform.Rotate(-50, 0, 0);
                //  Debug.Log(distance-100);
                GameObject.Find("skiplayer").transform.FindChild("up").gameObject.SetActive(false);
                GameObject.Find("skiplayer").transform.FindChild("down").gameObject.SetActive(false);
                GameObject.Find("skiplayer").transform.FindChild("left").gameObject.SetActive(false);
                GameObject.Find("skiplayer").transform.FindChild("right").gameObject.SetActive(false);

                

            }
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, speed/4), ForceMode.VelocityChange); //관성에 의한 밀림구현
            
        }

        if (coll.collider.tag == "exground")// 닿았을때 점수 출력등
        {
            if (startCheck == 2)
            {
                end.Play();
                jump.Play();
                startCheck = 3;
                ski = GameObject.Find("ski");
                ski.transform.Rotate(-23, 0, 0);
                //  Debug.Log(distance-100);
                GameObject.Find("skiplayer").transform.FindChild("up").gameObject.SetActive(false);
                GameObject.Find("skiplayer").transform.FindChild("down").gameObject.SetActive(false);
                GameObject.Find("skiplayer").transform.FindChild("left").gameObject.SetActive(false);
                GameObject.Find("skiplayer").transform.FindChild("right").gameObject.SetActive(false);


                GetComponent<Rigidbody>().AddForce(new Vector3(0, 2, speed/5), ForceMode.VelocityChange); //관성에 의한 밀림구현
                
               
            }
        }

        if (coll.collider.tag == "endcube")// 닿았을때 점수 출력등
        {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 4, -(speed/3)), ForceMode.VelocityChange); //관성에 의한 밀림구현         
        }
    }


}
