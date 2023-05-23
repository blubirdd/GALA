using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering;

public class TimeController : MonoBehaviour, IDataPersistence
{
    #region Singleton

    public static TimeController instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of TimeController found");
            return;
        }

        instance = this;
    }
    #endregion

    [Range(0f, 24f)]
    public float timeHour;

    [SerializeField]
    private float timeMultiplier;

    [SerializeField]
    private float startHour;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;

    [SerializeField]
    private Color dayAmbientLight;

    [SerializeField]
    private Color nightAmbientLight;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private float maxSunLightIntensity;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private float maxMoonLightIntensity;

    private DateTime currentTime;

    private TimeSpan sunriseTime;

    private TimeSpan sunsetTime;

    [Header("Fog Colors")]
    [SerializeField] private Color dayFogColor;
    [SerializeField] private Color nightFogColor;

    [Header("Canvases")]
    public GameObject sleepToNightCanvas;
    public GameObject sleeptoDayCanvas;

    [Header("Skybox Materials")]
    [SerializeField] private Material daySkyboxMaterial;
    [SerializeField] private Material nightSkyboxMaterial;

    // Start is called before the first frame update
    void Start()
    {

        //currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        //sunriseTime = TimeSpan.FromHours(sunriseHour);
        //sunsetTime = TimeSpan.FromHours(sunsetHour);


        ////set up skybox on start
        //if (timeHour >= sunriseHour && timeHour < sunsetHour)
        //{
        //    RenderSettings.skybox = daySkyboxMaterial;
        //}
        //else
        //{
        //    RenderSettings.skybox = nightSkyboxMaterial;
        //}

        //StartCoroutine(UpdateTimeOfDayCoroutineForOptimization());
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateTimeOfDay();
        //RotateSun();
        //UpdateLightSettings();
    }

    public void SetTimeOfDay(float hour)
    {
      currentTime = DateTime.Now.Date + TimeSpan.FromHours(hour);

      if(hour >= 19)
      {
            //turn to night
            sunLight.gameObject.SetActive(false);

            RenderSettings.skybox = nightSkyboxMaterial;

            RenderSettings.ambientMode = AmbientMode.Flat; // Set the ambient mode to Flat
            RenderSettings.ambientLight = nightAmbientLight; // Set the ambient light color to black or any other desired color

            RenderSettings.fogColor = nightFogColor;
            
            StartCoroutine(SetActiveForFewSeconds(sleepToNightCanvas));
      }
      if(hour <=10)
      {
            //turn to day
            sunLight.gameObject.SetActive(true);

            RenderSettings.skybox = daySkyboxMaterial;
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.fogColor = dayFogColor;
            StartCoroutine(SetActiveForFewSeconds(sleeptoDayCanvas));
      }
    }

    public void SetTimeOfDayWithoutCutScene(float hour)
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(hour);
    }

    IEnumerator SetActiveForFewSeconds(GameObject objectToActivate)
    {
        objectToActivate.SetActive(true);
        Debug.Log("active");
        yield return new WaitForSecondsRealtime(3f);
        Debug.Log("not active");
        objectToActivate.SetActive(false);
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if (timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
            timeHour = currentTime.Hour;
        }

        // Set skybox material based on time of day
        if (timeHour >= sunriseHour && timeHour < sunsetHour)
        {
            RenderSettings.skybox = daySkyboxMaterial;
            sunLight.intensity = 1;
            moonLight.intensity = 0f ;

        }
        else
        {
            RenderSettings.skybox = nightSkyboxMaterial;
            sunLight.intensity = 0f;
            moonLight.intensity = 1;
        }

    }

    private IEnumerator UpdateTimeOfDayCoroutineForOptimization()
    {
        while (true)
        {
            UpdateTimeOfDay();
            RotateSun();
            UpdateLightSettings();
            yield return new WaitForSeconds(0f);
        }
    }
    private void RotateSun()
    {
        float sunLightRotation;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        { 
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));

        RenderSettings.fogColor = Color.Lerp(nightFogColor, dayFogColor, lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    public void LoadData(GameData data)
    {
        //StartCoroutine(WaitForTimeToSet());
        //IEnumerator WaitForTimeToSet()
        //{
        //    yield return new WaitForEndOfFrame();
        //    SetTimeOfDayWithoutCutScene(data.time);
        //}

    }

    public void SaveData(GameData data)
    {
        data.time = timeHour;
    }
}
