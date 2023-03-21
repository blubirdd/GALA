using StarterAssets;
using System.Collections;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EquipmentManger found");
            return;
        }

        instance = this;
    }
    #endregion
    public Transform playerArmature;

    [Header("Throwing")]
    [SerializeField] private UICanvasControllerInput uICanvasControllerInput;
    public bool isReadyToThrow = false; 
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _releasePosition;
    public LayerMask collisionMask;

    [Header("Throwing - Display Controls")]
    [SerializeField][Range(10, 100)] private int LinePoints = 25;
    [SerializeField] [Range(0.01f, 0.25f)] private float TimeBetweenPoints = 0.1f;


    public Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    //caching singleton
    Inventory inventory;
    UIManager uiManager;

    //3D
    public GameObject handEquipObject;
    public HandItem[] handItems;

    [Header("Instantiation point")]
    public Transform throwingPoint;

    [Header("FIRING")]
    public GameObject reticleAimCrosshair;
    private void Start()
    {
        //cach singletons
        inventory = Inventory.instance;
        uiManager = UIManager.instance;

        int  numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

        //3D objects in hand
        handItems = handEquipObject.GetComponentsInChildren<HandItem>(true);

    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;
        
        //if there is currrent equipment in slot
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            Debug.Log("Replacing existing");
            inventory.Add(oldItem, 1);
            UnequipItemIn3D(oldItem);
        }

        //invoke the event
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        
        //fill up slot index
        currentEquipment[slotIndex] = newItem;

        //equip in 3D
        EquipItemIn3D(newItem);
        

    }



    //unequip
    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];

            inventory.Add(oldItem, 1);

            currentEquipment[slotIndex] = null;
            UnequipItemIn3D(oldItem);
            //invoke the event
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

        }

    }

    public void EquipItemIn3D(Equipment item)
    {
        //equip hand item in 3D space
        for (int i = 0; i < handItems.Length; i++)
        {
            if (handItems[i].handItem == item)
            {
                handItems[i].gameObject.SetActive(true);
                Debug.Log("Equipped" + handItems[i].handItem);

                break;
            }
        }

        //line
        if (item.isThrowable)
        {
            isReadyToThrow = true;

            uiManager.EnableThrowUI();


            StartCoroutine(DrawProjection());

            uICanvasControllerInput.VirtualAimInput(true);
        }

        if (item.isUsable)
        {
            uiManager.EnableUnequipButton();
        }

        if(item.isAimable && item.isFirable == false)
        {
            uiManager.EnableAimUI();
        }

        if(item.isFirable)
        {
            uICanvasControllerInput.VirtualAimInput(true);
            uiManager.EnableFishCastButton();
            reticleAimCrosshair.SetActive(true);
        }

    }

    IEnumerator DrawProjection()
    {
        while (isReadyToThrow)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.positionCount = Mathf.CeilToInt(LinePoints / TimeBetweenPoints) + 1;
            Vector3 startPosition = _releasePosition.position;
            Vector3 startVelocity = 15f * playerArmature.transform.forward / 1f;

            int i = 0;
            _lineRenderer.SetPosition(i, startPosition);

            for (float time = 0; time < LinePoints; time += TimeBetweenPoints)
            {
                i++;
                Vector3 point = startPosition + time * startVelocity;
                point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);

                _lineRenderer.SetPosition(i, point);

                Vector3 lastPosition = _lineRenderer.GetPosition(i - 1);

                if (Physics.Raycast(lastPosition, (point - lastPosition).normalized, out RaycastHit hit, (point - lastPosition).magnitude, collisionMask))
                {
                    _lineRenderer.SetPosition(i, hit.point);
                    _lineRenderer.positionCount = i + 1;

                    break;
                }
            }

            yield return true;
        }

    }
    public void UnequipItemIn3D(Equipment item)
    {

        //equip hand item in 3D space
        for (int i = 0; i < handItems.Length; i++)
        {
            if (handItems[i].handItem == item)
            {
                handItems[i].gameObject.SetActive(false);
                Debug.Log("Unequipped" + handItems[i].handItem);

                //disable line
                _lineRenderer.enabled = false;
                isReadyToThrow = false;

                if (item.isFirable)
                {
                    reticleAimCrosshair.SetActive(false);
                }
                break; 
            }
        }

    }

    public void ThrowItem()
    {
        Rigidbody throwableItemPrefab;

        StartCoroutine(WaitForThrowingAnimation());

        IEnumerator WaitForThrowingAnimation()
        {

            for (int i = 0; i < handItems.Length; i++)
            {

                if (handItems[i].gameObject.activeSelf)
                {
                   
                    uiManager.DisableThrowUI();

                    yield return new WaitForSeconds(1.3f);
                    //disable hand item gameobject

                    HandItem handItem;
                    if (handItems[i].gameObject.TryGetComponent(out handItem))
                    {
                        if(handItem.disableOnthrow)
                        {
                            currentEquipment[0] = null;
                            UnequipItemIn3D(handItem.handItem);
                        }

                        if(handItem.playPartiicleOnThrow)
                        {
                            Instantiate(handItems[i].particleToSpawnOnThrow, throwingPoint.position, Quaternion.identity);
                        }

                        if(handItem.equipmentToReplace)
                        {
                            Equip(handItem.equipmentToReplace);
                        }
                    }

                    GameObject go = Instantiate(handItems[i].handItemPrefab, throwingPoint.position, throwingPoint.rotation);
                    //particle


                    //launch instantiated item off hand
                    throwableItemPrefab = go.GetComponent<Rigidbody>();
                    throwableItemPrefab.transform.SetParent(null, true);
                    throwableItemPrefab.AddForce(playerArmature.transform.forward * 15f, ForceMode.Impulse);


                    //isReadyToThrow = false;
                    //_lineRenderer.enabled = false;
                    break;
                }
            }
            
        }

        uICanvasControllerInput.VirtualAimInput(false);

    }



}
