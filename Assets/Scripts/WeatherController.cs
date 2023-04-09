using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] Material sky;
    [SerializeField] Light sun;

    private float fullIntensity;
    private float cloudValue = 0f;

    // Start is called before the first frame update
    void Start()
    {
        fullIntensity = sun.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        var newCloudValue = cloudValue + .005f;
        newCloudValue = Mathf.Clamp(newCloudValue, 0, 1);
        if (Mathf.Approximately(cloudValue, newCloudValue))
            return;

        cloudValue = newCloudValue;
        SetOvercast(cloudValue);
    }

    private void SetOvercast(float value)
    {
        sky.SetFloat("_Blend", value);
        var newIntensity = fullIntensity - (fullIntensity * value);
        newIntensity = Mathf.Clamp(newIntensity, 0, fullIntensity);
        sun.intensity = newIntensity;
    }
}
