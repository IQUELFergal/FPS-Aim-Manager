using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform transformToModify = null;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Vector3 targetTranslation;
    [SerializeField] private Vector3 targetRotation;
    [SerializeField] private Vector3 targetScaling;
    [SerializeField] private bool canGoBothWays;
    [SerializeField] private bool allowMultipleTransformations = true;
    [SerializeField] private Item item;
    [SerializeField] private int numbItemInSlot;
    private bool canBeTransformed = true;
    private Vector3 basePosition;
    private Vector3 baseRotation;
    private Vector3 baseScale;
    [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private backToShelter backToShelter;

    private IEnumerator TransformCoroutine;


    private void Start()
    {
        if (transformToModify == null)
        {
            transformToModify = transform;
        }
        basePosition = transformToModify.localPosition;
        baseRotation = transformToModify.localEulerAngles;
        baseScale = transformToModify.localScale;
    }

    public string InteractionString => "Pick up";

    public void Interact(Interactor interactor)
    {
        if (canBeTransformed)
        {
            if (TransformCoroutine != null) StopCoroutine(nameof(Transform));
            interactor.GetComponent<Inventory>().AddItem(new ItemSlot(item, numbItemInSlot));
            TransformCoroutine = Transform(duration);
            StartCoroutine(TransformCoroutine);
        }
    }

    IEnumerator Transform(float duration)
    {
        canBeTransformed = false;
        Vector3 endPosition = basePosition + targetTranslation;
        Vector3 endRotation = baseRotation + targetRotation;
        Vector3 endScale = baseScale + targetScaling;

        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float interp = curve.Evaluate(t / duration);
            Vector3 newPosition = Vector3.Lerp(basePosition, endPosition, interp);
            Vector3 newRotation = Vector3.Lerp(baseRotation, endRotation, interp);
            Vector3 newScale = Vector3.Lerp(baseScale, endScale, interp);
            transformToModify.localPosition = newPosition;
            transformToModify.localEulerAngles = newRotation;
            transformToModify.localScale = newScale;
            yield return null;
        }
        if (canGoBothWays)
        {
            targetTranslation = -targetTranslation; 
            targetRotation = -targetRotation;
            targetScaling = -targetScaling; 
        }
        if (allowMultipleTransformations)
        {
            basePosition = endPosition;
            baseRotation = endRotation;
            baseScale = endScale;
            canBeTransformed = true;
        }
    }
}
