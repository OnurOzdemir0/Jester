using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CharacterController_ : MonoBehaviour
{
    [SerializeField] private Camera playerCam;

    private IInteractable currentInteractable = null;


    private float walkSpeed;

    [SerializeField]  private float speed;


    [SerializeField] int rayRadius = 5;


    private bool canMove  =true;
    private bool canDetectCard= false;

    // shoot ray to detect card

    // reverse card

    // play card

    [SerializeField] CharacterController controller;

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        if (canDetectCard)
        {
            shootRayIntoMouseDirection(); // always shoot 
        }
       
        
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Game Start"))
        {
            canMove = false;
            transform.DOMove(new Vector3( other.gameObject.transform.position.x , transform.position.y, other.gameObject.transform.position.z), 1);
            canDetectCard = true;
        }
    }
   

    private void Update()
    {
        if (canMove)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(-1 * horizontal, 0, -1 * vertical).normalized;
            if (virtualCamera)
            {
                if (direction.magnitude > 0)
                {
                    virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 5;
                }
                else
                {
                    virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1;
                }
            }
            if (controller)
            {
                controller.Move(direction * speed * Time.deltaTime);
            }

        }



        if (Input.GetMouseButtonDown(0))
        {
            if(currentInteractable != null)
            {
                currentInteractable.OnPressLeftClick();
            }

            


        }
        if(Input.GetMouseButtonDown(1))
        {
            if(currentInteractable != null)
            {
                currentInteractable.OnPressRightClick();
            }
        }

    }
    private void shootRayIntoMouseDirection()
    {
        Vector3  mousedir =  new Vector3 (Input.mousePosition.x, Input.mousePosition.y, transform.position.z + playerCam.nearClipPlane) ;
        mousedir = playerCam.ScreenToWorldPoint(mousedir);
     
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        //Debug.Log("origin :" + ray.origin + " dir : " + mousedir);
        Debug.DrawRay(ray.origin,ray.direction*1000,Color.red,1);


        Dictionary<Collider, int> keyValuePairs = new Dictionary<Collider, int>();


        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {   


            if (keyValuePairs.TryGetValue(hit.collider, out int val))
            {

                keyValuePairs[hit.collider] = val++;

            }
            else
            {
                keyValuePairs.Add(hit.collider, 1);
            }

        }

        for (int i = 0; i< playerCam.transform.childCount;i++)
        {
            Transform child = playerCam.transform.GetChild(i);
           // Debug.Log(child.forward);
            
            Ray newRay= new Ray(child.position,child.forward);

            RaycastHit myHit;
            
            if (Physics.Raycast(ray, out myHit, Mathf.Infinity))
            {
                Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 1);
                if (keyValuePairs.TryGetValue(myHit.collider, out int val)) {

                    keyValuePairs[myHit.collider] = val++;

                }
                else
                {
                    keyValuePairs.Add(myHit.collider, 1);
                }

            }


        }
        Collider maximumCollided = null;
        int max = 0;
        foreach( Collider collider in keyValuePairs.Keys ) {
            //Debug.Log(collider.name);
            int val = keyValuePairs[collider];
            if(val > max)
            {
                max = val;
                maximumCollided = collider;
            }

      
        }
        


        if(maximumCollided){
            
            // Debug.Log("Card found");
            if (maximumCollided.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                if(currentInteractable == null)
                {
                    currentInteractable = interactable;
                    currentInteractable.OnSelection();

                }else if (currentInteractable != interactable)
                {
                    currentInteractable.OnDeSelection();
                    currentInteractable = interactable;
                    currentInteractable.OnSelection();

                }

            }
            else
            {   
                if(currentInteractable != null)
                {
                    currentInteractable.OnDeSelection();
                    currentInteractable = null;
                }
               
                
            }

        }
        else
        {   if(currentInteractable != null) {
                currentInteractable.OnDeSelection();
                currentInteractable = null;
            
            }
            
        }
    }

    


}
