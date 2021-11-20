using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public UnityEvent gameStart;

    [SerializeField]
    Button StartButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(startGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void startGame() {
        gameStart.Invoke();
        StartButton.gameObject.SetActive(false);
    }
}
