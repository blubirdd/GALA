using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookOthers : MonoBehaviour
{
    #region Singleton

    public static BookOthers instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of BookOthers found");
            return;
        }

        instance = this;
    }
    #endregion

    [Header("OTHERS")]
    public TextMeshProUGUI villageQuizScoreText;
    public TextMeshProUGUI grasslandQuizScoreText;
    public TextMeshProUGUI riverQuizScoreText;
    public TextMeshProUGUI swampQuizScoreText;
    public TextMeshProUGUI galaQuizScoreText;

    //Player player;
    //private void Start()
    //{
    //    player = Player.instance;
    //}

    public void UpdateVillageQuizScore()
    {
        villageQuizScoreText.text = Player.instance.villageQuizScore.ToString();
    }

    public void UpdateGrasslandQuizScore()
    {
        grasslandQuizScoreText.text = Player.instance.grasslandQuizScore.ToString();
    }

    public void UpdateRiverlandQuizScore()
    {
        riverQuizScoreText.text = Player.instance.riverQuizScore.ToString();
    }

    public void UpdatSwampQuizScore()
    {
        swampQuizScoreText.text = Player.instance.swampQuizScore.ToString();
    }

    public void UpdateGalaQuizScore()
    {
        galaQuizScoreText.text = Player.instance.galaQuizScore.ToString();
    }

    private void OnEnable()
    {
        UpdateVillageQuizScore();
        UpdateGrasslandQuizScore();
        UpdateRiverlandQuizScore();
        UpdatSwampQuizScore();
        UpdateGalaQuizScore();
    }

    //private void OnEnable()
    //{
    //    if(Player.instance.villageQuizScore > 0)
    //    {
    //        villageQuizScoreText.text = "Village Quiz : "+ Player.instance.villageQuizScore.ToString() +"/5";
    //    }
    //    else
    //    {
    //        villageQuizScoreText.text = "Village Quiz: ???";
    //    }

    //    if (Player.instance.grasslandQuizScore > 0)
    //    {
    //        grasslandQuizScoreText.text = "Quiz : " + Player.instance.grasslandQuizScore.ToString() + "/5";
    //    }
    //    else
    //    {
    //        grasslandQuizScoreText.text = "Grassland Quiz: ???";
    //    }

    //}
}
