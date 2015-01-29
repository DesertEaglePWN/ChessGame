using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour 
{
    public Texture2D BackgroundTexture, SplashScreenTexture;
    private GUITexture Background, SplashScreen;
    private Rect BackgroundRect;
    public float alpha = 0, TransitionTime, splashAlpha =0, alphaChangeTime, alphaChangeAmount;
    private bool fading = false, splashFading = false;

    private void Awake()
    {
        Background = new GameObject("Background").AddComponent<GUITexture>();
        SplashScreen = new GameObject("SplashScreen").AddComponent<GUITexture>();
        Background.texture = BackgroundTexture;
        SplashScreen.texture = SplashScreenTexture;
    }

	// Use this for initialization
	void Start () 
    {
        
        Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, 0);
        SplashScreen.color = new Color(SplashScreen.color.r, SplashScreen.color.g, SplashScreen.color.b, 0);
        BackgroundRect.Set(0, 0, Screen.width, Screen.height);
        Background.pixelInset = BackgroundRect;
        SplashScreen.pixelInset = BackgroundRect;
        
        StartCoroutine(ScreenFadeOut(TransitionTime));
	}

    private IEnumerator ScreenFadeOut(float timeForTransition)
    {
        fading = true;
        while (alpha <= 1)
        {
            yield return new WaitForSeconds(alphaChangeTime);
            if (alpha + alphaChangeAmount > 1)
            {
                alpha = 1;
                break;
            }
            alpha += alphaChangeAmount;
        }
        yield return new WaitForSeconds(timeForTransition);
        fading = false;
        StartCoroutine(SplashScreenFade());
        StartCoroutine(ScreenFadeIn());
        yield return null;
    }

    private IEnumerator SplashScreenFade()
    {
        splashFading = true;
        while (splashAlpha <= 1)
        {
            yield return new WaitForSeconds(alphaChangeTime);
            if (splashAlpha + alphaChangeAmount > 1)
            {
                splashAlpha = 1;
                break;
            }
            splashAlpha += alphaChangeAmount;
        }
        yield return new WaitForSeconds(TransitionTime);
        while (splashAlpha <= 1)
        {
            yield return new WaitForSeconds(alphaChangeTime);
            if (splashAlpha - alphaChangeAmount < 0)
            {
                splashAlpha = 0;
                break;
            }
            splashAlpha -= alphaChangeAmount;
        }
        yield return null;
    }

    private IEnumerator ScreenFadeIn()
    {
        fading = true;
        while (alpha > 0)
        {
            yield return new WaitForSeconds(alphaChangeTime);
            if (alpha - alphaChangeAmount < 0)
            {
                alpha = 0;
                break;
            }
            alpha -= alphaChangeAmount;
        }
        fading = false;
        yield return null;
    }

    private void Update()
    {
        if(fading)
        {
            Background.color = Color.Lerp(Background.color, new Color(Background.color.r, Background.color.g, Background.color.b, alpha), 1);
        }

        if (splashFading)
        {
            SplashScreen.color = Color.Lerp(SplashScreen.color, new Color(SplashScreen.color.r, SplashScreen.color.g, SplashScreen.color.b, splashAlpha), 1);
        }
    }

}
