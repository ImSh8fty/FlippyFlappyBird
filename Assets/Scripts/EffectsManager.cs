using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public Player player;
    public Transform worldRoot;

    private float timer;
    private float currentPhaseDuration;
    private enum EffectState { Normal, UpsideDown }
    private EffectState currentState;

    private bool isRunning = false;

    private void Update()
    {
        if (!isRunning) return;

        timer += Time.deltaTime;

        if (timer >= currentPhaseDuration)
        {
            AdvancePhase();
        }
    }

    public void StartCycle()
    {
        isRunning = true;
        timer = 0f;

        // Start in normal mode, give a proper delay before flipping
        if (currentState == EffectState.Normal)
        {
            SetState(EffectState.Normal, Random.Range(6f, 10f));
        }
        else
        {
            AdvancePhase();
        }
    }

    public void StopCycle()
    {
        isRunning = false;
        // Leave screen as-is (flipped or not)
    }

    public void ResetToNormal()
    {
        currentState = EffectState.Normal;
        timer = 0f;
        SetState(EffectState.Normal, Random.Range(6f, 10f));
    }

    private void AdvancePhase()
    {
        if (currentState == EffectState.Normal)
        {
            SetState(EffectState.UpsideDown, Random.Range(4f, 6f));
        }
        else
        {
            SetState(EffectState.Normal, Random.Range(6f, 10f));
        }
    }

    private void SetState(EffectState newState, float duration)
    {
        currentState = newState;
        currentPhaseDuration = duration;
        timer = 0f;

        switch (newState)
        {
            case EffectState.Normal:
                player.SetUpsideDown(false);
                player.SetGravityFlipped(false);
                FlipVisual(false);
                break;

            case EffectState.UpsideDown:
                player.SetUpsideDown(true);
                player.SetGravityFlipped(true);
                FlipVisual(true);
                break;
        }
    }

    private void FlipVisual(bool flip)
    {
        if (worldRoot != null)
        {
            worldRoot.localScale = new Vector3(1f, flip ? -1f : 1f, 1f);
        }
    }
}
