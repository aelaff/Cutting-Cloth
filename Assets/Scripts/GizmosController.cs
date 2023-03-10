
using UnityEngine;


public class GizmosController : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Collider>().enabled = true;
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        }
        if (Input.GetMouseButton(0)){
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            transform.position = new Vector3(curPosition.x, curPosition.y,3);
        }
        if (Input.GetMouseButtonUp(0)) {
            GetComponent<Collider>().enabled = false;
            transform.position = new Vector3(0, 0, 2);


        }
    }
  

}