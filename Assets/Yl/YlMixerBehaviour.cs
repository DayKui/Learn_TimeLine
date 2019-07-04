using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class YlMixerBehaviour : PlayableBehaviour
{
    float m_DefaultVolume;

    AudioSource m_TrackBinding;
    bool m_FirstFrameHappened;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        m_TrackBinding = playerData as AudioSource;

        if (m_TrackBinding == null)
            return;

        if (!m_FirstFrameHappened)
        {
            m_DefaultVolume = m_TrackBinding.volume;
            m_FirstFrameHappened = true;
        }

        int inputCount = playable.GetInputCount ();

        float blendedVolume = 0f;
        float totalWeight = 0f;
        float greatestWeight = 0f;
        int currentInputs = 0;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<YlBehaviour> inputPlayable = (ScriptPlayable<YlBehaviour>)playable.GetInput(i);
            YlBehaviour input = inputPlayable.GetBehaviour ();
            
            blendedVolume += input.volume * inputWeight;
            totalWeight += inputWeight;

            if (inputWeight > greatestWeight)
            {
                greatestWeight = inputWeight;
            }

            if (!Mathf.Approximately (inputWeight, 0f))
                currentInputs++;
        }

        m_TrackBinding.volume = blendedVolume + m_DefaultVolume * (1f - totalWeight);
    }

    public override void OnPlayableDestroy (Playable playable)
    {
        m_FirstFrameHappened = false;

        if (m_TrackBinding == null)
            return;

        m_TrackBinding.volume = m_DefaultVolume;
    }
}
