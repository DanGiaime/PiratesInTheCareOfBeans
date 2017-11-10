using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanCamera : MonoBehaviour {

    public Vector2[] cameraBounds;

    Transform followObj;
    public Transform FollowObject {
        get { return followObj; }

        set {
            if (value != null)
                followingObject = true;
            else
                followingObject = false;

            followObj = value;
        }
    }

    bool followingObject;

    [SerializeField]
    float panSpeed, panSensitivity;

    [SerializeField]
    Image panHandle;

    [SerializeField]
    Canvas canvas;

    Camera cam;

	void Start () {
        cam = Camera.main;
	}
	
	/// <summary>
    /// Update the camera movement behavior.
    /// </summary>
	void Update () {
        //If we're not following an object...
        if (!followingObject) {

            panHandle.enabled = false;

            //Pan the camera if necessary
            if (MouseCloseToCameraBounds()) {
                panHandle.enabled = true;

                Vector2 pos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
                Vector3 tfp = canvas.transform.TransformPoint(pos);
                panHandle.transform.position = new Vector2(
                    Mathf.Clamp(tfp.x, panHandle.rectTransform.rect.width, cam.pixelWidth - panHandle.rectTransform.rect.width),
                    Mathf.Clamp(tfp.y, panHandle.rectTransform.rect.height, cam.pixelHeight - panHandle.rectTransform.rect.height));

                //Get a vector from the center of the camera space to the mouse, translate the camera along it.
                Vector2 dir = cam.ScreenToWorldPoint(Input.mousePosition) - cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2));

                cam.transform.Translate(dir * panSpeed * Time.deltaTime);

                //Rotate the image
                Vector2 target = transform.position - cam.ScreenToWorldPoint(Input.mousePosition);

                float ang = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
                panHandle.transform.rotation = Quaternion.Euler(0, 0, ang + 180f);
            }
        } else {
            //If we're ffollowing an object...
            //Break the follow by panning
            if (MouseCloseToCameraBounds())
                FollowObject = null;

            //Follow the object
            cam.transform.position = new Vector3(FollowObject.position.x, FollowObject.position.y, cam.transform.position.z);
        }

        //Clamp the camera into the level bounds
        ClampCameraBounds();
    }

    /// <summary>
    /// Clamps the camera position into the level bounds
    /// </summary>
    void ClampCameraBounds() {
        cam.transform.position = new Vector3(
            Mathf.Clamp(cam.transform.position.x, cameraBounds[0].x, cameraBounds[0].y),
            Mathf.Clamp(cam.transform.position.y, cameraBounds[1].x, cameraBounds[1].y),
            cam.transform.position.z);
    }

    /// <summary>
    /// Determine if the mouse is close to the screen edge (i.e. we should be panning).
    /// </summary>
    /// <returns>True if mouse is close to the camera bounds.</returns>
    bool MouseCloseToCameraBounds()
    {
        Vector2 mPos = Input.mousePosition;

        float sensX = cam.pixelWidth * panSensitivity;
        float sensY = cam.pixelHeight * panSensitivity;

        return mPos.x > cam.pixelWidth - sensX
            || mPos.x < sensX
            || mPos.y > cam.pixelHeight - sensY
            || mPos.y < sensY;
    }
}
