using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public bool isShadow;

    public Player player;

    public Image portrait;

    public TMP_Text nameText;
    public TMP_Text characlassText;
    public TMP_Text levelText;
    public TMP_Text pointsText;
    public TMP_Text[] statTexts;
    public Button[] statPointsButtons;
    public int[] allocatedPoints;
    private int tempPoints;

    private void Start()
    {
        statTexts = GetComponentsInChildren<TMP_Text>();
        statPointsButtons = GetComponentsInChildren<Button>(true);      // true to get inactive too
        allocatedPoints = new int[8];

        for (int i = 0; i < statPointsButtons.Length; i++)
        {
            // Plus button
            if (i % 2 == 0)
            {
                int x = i / 2;              // each button has own x
                statPointsButtons[i].onClick.AddListener(() =>
                {
                    allocatedPoints[x]++;
                    tempPoints--;
                });
            }
            // Minus button
            else
            {
                int x = i / 2;              // each button has own x
                statPointsButtons[i].onClick.AddListener(() =>
                {
                    allocatedPoints[x]--;
                    tempPoints++;
                });
            }
        }
    }

    private void OnEnable()
    {
        if (isShadow)
        {
            player = PartyController.shadow.GetComponent<Player>();
        }
        else
        {
            player = PartyController.player.GetComponent<Player>();
        }
        ClearPoints();
    }


    void Update()
    {
        if (tempPoints > player.statPoints)
        {
            tempPoints = player.statPoints;
        }

        portrait.sprite = player.gameObject.GetComponent<PortraitBehaviour>().portraitToDisplay;
        characlassText.text = player.getCharClass();
        levelText.text = "" + player.currentLevel;
        pointsText.text = "" + tempPoints;
        
        statTexts[0].text = "HP";
        statTexts[1].text = "" + (player.getBaseStats()["hp"] + allocatedPoints[0]);
        statTexts[2].text = "MP";
        statTexts[3].text = "" + (player.getBaseStats()["mp"] + allocatedPoints[1]);
        statTexts[4].text = "Atk";
        statTexts[5].text = "" + (player.getBaseStats()["atk"] + allocatedPoints[2]);
        statTexts[6].text = "Def";
        statTexts[7].text = "" + (player.getBaseStats()["def"] + allocatedPoints[3]);
        statTexts[8].text = "Matk";
        statTexts[9].text = "" + (player.getBaseStats()["matk"] + allocatedPoints[4]);
        statTexts[10].text = "Mdef";
        statTexts[11].text = "" + (player.getBaseStats()["mdef"] + allocatedPoints[5]);
        statTexts[12].text = "Agi";
        statTexts[13].text = "" + (player.getBaseStats()["agi"] + allocatedPoints[6]);
        statTexts[14].text = "Luk";
        statTexts[15].text = "" + (player.getBaseStats()["luk"] + allocatedPoints[7]);


        for (int i = 0; i < statPointsButtons.Length; i++)
        {
            // Plus button
            if (i % 2 == 0)
            {
                if (tempPoints > 0)
                {
                    statPointsButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    statPointsButtons[i].gameObject.SetActive(false);
                }
            }

            // Minus button
            else
            {
                if (allocatedPoints[i/2] > 0)
                {
                    statPointsButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    statPointsButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void ShowShadowStats()
    {
        isShadow = true;
        OnEnable();
    }

    public void ShowPlayerStats()
    {
        isShadow = false;
        OnEnable();
    }

    public void ConfirmPoints()
    {
        for (int i = 0; i < allocatedPoints.Length; i++)
        {
            if (allocatedPoints[i] > 0)
            {
                string statName = i switch
                {
                    0 => "hp",
                    1 => "mp",
                    2 => "atk",
                    3 => "def",
                    4 => "matk",
                    5 => "mdef",
                    6 => "agi",
                    7 => "luk",
                    _ => "hp"
                };

                if (player.statPoints - allocatedPoints[i] >= 0)
                {
                    player.stats.addBaseStat(statName, allocatedPoints[i]);
                    player.statPoints -= allocatedPoints[i];
                }
            }
        }

        ClearPoints();      // reset points shown

    }

    public void ClearPoints()
    {
        allocatedPoints = new int[8];
        tempPoints = player.statPoints;
    }

}