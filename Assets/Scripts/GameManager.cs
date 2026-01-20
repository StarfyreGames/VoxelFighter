using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public GameObject PopUpScreen;
    [SerializeField] public GameObject BossMeter;
    [SerializeField] public TextMeshProUGUI PopUpText;
    [SerializeField] public Image FadeScreen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PopUpScreen.SetActive(false);
        BossMeter.SetActive(false);
    }

    //Handle Scene Changes?
    //keeping copy of current upgrades etc when moving to next level

    public void KillGame()
    {
        Destroy(this);
    }

}
