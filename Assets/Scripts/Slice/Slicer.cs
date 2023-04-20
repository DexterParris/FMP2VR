using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
    public LayerMask sliceMask;
    public bool isTouched;

    private void Update()
    {
        if (isTouched == true)
        {
            StartCoroutine(SliceDebounce());
        }
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        obj.layer = LayerMask.NameToLayer("Sliceable");
        XRGrabInteractable grabber = obj.AddComponent<XRGrabInteractable>();

        grabber.movementType = XRBaseInteractable.MovementType.VelocityTracking;

    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }


    IEnumerator SliceDebounce()
    {

        isTouched = false;
        Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);

        foreach (Collider objectToBeSliced in objectsToBeSliced)
        {
            SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, materialAfterSlice);

            GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);
            GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

            upperHullGameobject.transform.position = objectToBeSliced.transform.position;
            lowerHullGameobject.transform.position = objectToBeSliced.transform.position;

            MakeItPhysical(upperHullGameobject);
            MakeItPhysical(lowerHullGameobject);

            Destroy(objectToBeSliced.gameObject);
        }
        yield return new WaitForSeconds(0.3f);
        
    }

}
