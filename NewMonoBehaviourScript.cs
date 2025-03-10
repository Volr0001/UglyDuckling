using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
    public Image fadeScreen; // assign in inspector (black UI image in our canvas)
    public TextMeshProUGUI gameOverText; // assign in inspector 
    public float fadeDuration = 2f; //time to fully fade
    private bool gameOverTriggered = false;
    private void Start()
    {
        //ensure UI starts fully invisible
        fadeScreen.color = new Color(0, 0, 0, 0); // fully transparent black
        gameOverText.color = new Color(gameOverText.color.r, gameOverText.color.g, gameOverText.color.b, 0); //fully transparent text
    }

    public void TriggerGameOver()
    {
        // start fade to black when triggered
        if (gameOverTriggered) return;
        gameOverTriggered = true;
        StartCoroutine(FadeToBlack());

    }

    private IEnumerator FadeToBlack()
    {
        float t = 0f;
        Color fadeColor = fadeScreen.color;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            fadeColor.a = Mathf.Lerp(0, 1, t); // increase alpha (fade in)
            fadeScreen.color = fadeColor;
            yield return null;
        }


        // start showing game over text after fade to black
        StartCoroutine(ShowGameOverText());
        yield break;//stops routine once triggered
    }


    private IEnumerator ShowGameOverText()
    {
        float t = 0f;
        Color textColor = gameOverText.color;

        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            textColor.a = Mathf.Lerp(0, 1, t); // increase alpha (fade in)
            gameOverText.color = textColor;
            yield return null;
        }
    }
}
