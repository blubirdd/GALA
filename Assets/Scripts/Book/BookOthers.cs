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
        if(Player.instance.villageQuizScore != 0)
        {
            villageQuizScoreText.text = "Village Quiz: " + Player.instance.villageQuizScore.ToString() + "/5";
        }
       
    }

    public void UpdateGrasslandQuizScore()
    {
        if(Player.instance.grasslandQuizScore != 0)
        {
            grasslandQuizScoreText.text = "Grassland Quiz: " + Player.instance.grasslandQuizScore.ToString() + "/5";
        }
    }

    public void UpdateRiverlandQuizScore()
    {
        if(Player.instance.riverQuizScore != 0)
        {
            riverQuizScoreText.text = "River Quiz: " + Player.instance.riverQuizScore.ToString() + "/5";
        }
      
    }

    public void UpdatSwampQuizScore()
    {
        if(Player.instance.swampQuizScore != 0)
        {
            swampQuizScoreText.text = "Wetlands Quiz: " + Player.instance.swampQuizScore.ToString() + "/5";
        }

    }

    public void UpdateGalaQuizScore()
    {
        if(Player.instance.galaQuizScore != 0)
        {
            galaQuizScoreText.text = "Forest Quiz: " + Player.instance.galaQuizScore.ToString() + "/5";
        }
       
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
