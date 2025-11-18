using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public GameObject PopUpScreen;
    [SerializeField] public TextMeshProUGUI PopUpText;

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
    }

    //Handle Scene Changes?
    //keeping copy of current upgrades etc when moving to next level



}
