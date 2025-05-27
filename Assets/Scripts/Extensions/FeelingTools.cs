using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class FeelingTools
{
    public static void ChangeAlpha(SpriteRenderer spriteRenderer, float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    public static void FadeIn(Image image, float fadeTime = 1f, float targetAlpha = 1)
    {
        image.gameObject.SetActive(true);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        image.StartCoroutine(FadeInCoroutine(image, fadeTime, targetAlpha));
    }

    public static IEnumerator FadeInCoroutine(Image image, float fadeTime, float targetAlpha = 1f, System.Action callBack = null)
    {
        float elapsedTime = 0f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        Color startColor = image.color;
        image.gameObject.SetActive(true);
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            image.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeTime);
            yield return null;
        }
        if (callBack != null)
        {
            callBack.Invoke();
        }
        image.color = targetColor;
    }
    public static IEnumerator FadeInCoroutine(SpriteRenderer spriteRenderer, float fadeTime, float targetAlpha = 1f)
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
        spriteRenderer.gameObject.SetActive(true);

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, targetAlpha, elapsedTime / fadeTime);
            spriteRenderer.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        spriteRenderer.color = color;
    }
    public static IEnumerator FadeOutCoroutine(Image image, float fadeTime, float targetAlpha = 0f, System.Action callBack = null)
    {
        float elapsedTime = 0f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            image.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeTime);
            yield return null;
        }
        if (callBack != null)
        {
            callBack.Invoke();
        }
        image.gameObject.SetActive(false);
    }
    public static IEnumerator FadeOutCoroutine(SpriteRenderer spriteRenderer, float fadeTime, float targetAlpha = 0f)
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1, targetAlpha, elapsedTime / fadeTime);
            spriteRenderer.color = color;
            yield return null;
        }
        spriteRenderer.gameObject.SetActive(false);
    }
    public static void FlipZ(Transform transform)
    {
        Vector3 scale = transform.localScale;
        scale.z *= -1;
        transform.localScale = scale;
    }
    public static void FlipZ(Transform transform, float newZ = 180)
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.z = newZ;
        transform.eulerAngles = rotation;
    }

    public static void FlipY(Transform transform)
    {
        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }
    public static void FlipY(Transform transform, float newY = 180)
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = newY;
        transform.eulerAngles = rotation;
    }
    public static IEnumerator LerpPosition(Transform obj, Vector2 target, float duration = 0.5f)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            obj.position = Vector2.Lerp(obj.position, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.position = target;
    }
    public static bool RandomChance(float percent)
    {
        return UnityEngine.Random.Range(0f, 100f) < percent;
    }

    public static Vector3 ConvertUIToWorldPosition(RectTransform uiElement, Camera worldCamera)
    {
        Vector3 worldPosition = Vector3.zero;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(uiElement, uiElement.position, worldCamera, out worldPosition))
        {
            return worldPosition;
        }
        return Vector3.zero;
    }
    public static Vector3 ConvertUIToWorldPositionCamera(RectTransform uiElement, Camera uiCamera)
    {
        if (uiElement == null || uiCamera == null)
            return Vector3.zero;

        Vector3 screenPosition = uiCamera.WorldToScreenPoint(uiElement.position);

        Vector3 worldPosition;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(uiElement, screenPosition, uiCamera, out worldPosition))
        {
            return worldPosition;
        }

        return Vector3.zero;
    }


    public static Vector3 ConvertUIToWorldPosition(Image image, Camera worldCamera)
    {
        var uiElement = image.GetComponent<RectTransform>();
        Vector3 worldPosition = Vector3.zero;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(uiElement, uiElement.position, worldCamera, out worldPosition))
        {
            return worldPosition;
        }
        return Vector3.zero;
    }
    public static IEnumerator ZoomInCoroutine(RectTransform rectTransform, float targetScale, float targetTime = 0.5f, System.Action onComplete = null)
    {
        rectTransform.localScale = Vector3.zero;
        rectTransform.gameObject.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < targetTime)
        {
            elapsedTime += Time.deltaTime;
            rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * targetScale, elapsedTime / targetTime);
            yield return null;
        }
        onComplete?.Invoke();
        rectTransform.gameObject.SetActive(false);
    }
    public static IEnumerator ZoomInCoroutine(Transform transform, float targetScale, float targetTime = 0.5f, System.Action onComplete = null)
    {
        float elapsedTime = 0f;
        Vector2 startScale = transform.localScale;
        while (elapsedTime < targetTime)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, Vector3.one * targetScale, elapsedTime / targetTime);
            yield return null;
        }
        onComplete?.Invoke();
        transform.gameObject.SetActive(false);
    }
    public static IEnumerator ZoomOutCoroutine(Transform transform, float targetScale, float targetTime = 0.5f)
    {
        transform.gameObject.SetActive(true);
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;
        while (elapsedTime < targetTime)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, Vector3.one * targetScale, elapsedTime / targetTime);
            yield return null;
        }
        transform.localScale = Vector3.one * targetScale;
    }
    public static IEnumerator MoveToTarget(RectTransform image, float time, Vector3 targetPos, System.Action onComplete = null, float overshootAmount = 30f)
    {
        Vector3 startPos = image.anchoredPosition;
        float overshootTime = time * 0.3f;
        float returnTime = time * 0.3f;

        // Tính điểm vượt quá mục tiêu
        Vector3 direction = (targetPos - startPos).normalized;
        Vector3 overshootPos = targetPos + direction * overshootAmount;

        // Đi tới vị trí vượt quá
        float elapsed = 0f;
        while (elapsed < overshootTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / overshootTime);
            image.anchoredPosition = Vector3.Lerp(startPos, overshootPos, t);
            yield return null;
        }

        // Quay lại vị trí mục tiêu
        elapsed = 0f;
        Vector3 currentPos = image.anchoredPosition;
        while (elapsed < returnTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / returnTime);
            image.anchoredPosition = Vector3.Lerp(currentPos, targetPos, t);
            yield return null;
        }

        image.anchoredPosition = targetPos;
        onComplete?.Invoke();
    }


    public static IEnumerator MoveToTarget(Transform transform, float time, Vector3 targetPos, System.Action onComplete = null)
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / time);
            yield return null;
        }
        transform.position = targetPos;
        onComplete?.Invoke();
    }
    public static IEnumerator Rotate(RectTransform rectTransform, float time, float rotSpeed = 720f)
    {
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            //rotate z
            float deltaRotation = rotSpeed * Time.deltaTime;
            rectTransform.Rotate(0f, 0f, deltaRotation); 
            //
            yield return null;
        }
    }
    public static IEnumerator ZoomInUI(RectTransform rectTransform, float startScale, float targetScale, float targetTime = 0.5f,float overScale = 1.2f , System.Action onComplete = null)
    {
        rectTransform.gameObject.SetActive(true);
        float halfTime = targetTime / 2f;
        float t = 0f;
        while (t < halfTime)
        {
            t += Time.deltaTime;
            float lerp = t / halfTime;
            float scale = Mathf.Lerp(startScale, overScale, lerp);
            rectTransform.localScale = Vector3.one * scale;
            yield return null;
        }
        t = 0f;
        while (t < halfTime)
        {
            t += Time.deltaTime;
            float lerp = t / halfTime;
            float scale = Mathf.Lerp(overScale, targetScale, lerp);
            rectTransform.localScale = Vector3.one * scale;
            yield return null;
        }
        rectTransform.localScale = Vector3.one * targetScale;
        onComplete?.Invoke();
    }
    public static IEnumerator ZoomInList(List<RectTransform> listUI, float delayBetween = 0.05f, float startScale = 0f
        , float targetScale = 1f, float targetTime = 0.25f, float overScale = 1.2f)
    {
        foreach (var rect in listUI)
        {
            rect.localScale = Vector3.one * startScale;
            CoroutineHelper.Instance.StartCoroutine(ZoomInUI(rect, startScale, targetScale, targetTime, overScale));
            yield return new WaitForSeconds(delayBetween);
            delayBetween *= 0.9f;
        }
    }


    public static IEnumerator FlashSpriteCoroutine(SpriteRenderer spriteRenderer, float time)
    {
        if (spriteRenderer != null)
        {
            var mat = spriteRenderer.material;
            if (mat != null && mat.HasProperty("_flash"))
            {
                float elapsedTime = 0;
                float duration = time;
                float flashPeak = 0.4f;
                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float t = elapsedTime / duration;

                    float flashValue = Mathf.Sin(t * Mathf.PI) * flashPeak;

                    mat.SetFloat("_flash", flashValue);
                    yield return null;
                }

                mat.SetFloat("_flash", 0);
            }
        }
    }
    public static IEnumerator FlashSpriteCoroutine(Image image, float time, Action callBack = null)
    {
        if (image != null)
        {
            image.material = new Material(image.material);
            var mat = image.material;

            if (mat != null && mat.HasProperty("_flash"))
            {
                float elapsedTime = 0f;
                float flashPeak = 0.4f;

                while (elapsedTime < time)
                {
                    elapsedTime += Time.deltaTime;
                    float t = elapsedTime / time;
                    float flashValue = Mathf.Sin(t * Mathf.PI) * flashPeak;
                    mat.SetFloat("_flash", flashValue);
                    yield return null;
                }

                mat.SetFloat("_flash", 0);
            }
        }
        callBack?.Invoke();
    }
    public static IEnumerator ShowPanelWithBounceEffect(RectTransform panel, float bounceDistance = 25f, float duration = 0.2f, System.Action onComplete = null)
    {
        if (panel == null)
        {
            Debug.LogError("Panel is null in ShowPanelWithBounceEffect.");
            yield break;
        }

        // Ensure the panel is active
        panel.gameObject.SetActive(true);

        // Calculate the original and target positions
        Vector3 originalPosition = panel.anchoredPosition;
        Vector3 downPosition = originalPosition - new Vector3(0, bounceDistance, 0);

        // First half: Move down
        float halfDuration = duration / 2f;
        float elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / halfDuration);
            panel.anchoredPosition = Vector3.Lerp(originalPosition, downPosition, t);
            yield return null;
        }

        // Second half: Move back up
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / halfDuration);
            panel.anchoredPosition = Vector3.Lerp(downPosition, originalPosition, t);
            yield return null;
        }

        // Ensure the panel is exactly at the original position
        panel.anchoredPosition = originalPosition;

        // Invoke the callback if provided
        onComplete?.Invoke();
    }


}
