using UnityEngine;

public class PlayerWarning : MonoBehaviour
{
    public SpriteRenderer warningImage;

    private void Start()
    {

        PlayerWarningTrigger.onWarnPlayer += ShowWarn;
        PlayerWarningTrigger.onNoWarnPlayer += HideWarn;

        warningImage.enabled = false;
    }

    private void OnDestroy()
    {
        PlayerWarningTrigger.onWarnPlayer -= ShowWarn;
        PlayerWarningTrigger.onNoWarnPlayer -= HideWarn;
    }

    private void ShowWarn()
    {
        if (warningImage != null)
        {
            warningImage.enabled = true;
        }
    }

    private void HideWarn()
    {
        if (warningImage != null)
        {
            warningImage.enabled = false;
        }
    }
}
