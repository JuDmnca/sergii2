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

    public void Start()
    {
        InitAnimations();
        InitTooltips();
        InitAudio();
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
        _audio.Play();

        isCutting = true;

        handAnimator.speed = speed;
        breadAnimator.speed = speed;
        sergeAnimator.speed = speed;

        sergeAnimator.Play("cut", -1, sergeCutProgress + 0.001f);
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
}
