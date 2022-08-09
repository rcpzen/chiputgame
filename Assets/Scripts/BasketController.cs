using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{

  public Camera cam;
  // Start is called before the first frame update
  private Rigidbody2D rb;
  private float maxWidth;
  void Start()
  {

    rb = GetComponent<Rigidbody2D>();
    Vector3 upperCorner = new Vector3(Screen.width,Screen.height,0.0f);
    Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
    float hatWidth = GetComponent<Renderer>().bounds.extents.x;
    maxWidth = targetWidth.x - hatWidth;
    if (cam == null)
    {
      cam = Camera.main;
    }
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);
    Vector3 targetPosition = new Vector3(rawPosition.x,0.0f,0.0f);
    float targetWidth = Mathf.Clamp(targetPosition.x,-maxWidth,maxWidth);
    targetPosition = new Vector3(targetWidth,transform.position.y,targetPosition.z);
    rb.MovePosition(targetPosition);
  }
}
