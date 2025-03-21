using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DefaultEventHandling : MonoBehaviour
{
    public GameObject Event; // Reference to the parent GameObject
    private bool meshColliderEnabled = true;
    List<GameObject> hoverObjs = new List<GameObject>();
    private int temp = 0; //DEBUGGING

    // Start is called before the first frame update
    void Start()
    {
        //// Start processing from the Event GameObject's children
        //ProcessChildren(Event.transform, 0); // Start with the top level (0)

        //// Start the coroutine to trigger the headset debugger after 10 seconds
        //StartCoroutine(TriggerHeadsetDebuggerAfterDelay());
    }
    // Method to recursively process children, grandchildren, and great-grandchildren
    void ProcessChildren(Transform parentTransform, int currentLevel)
    {
        // If we've processed 3 levels, stop further processing
        if (currentLevel > 3) return;

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform child = parentTransform.GetChild(i);  // Get child Transform

            // Check if the current object (child, grandchild, etc.) is named "default"
            if (child.gameObject.name.ToLower() == "default")
            {
                // Add the required components to the object named "default"
                AddComponents(child.gameObject);
                
            }

            // Now, process the children of this object (grandchildren, etc.)
            ProcessChildren(child, currentLevel + 1);
        }
    }

    // Method to add components to a GameObject if they are not already added
    void AddComponents(GameObject targetObject)
    {
        // Only add components if they are not already added... andrew changed this to be a bit more specific just in case 
        if (targetObject.GetComponent<MeshCollider>() == null)
        {
            targetObject.AddComponent<MeshCollider>();
        }
        if (targetObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>() == null)
        {
            targetObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        }
        if (targetObject.GetComponent<hoverOBJ>() == null)
        {
            targetObject.AddComponent<hoverOBJ>();
            //// DEBUGGING
            //HeadsetDebuggerText(targetObject, temp);
            //temp += 1;
        }
        hoverObjs.Add(targetObject);
    }
    
    public void ToggleBtnOBJHover()
    {
        meshColliderEnabled = !meshColliderEnabled; // Flip the state
        foreach (var item in hoverObjs)
        {
            MeshCollider meshCollider = item.GetComponent<MeshCollider>();

            if (meshCollider != null) // If a MeshCollider exists
            {
                meshCollider.enabled = meshColliderEnabled; // Enable/Disable MeshCollider based on the state
                print("bruh");
            }
        }

    }

    IEnumerator TriggerHeadsetDebuggerAfterDelay()
    {
        yield return new WaitForSeconds(10f); // Wait for 10 seconds
        Debug.Log("Triggering headset debugger after 10 seconds.");

        // Trigger your HeadsetDebuggerText method here. 
        // Assuming the targetObject and temp values are relevant to the method.
        foreach (var item in hoverObjs)
        {
            HeadsetDebuggerText(item, temp);
            temp += 1;
        }
    }

    void HeadsetDebuggerText(GameObject targetObject, int i)
    {
        // Get the parent of the GameObject to which hoverOBJ was added
        Transform parentTransform = targetObject.transform.parent;

        if (parentTransform != null)
        {
            // Create a new GameObject for the 3D TextMesh Pro
            GameObject textObject = new GameObject("ComponentCheckText");

            // Add a TextMeshPro component to the new GameObject (this will render 3D text)
            TextMeshPro textMesh = textObject.AddComponent<TextMeshPro>();

            // Check if the required components are attached
            hoverOBJ hoverObjComponent = targetObject.GetComponent<hoverOBJ>();
            MeshCollider meshColliderComponent = targetObject.GetComponent<MeshCollider>();
            UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable xrSimpleInteractableComponent = targetObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();

            // Check if the MeshCollider is enabled (making it interactable)
            bool isMeshColliderInteractable = meshColliderComponent != null && meshColliderComponent.enabled;

            // Generate the message to display
            string message = $"Components for {parentTransform.name}:\n";
            message += $"hoverOBJ: {(hoverObjComponent != null ? "Attached" : "Not Attached")}\n";
            message += $"MeshCollider: {(meshColliderComponent != null ? "Attached" : "Not Attached")}\n";
            message += $"MeshCollider Interactable: {(isMeshColliderInteractable ? "Enabled" : "Disabled")}\n";
            message += $"XRSimpleInteractable: {(xrSimpleInteractableComponent != null ? "Attached" : "Not Attached")}\n";

            // Set the text to the message
            textMesh.text = message;
            textMesh.fontSize = 1.5f; // Set the font size (adjust to your preference)
            textMesh.color = Color.white; // Set the text color
            textMesh.alignment = TextAlignmentOptions.Center;  // Center the text

            // Position the text in the 3D world (adjust the offset to position the text relative to the object)
            textObject.transform.position = targetObject.transform.position + Vector3.up * 1.5f + new Vector3(i, i, i); // 1.5f above the object
        }
    }

    //andrew stuff
    //public GameObject allObjectsInScene;
    //private List<GameObject> AllChilds(GameObject root)
    //{
    //    List<GameObject> result = new List<GameObject>();
    //    if (root.transform.childCount > 0)
    //    {
    //        foreach (Transform VARIABLE in root.transform)
    //        {
    //            Searcher(result, VARIABLE.gameObject);
    //        }
    //    }
    //    return result;
    //}

    //private void Searcher(List<GameObject> list, GameObject root)
    //{
    //    list.Add(root);
    //    if (root.transform.childCount > 0)
    //    {
    //        foreach (Transform VARIABLE in root.transform)
    //        {
    //            Searcher(list, VARIABLE.gameObject);
    //        }
    //    } 
    //}
    // end of andrew stuff
}
