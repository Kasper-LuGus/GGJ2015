using UnityEngine;
using System.Collections;

public class TOD : MonoBehaviour
{
    public float slider;
    public float slider2;
    public float Hour;
    private float Tod;

    public Light sun;

    public int speed = 50;

    public Color NightFogColor;
    public Color DuskFogColor;
    public Color MorningFogColor;
    public Color MiddayFogColor;

    public Color NightAmbientLight;
    public Color DuskAmbientLight;
    public Color MorningAmbientLight;
    public Color MiddayAmbientLight;

    public Color NightTint;
    public Color DuskTint;
    public Color MorningTint;
    public Color MiddayTint;

    public Material SkyBoxMaterial1;
    public Material SkyBoxMaterial2;

    public Color SunNight;
    public Color SunDay;

    public GameObject Water;
    public bool IncludeWater = false;
    public Color WaterNight;
    public Color WaterDay;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (speed == 0)
        {
            return;
        }
        if (slider >= 1.0)
        {
            slider = 0;
        }

        Hour = slider * 24;
        Tod = slider2 * 24;
        sun.transform.localEulerAngles = new Vector3((slider * 360) - 90, 0, 0);
        slider = slider + Time.deltaTime / speed;
        sun.color = Color.Lerp(SunNight, SunDay, slider * 2);

        if (IncludeWater == true)
        {
            Water.renderer.material.SetColor("_horizonColor", Color.Lerp(WaterNight, WaterDay, slider2 * 2 - 0.2f));
        }

        if (slider < 0.5)
        {
            slider2 = slider;
        }
        else if (slider > 0.5)
        {
            slider2 = (1 - slider);
        }
        sun.intensity = (slider2 - 0.2f) * 1.7f;


        if (Tod < 4)
        {
            //it is Night
            RenderSettings.skybox = SkyBoxMaterial1;
            RenderSettings.skybox.SetFloat("_Blend", 0);
            SkyBoxMaterial1.SetColor("_Tint", NightTint);
            RenderSettings.ambientLight = NightAmbientLight;
            RenderSettings.fogColor = NightFogColor;
        }
        else if (Tod > 4 && Tod < 6)
        {
            //it is Dusk
            RenderSettings.skybox = SkyBoxMaterial1;
            RenderSettings.skybox.SetFloat("_Blend", 0);
            RenderSettings.skybox.SetFloat("_Blend", (Tod / 2) - 2);
            SkyBoxMaterial1.SetColor("_Tint", Color.Lerp(NightTint, DuskTint, (Tod / 2) - 2));
            RenderSettings.ambientLight = Color.Lerp(NightAmbientLight, DuskAmbientLight, (Tod / 2) - 2);
            RenderSettings.fogColor = Color.Lerp(NightFogColor, DuskFogColor, (Tod / 2) - 2);
        }
        else if (Tod > 6 && Tod < 8)
        {
            //it is Morning
            RenderSettings.skybox = SkyBoxMaterial2;
            RenderSettings.skybox.SetFloat("_Blend", 0);
            RenderSettings.skybox.SetFloat("_Blend", (Tod / 2) - 3);
            SkyBoxMaterial2.SetColor("_Tint", Color.Lerp(DuskTint, MorningTint, (Tod / 2) - 3));
            RenderSettings.ambientLight = Color.Lerp(DuskAmbientLight, MorningAmbientLight, (Tod / 2) - 3);
            RenderSettings.fogColor = Color.Lerp(DuskFogColor, MorningFogColor, (Tod / 2) - 3);
        }
        else if (Tod > 8 && Tod < 10)
        {
            //it is getting Midday
            RenderSettings.ambientLight = MiddayAmbientLight;
            RenderSettings.skybox = SkyBoxMaterial2;
            RenderSettings.skybox.SetFloat("_Blend", 1);
            SkyBoxMaterial2.SetColor("_Tint", Color.Lerp(MorningTint, MiddayTint, (Tod / 2) - 4));
            RenderSettings.ambientLight = Color.Lerp(MorningAmbientLight, MiddayAmbientLight, (Tod / 2) - 4);
            RenderSettings.fogColor = Color.Lerp(MorningFogColor, MiddayFogColor, (Tod / 2) - 4);
        }
    }
}
