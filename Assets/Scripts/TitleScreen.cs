using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using TMPro;
using UnityEditor.SearchService;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] string firstSceneToLoad; 

    [Header("Slides")]
    [SerializeField] public Sprite Title;
    [SerializeField] public Sprite Presents;
    [SerializeField] public Sprite Company;

    [Header("DisplayField")]
    [SerializeField] private UnityEngine.UI.Image frame;
    [SerializeField] private TextMeshProUGUI pressKey;

    private bool canPressKey = false;



    private void Start()
    {
        pressKey.gameObject.SetActive(false);
        frame.sprite = null;
        StartCoroutine(CycleScreen());
    }

    private void Update()
    {
        if (canPressKey && Input.anyKeyDown) 
        {
            SceneManager.LoadScene(firstSceneToLoad);
        }
    }
    
    private IEnumerator CycleScreen() //Vibe coded
    {
        while (true) 
        {
            yield return StartCoroutine(FadeImage(frame, Company, 1f));
            yield return new WaitForSeconds(10f);

            yield return StartCoroutine(FadeImage(frame, Presents, 1f));
            yield return new WaitForSeconds(5f);

            yield return StartCoroutine(FadeImage(frame, Title, 1f));

            pressKey.gameObject.SetActive(true);
            canPressKey = true;
            pressKey.text = $"Press Any Key To Continue...";
            StartCoroutine(GlintText(pressKey, 3f)); // 3 = blink speed

            yield return new WaitForSeconds(15f);

            //SceneManager.LoadScene(firstSceneToLoad);
        }
    }

    private IEnumerator FadeImage(Image img, Sprite newSprite, float duration)
    {
        // Fade out
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = 1 - (t / duration);
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
            yield return null;
        }

        img.sprite = newSprite;

        // Fade in
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = t / duration;
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
            yield return null;
        }
    }
    private IEnumerator GlintText(TextMeshProUGUI tmp, float speed)
    {
        while (true)
        {
            float alpha = (Mathf.Sin(Time.time * speed) + 1f) / 2f; // oscillates 0–1
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, alpha);
            yield return null;
        }
    }



}
