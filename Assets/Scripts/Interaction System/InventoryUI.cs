using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    public TextMeshProUGUI bagName;

    [Header("Item Details")]

    public GameObject itemContents;
    public GameObject itemDetailsParent;
    public Image itemDetailsBackgroundImage;

    Inventory inventory;
    Book book;
    UIManager uiManager;
    InventorySlot[] slots;

    public Item currentItem;

    [Header("Rewards collections")]
    public TextMeshProUGUI goldCoinsCollectedText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI numberOfPhotographsCollectedText;

    void Start()
    {
        inventory = Inventory.instance;
        book = Book.instance;
        uiManager = UIManager.instance;
        inventory.OnItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>(true);

        UpdateUI();

        //bagName.text = Player.playerName + "'s Bag";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.container.Count)
            {
                slots[i].AddItem(inventory.container[i].item, inventory.container[i].amount);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void OpenInventory()
    {
        //inventoryUI.SetActive(!inventoryUI.activeSelf);
        inventoryUI.SetActive(true);
        //itemDetailsParent.SetActive(true);
        itemContents.SetActive(false);
        inventoryUI.transform.localPosition = new Vector3(-132, -500, 0);
        inventoryUI.GetComponent<CanvasGroup>().alpha = 0;

        inventoryUI.transform.DOLocalMoveY(-50, 0.5f).SetEase(Ease.OutBack);
        inventoryUI.GetComponent<CanvasGroup>().DOFade(1, 0.5f);

        UIManager.instance.DisablePlayerMovement();

        goldCoinsCollectedText.text = inventory.goldCoins.ToString();
        scoreText.text = inventory.naturePoints.ToString();
        numberOfPhotographsCollectedText.text = book.photosInventory.Count.ToString();

        SoundManager.instance.PlaySoundFromClips(9);

        uiManager.RotatePlayerToCamera();
        uiManager.animatedFirstPersonCamera.SetActive(true);
    }

    public void CloseInventory()
    {
        //itemDetailsBackgroundImage.color = new Color32(116, 91, 91, 255);
        inventoryUI.transform.DOLocalMoveY(-500, 0.5f).SetEase(Ease.InBack);
        inventoryUI.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() =>
        {
            inventoryUI.SetActive(false);
            //itemDetailsParent.SetActive(false);
        });

        //inventoryUI.SetActive(false);
        //itemDetailsParent.SetActive(false);
        UIManager.instance.EnablePlayerMovement();
        uiManager.animatedFirstPersonCamera.SetActive(false);
    }

    public void ConsumeItem()
    {
        currentItem.Use();

        CloseInventory();
    }


}
