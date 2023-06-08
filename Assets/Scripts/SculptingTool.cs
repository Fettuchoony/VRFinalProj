using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SculptingTool : MonoBehaviour
{

    // [SerializeField] private GameObject laser;
    private Vector3 longForward;
    private GameObject[] sculptPoints;
    public LayerMask IgnoreMe;

    // Start is called before the first frame update
    void Start()
    {
        longForward = new Vector3(0f, 0f, 100f);
        sculptPoints = GameObject.FindGameObjectsWithTag("Sculptor");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 forward = transform.GetChild(6).position;

        RaycastHit hit;
        Physics.Linecast(transform.position, forward, out hit, ~IgnoreMe);
        foreach (GameObject sculptPoint in sculptPoints) {
                    sculptPoint.transform.position = hit.point;
                }
        DrawLine(transform.position, forward, Color.white);

        // Button press handler, almost gave me a stroke
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        bool invalidDeviceFound = false;
        foreach(var device in inputDevices)
        {
            bool primaryButtonState = false;
            bool tempState = device.isValid // the device is still valid
                        && device.TryGetFeatureValue(CommonUsages.triggerButton, out primaryButtonState) // did get a value
                        && primaryButtonState; // the value we got
            if (!device.isValid)
                invalidDeviceFound = true;
            if (primaryButtonState) {
                foreach (GameObject sculptPoint in sculptPoints) {
                    sculptPoint.transform.position = hit.point;
                }
            }
        }

    }

    private void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.05f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        // lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.03f, 0.03f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

}
