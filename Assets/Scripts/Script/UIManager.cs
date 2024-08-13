using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour //�÷��̾� UI
{
    public static UIManager s;


    public Image playerHP;
    public Text playerGold;


    private void Awake()
    {
        s = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayer(PlayerStats stats)
    { 
        playerHP.rectTransform.localScale = new Vector3((float)stats.statCurHP / (float)stats.statMaxHP, 1f, 1f);
        playerGold.text = "Gold: " + stats.curGold.ToString();
    }
}
