using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : Task
{
    public GameObject hand;
    public Animator handAnimator;

    public GameObject bread;
    public Animator breadAnimator;

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
    private int stepsTotal = 5;
    private bool ended = false;

    public void Start()
    {
        InitTooltips();
        InitAudio();
        StartCoroutine(CancelBerry());
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

            // Translate hand when the animation is finished
            if(handAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                MoveHand();
            }

            // Animate on click when the animation is not already playing
            if(Input.GetButtonDown("Fire1") && (handAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 || !handAnimator.GetBool("isCutting")))
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

        handAnimator.SetBool("isCutting", true);
        handAnimator.speed = speed;
        handAnimator.Play("cut", -1, 0f);

        breadAnimator.SetBool("isCutting", true);
        breadAnimator.speed = speed;
        breadAnimator.Play("cut", -1, 0f);
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

    void MoveHand() {
        if(handAnimator.GetBool("isCutting"))
        {
            hand.transform.Translate(0, 0, 0.1f);

            handAnimator.SetBool("isCutting", false);
            breadAnimator.SetBool("isCutting", false);

            step += 1;
        }
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
