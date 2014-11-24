using UnityEngine;
using System.Collections;

public enum ETransition
{
	NONE,
	IN,
	OUT,
	PAUSE,
	COUNT,
};

public class UITransition 
{
	public delegate bool DoState(float fTransitionTime, float fTransitionDuration);
	ETransition m_eTransitionMode = ETransition.NONE;
	float m_fTransitionTime = 0.0f;
	float m_fTransitionDuration = 0.0f;

	DoState OnEnter = null;
	DoState DoTransition = null;
	DoState OnExit = null;

    public UITransition()
    {

    }

	public UITransition(ETransition eMode, DoState enter, DoState doTrans, DoState exit, float fDuration)
	{
		Init(eMode, enter, doTrans, exit, fDuration);
	}

	public void Init(ETransition eMode, DoState enter, DoState doTrans, DoState exit, float fDuration)
	{
		m_eTransitionMode = eMode;
		SetDefaults(fDuration);
		
		OnEnter = enter;
		DoTransition = doTrans;
		OnExit = exit;

        if (OnEnter != null)
            OnEnter(m_fTransitionTime, m_fTransitionDuration);
        else if (DoTransition != null)
            DoTransition(m_fTransitionTime, m_fTransitionDuration);
        else if (OnExit != null)
            OnExit(m_fTransitionTime, m_fTransitionDuration);
	}

	public void SetDefaults(float fDuration)
	{
		if (m_eTransitionMode == ETransition.NONE || m_eTransitionMode == ETransition.IN || m_eTransitionMode == ETransition.PAUSE) 
		{
			m_fTransitionTime = 0.0f;
			m_fTransitionDuration = fDuration;
		} 
		else if(m_eTransitionMode == ETransition.OUT) 
		{
			m_fTransitionTime = fDuration;
			m_fTransitionDuration = fDuration;
		}
	}

	public ETransition GetTransitionMode()
	{
		return m_eTransitionMode;
	}

	public float GetTransitionTime()
	{
		return m_fTransitionTime;
	}

	public float GetTransitionDuration()
	{
		return m_fTransitionDuration;
	}

	public bool UpdateTransition()
	{
		if (m_eTransitionMode == ETransition.NONE || m_eTransitionMode == ETransition.COUNT)
			return true;
		
		if (m_eTransitionMode != ETransition.OUT) 
		{
			if(m_fTransitionTime >= m_fTransitionDuration)
			{
				if(OnExit != null)
					OnExit(m_fTransitionTime, m_fTransitionDuration);
				else
				{
					m_eTransitionMode = ETransition.NONE;
				}
			}

			m_fTransitionTime += Time.deltaTime;
			m_fTransitionTime = Mathf.Min(m_fTransitionTime, m_fTransitionDuration);
		}
		else 
		{
			if(m_fTransitionTime <= 0.0f)
			{
				if(OnExit != null)
					OnExit(m_fTransitionTime, m_fTransitionDuration);
				else
				{
					m_eTransitionMode = ETransition.NONE;
				}
			}

			m_fTransitionTime -= Time.deltaTime;	
			m_fTransitionTime = Mathf.Max(m_fTransitionTime, 0.0f);
		}

        if (DoTransition != null)
            DoTransition(m_fTransitionTime, m_fTransitionDuration);

		return false;
	}
}
