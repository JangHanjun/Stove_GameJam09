using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class CameraManager : MonoBehaviour
{
    // -6.5 -14.55
    // 8
    // 14.5

    public bool isInGame;
    public bool startGame;
    [SerializeField] CinemachineVirtualCamera InGameCam;
    [SerializeField] CinemachineVirtualCamera TimelineCam;

    [Header("UI 관련")]
    public bool tabbed;
    [SerializeField] GameObject InGameUI;
    [SerializeField] GameObject TimeLineUI;
    [SerializeField] TextMeshProUGUI tabCountUI;
    void Awake()
    {
        isInGame = false;
        startGame = false;
        tabbed = false;
        TimeLineUI.SetActive(false);
    }

    # region 시작시 스테이지 보고 플레이어가 타임라인 조작하게 하기


    private void Start() 
    {
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(2f);

        // camera move

        InGameUI.SetActive(false);
        TimelineCam.gameObject.SetActive(true);
        InGameCam.gameObject.SetActive(false);

        startGame = true;
        yield return new WaitForSeconds(0.25f);
        TimeLineUI.SetActive(true);
    }



    #endregion





    public void ChangeCam()
    {
        if(isInGame && !tabbed)
        {
            isInGame = false;
            tabbed = true;

            InGameUI.SetActive(false);

            tabCountUI.text = "You Can't Tab!";
            TimeLineUI.SetActive(true);
            TimelineCam.gameObject.SetActive(true);
            InGameCam.gameObject.SetActive(false);


            //ShakeCamera(0f, 0f);
        }
        else
        {
            isInGame = true;

            InGameUI.SetActive(true);
            TimeLineUI.SetActive(false);
            InGameCam.gameObject.SetActive(true);
            TimelineCam.gameObject.SetActive(false);


            //ShakeCamera(0f, 0f);
        }
    }

    private void Update() 
    {

        if(Input.GetKeyDown(KeyCode.Tab) && startGame)
        {
            ChangeCam();
        }
        
    }
}


/*



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public bool isInGame;
    float shakeTimer;
    float totalShakeTimer;
    float startingIntensity;
    [SerializeField] CinemachineVirtualCamera InGameCam;
    [SerializeField] CinemachineVirtualCamera TimelineCam;
    public CinemachineConfiner InGameConfiner;
    public CinemachineConfiner TimeLineConfiner;
    void Awake()
    {
        instance = this;
        isInGame = true;
    }

    public void ChangeCam(Collider2D border)
    {
        if(instance.isInGame)
        {
            instance.TimeLineConfiner.m_BoundingShape2D = border;
            instance.isInGame = false;
            instance.TimelineCam.gameObject.SetActive(true);
            instance.InGameCam.gameObject.SetActive(false);


            //instance.ShakeCamera(0f, 0f);
        }
        else
        {
            instance.InGameConfiner.m_BoundingShape2D = border;
            instance.isInGame = true;
            instance.InGameCam.gameObject.SetActive(true);
            instance.TimelineCam.gameObject.SetActive(false);


            //instance.ShakeCamera(0f, 0f);
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin multiChannelPerlin;
        if(isInGame)
        {
            multiChannelPerlin = InGameCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        else 
        {
            multiChannelPerlin = TimelineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        
        multiChannelPerlin.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        totalShakeTimer = time;
        shakeTimer = time;
    }

    private void Update() 
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f)
            {   
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
                if(isInGame)
                {
                    cinemachineBasicMultiChannelPerlin = InGameCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                }
                else
                {
                    cinemachineBasicMultiChannelPerlin = TimelineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                }
                
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 
                    Mathf.Lerp(startingIntensity, 0f, (1 - (shakeTimer / totalShakeTimer)));
            }
        }
    }
}


*/