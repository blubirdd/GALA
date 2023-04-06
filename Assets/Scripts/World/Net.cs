using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Animal animal;
        Character character;

        if (other.TryGetComponent(out animal))
        {

            if (other.TryGetComponent(out character))
            {
                if (animal.canMove)
                {
                    TalkEvents.CharacterApproach(character);
                    Debug.Log("Goal triggered: " + character.name);
                }

            }

            animal.canMove = false;
            animal.ChangeUI(Animal.AnimalStateUI.Captured);
            ParticleManager.instance.SpawnPuffParticle(other.transform.position);


            PopupWindow.instance.AddToQueue(item);
            Destroy(gameObject);
        }
    }

}
