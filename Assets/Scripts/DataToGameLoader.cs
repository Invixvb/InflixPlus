using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataToGameLoader : MonoBehaviour
{
    #region Public variables
    [Header("Background Image")]
    public Image backgroundImage;
    
    [Header("Buttons")]
    public Button previousButton;
    public Button nextButton;
    
    [Header("Header Text")]
    public TextMeshProUGUI headerText;

    [Header("Local Time Text")]
    public TextMeshProUGUI localTimeText;
    
    [Header("Footer")]
    public Image footerBackgroundImage;
    public TextMeshProUGUI footerText;
    
    [Header("Timer")]
    public Image timerBackgroundImage;
    public TextMeshProUGUI timerText;

    [Header("Header Image")]
    public Image headerImage;

    [Header("Audio")] 
    public AudioSource audioFragment;
    #endregion
    
    private int _currentSlideIndex;
    
    private readonly List<SO_Slide> _currentThemeLevelList = new();
    
    private void Start()
    {
        StartCoroutine(LocalTimerUpdate());
        LoadDataFromSo();
    }

    private IEnumerator LocalTimerUpdate()
    {
        localTimeText.text = DateTime.Now.ToString("HH:mm");

        yield return new WaitForSeconds(2f);
        
        StartCoroutine(LocalTimerUpdate());
    }

    private void LoadDataFromSo()
    {
        var levelSetting = StaticConfig.PublicConfig.levelSetting;
        var themeList = StaticConfig.PublicConfig.themeList;
        var themeSelectedIndex = StaticConfig.PublicConfig.themeSelectedIndex;

        var selectedTheme = themeList[themeSelectedIndex];

        switch (levelSetting)
        {
            case 0:
                _currentThemeLevelList.AddRange(selectedTheme.levelOneSlides);
                break;
            case 1:
                _currentThemeLevelList.AddRange(selectedTheme.levelTwoSlides);
                break;
            default:
                Debug.LogError("Level index not found");
                break;
        }
        
        var currentSlide = _currentThemeLevelList[_currentSlideIndex];
        
        backgroundImage.sprite = currentSlide.backgroundImage;
        headerImage.sprite = currentSlide.headerImage;
        footerBackgroundImage.sprite = currentSlide.footerBackgroundImage;
        timerBackgroundImage.sprite = currentSlide.timerBackgroundImage;
        previousButton.GetComponent<Image>().sprite = currentSlide.previousButtonImage;
        nextButton.GetComponent<Image>().sprite = currentSlide.nextButtonImage;

        headerText.text = currentSlide.header;
        footerText.text = currentSlide.footer;

        //Null checks
        backgroundImage.enabled = currentSlide.backgroundImage;
        
        if (!currentSlide.previousButtonImage) 
            Debug.LogError($"No PreviousButtonImage selected");
        
        if (!currentSlide.nextButtonImage) 
            Debug.LogError($"No NextButtonImage selected");
        
        headerText.enabled = currentSlide.header != null;
        
        footerBackgroundImage.enabled = currentSlide.footerBackgroundImage;
        
        footerText.enabled = currentSlide.footer != null;
        
        timerBackgroundImage.enabled = currentSlide.timerBackgroundImage;
        
        headerImage.enabled = currentSlide.headerImage;
    }
    
    public void PreviousSlide()
    {
        if (_currentSlideIndex >= 1)
        {
            _currentSlideIndex--;

            LoadDataFromSo();
        }
    }

    public void NextSlide()
    {
        _currentSlideIndex++;
        
        LoadDataFromSo();
    }
}
