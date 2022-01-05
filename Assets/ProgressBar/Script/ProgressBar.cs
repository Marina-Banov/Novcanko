using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]

public class ProgressBar : MonoBehaviour
{
    public Canvas canvas;

    [Header("Title Setting")]
    public string Title;
    public Color TitleColor;
    public Font TitleFont;
    public int TitleFontSize = 10;

    [Header("Bar Setting")]
    public Color BarColor;   
    public Color BarBackGroundColor;
    public Sprite BarBackGroundSprite;
    [Range(1f, 100f)]
    private int Alert = 50; // koliko treba platit pa da se taka zvuk
    public Color BarAlertColor;
    public Color BarCorrectColor;

    [Header("Sound Alert")]
    public AudioClip sound;
    public bool repeat = false;
    public float RepeatRate = 1f;

    private Image bar, barBackground;
    private float nextPlay;
    private AudioSource audiosource;
    private Text txtTitle;
    private float barValue;
    public float BarValue
    {
        get { return barValue; }

        set
        {
            //value = Mathf.Clamp(value, 0, 420);
            barValue = value;
            UpdateValue(barValue);

        }
    }

        

    private void Awake()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        barBackground = GetComponent<Image>();
        txtTitle = transform.Find("Text").GetComponent<Text>();
        barBackground = transform.Find("BarBackground").GetComponent<Image>();
        audiosource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        txtTitle.text = Title;
        txtTitle.color = TitleColor;
        txtTitle.font = TitleFont;
        txtTitle.fontSize = TitleFontSize;

        bar.color = BarColor;
        barBackground.color = BarBackGroundColor; 
        barBackground.sprite = BarBackGroundSprite;

        UpdateValue(barValue);


    }

    void UpdateValue(float val)
    {

        //Debug.Log(canvas.GetComponent<LevelManagement>().getGivenNumver());
        float amount = Mathf.Round(canvas.GetComponent<LevelManagement>().getAmountNumber() * 100f) / 100f;
        val = Mathf.Round(val * 100f) / 100f;
        bar.fillAmount = (val / (amount * 2));
        txtTitle.text = Title + " " + val.ToString("F2") + "kn / " + amount.ToString("F2") + "kn";

        float diff = Mathf.Round((amount - val) * 100f) / 100f;

        //Debug.Log(diff);
        if (diff == 0.00)
        {
            bar.color = BarCorrectColor;
        }
        else if (amount > val)
        {
            bar.color = BarAlertColor;
        }
        else
        {
            bar.color = BarColor;
        }

    }


    private void Update()
    {
        if (!Application.isPlaying)
        {           
            UpdateValue(50);
            txtTitle.color = TitleColor;
            txtTitle.font = TitleFont;
            txtTitle.fontSize = TitleFontSize;

            bar.color = BarColor;
            barBackground.color = BarBackGroundColor;

            barBackground.sprite = BarBackGroundSprite;           
        }
        else
        {
            if (Alert >= barValue && Time.time > nextPlay)
            {
                //zvuk se stalno ponavlja iz nekog razloga
                //nextPlay = Time.time + RepeatRate; 
                //audiosource.PlayOneShot(sound);
            }
        }
    }

}
