using UnityEngine;

public class LoopEffects : MonoBehaviour
{
    public Light[] lights;
    public AudioSource radio;
    public GameObject ghost;

    void OnEnable()
    {
        if (LoopManager.Instance != null) LoopManager.Instance.OnLoopChanged += ApplyLoop;
    }

    void OnDisable()
    {
        if (LoopManager.Instance != null) LoopManager.Instance.OnLoopChanged -= ApplyLoop;
    }

    void ApplyLoop(int loop)
    {
        Debug.Log("Aplicando efectos para loop " + loop);
        switch (loop)
        {
            case 1:
                // ambiente normal
                ghost.SetActive(false);
                break;
            case 2:
                // hacer que la radio suene y empezar parpadeo
                if (radio) radio.Play();
                StartFlicker(true);
                break;
            case 3:
                ghost.SetActive(true);
                break;
            case 4:
                // intensificar efectos
                break;
            case 5:
                // final
                break;
        }
    }

    void StartFlicker(bool on)
    {
        foreach (var l in lights)
        {
            var flick = l.GetComponent<LightFlicker>();
            if (flick) flick.enabled = on;
        }
    }
}
