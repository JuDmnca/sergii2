using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : Task
{
    public Animator handAnimator;
    public Animator breadAnimator;
    public Animator sergeAnimator;
    private float sergeCutProgress;

    public RectTransform cursorTransform;
    public RectTransform sliderTransform;
    private float time = 3f * Mathf.PI / 2f;
    private float sliderPosition = -1;

    public GameObject cutTooltip;
    public RectTransform cutTooltipTransform;
    public RectTransform berryTransform;
    private float cutTooltipDest;
    private float initCutTooltipPos;
    private float berryDest;

    private AudioSource _audio;
    public float speed = 2.0f;
    public float multiplier = 1.0f;

    private int step = 0;
    private int stepsTotal = 4;
    private bool ended = false;
    private bool isCutting = false;

    public GameObject bloodStep1_1;
    public GameObject bloodStep1_2;
    public GameObject bloodStep2_1;
    public GameObject bloodStep2_2;
    public GameObject bloodStep3_1;
    public GameObject bloodStep3_2;
    public GameObject bloodStep4_1;
    public GameObject bloodStep4_2;
    private GameObject[][] bloodArray;

    public void Start()
    {
        InitAnimations();
        InitTooltips();
        InitAudio();
        InitBlood();
    }

    void InitAnimations() {
        KitchenController.Instance.OnCutOver += HandleCutOver;

        handAnimator.speed = 0;
        breadAnimator.speed = 0;
        sergeAnimator.speed = 0;
    }
    void InitAudio() {
        _audio = GetComponent<AudioSource>();
    }

    void InitTooltips() {
        cutTooltipDest = cutTooltipTransform.anchoredPosition.x;
        initCutTooltipPos = cutTooltipDest;
        berryDest = berryTransform.anchoredPosition.x;
    }

    void InitBlood()
    {
        bloodArray = new GameObject[stepsTotal][];
        bloodArray[0] = new GameObject[] { bloodStep1_1, bloodStep1_2 };
        bloodArray[1] = new GameObject[] { bloodStep2_1, bloodStep2_2 };
        bloodArray[2] = new GameObject[] { bloodStep3_1, bloodStep3_2 };
        bloodArray[3] = new GameObject[] { bloodStep4_1, bloodStep4_2 };

        HideAllBlood();

        GameController.Instance.OnNoBerry += ShowCurrentBlood;
        GameController.Instance.OnEatBerry += HideAllBlood;

        SceneController.Instance.OnCloseScene += RemoveListeners;
    }

    public void Update()
    {
        if(step < stepsTotal)
        {
            // AnimateSlider
            MoveSlider();

            // Animate tooltips
            MoveTooltips();

            // Animate on click when the animation is not already playing
            if(Input.GetButtonDown("Fire1") && (!isCutting))
            {
                CutBread();

            }
        } else if (step == stepsTotal && ended == false) {
            EndTask();
        }
    }

    void CutBread() {
        cutTooltipDest = cutTooltipTransform.rect.width;
        berryDest = initCutTooltipPos;

        speed = 3f - (Mathf.Abs(sliderPosition) * 2f);
        _audio.pitch = speed * multiplier;
        StartCoroutine(PlayAudio());

        isCutting = true;

        handAnimator.speed = speed;
        breadAnimator.speed = speed;
        sergeAnimator.speed = speed;

        sergeAnimator.Play("cut", -1, sergeCutProgress + 0.001f);
    }

    IEnumerator PlayAudio() {
        yield return new WaitForSeconds(0.5f);
        _audio.Play();
    }

    void HandleCutOver() {
        sergeAnimator.speed = 0;
        sergeCutProgress = sergeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        step += 1;
        isCutting = false;

        handAnimator.speed = 0;
        breadAnimator.speed = 0;

        if (step == 1) {
            GameController.Instance.CancelBerry();
        }
        if (!GameController.Instance.IsOnBerry()) {
            ShowCurrentBlood();
        }
    }

    void MoveTooltips() {
        float speedTooltips = 5f;

        float berryX = berryTransform.anchoredPosition.x;
        if (berryX - berryDest < 0) {
            berryX += speedTooltips;
        }
        berryTransform.anchoredPosition = new Vector2(berryX, berryTransform.anchoredPosition.y);

        float cutX = cutTooltipTransform.anchoredPosition.x;
        if (cutTooltipDest != initCutTooltipPos && cutX - cutTooltipDest >= 0) {
            cutTooltip.SetActive(false);
        } else if (cutX - cutTooltipDest < 0) {
            cutX += speedTooltips;
        }
        cutTooltipTransform.anchoredPosition = new Vector2(cutX, cutTooltipTransform.anchoredPosition.y);
    }

    void MoveSlider() {
        time += 0.01f;

        float sliderWidth = sliderTransform.rect.width;
        float cursorWidth = cursorTransform.rect.width;
        sliderPosition =  Mathf.Sin(time);
        float x = sliderPosition * ((sliderWidth / 2f) - (cursorWidth / 2f));

        cursorTransform.anchoredPosition = new Vector2(x, 0);
    }

    void EndTask() {
        // Switch scene
        GameController.Instance.FinishTask();
        ended = true;
        SceneController.Instance.TransitionScene(new string[] {"Kitchen"}, new string[] {"Sergii"});
    }

    void ShowCurrentBlood()
    {
        for (int i = 0; i < step; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                bloodArray[i][j].SetActive(true);
            }
        }
    }

    void HideAllBlood()
    {
        for (int i = 0; i < stepsTotal; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                bloodArray[i][j].SetActive(false);
            }
        }
    }

    void RemoveListeners(string scene) {
        GameController.Instance.OnNoBerry -= ShowCurrentBlood;
        GameController.Instance.OnEatBerry -= HideAllBlood;
    }
}
