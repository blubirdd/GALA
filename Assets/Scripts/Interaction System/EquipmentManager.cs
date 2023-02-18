using System.Collections;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;
    public Transform playerArmature;

    [Header("Throwing")]
    public bool isReadyToThrow = false; 
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _releasePosition;
    public LayerMask collisionMask;

    [Header("Throwing - Display Controls")]
    [SerializeField][Range(10, 100)] private int LinePoints = 25;
    [SerializeField] [Range(0.01f, 0.25f)] private float TimeBetweenPoints = 0.1f;

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

    Equipment[] currentEquipment;

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

        //enable ui
        uiManager.EnableThrowUI();

    }



    //unequip
    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];

            inventory.Add(oldItem, 1);

            currentEquipment[slotIndex] = null;

            //invoke the event
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            UnequipItemIn3D(oldItem);
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
            StartCoroutine(DrawProjection());
        }
    }

    IEnumerator DrawProjection()
    {
        while (isReadyToThrow)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.positionCount = Mathf.CeilToInt(LinePoints / TimeBetweenPoints) + 1;
            Vector3 startPosition = _releasePosition.position;
            Vector3 startVelocity = 10f * playerArmature.transform.forward / 1f;

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
                    handItems[i].gameObject.SetActive(false);
                    GameObject go = Instantiate(handItems[i].handItemPrefab, throwingPoint.position, throwingPoint.rotation);

                    //launch instantiated item off hand
                    throwableItemPrefab = go.GetComponent<Rigidbody>();
                    throwableItemPrefab.transform.SetParent(null, true);
                    throwableItemPrefab.AddForce(playerArmature.transform.forward * 10f, ForceMode.Impulse);


                    isReadyToThrow = false;
                    _lineRenderer.enabled = false;
                    break;
                }
            }
            
        }

    }



}