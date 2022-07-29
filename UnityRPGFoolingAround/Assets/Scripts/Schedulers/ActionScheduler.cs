using UnityEngine;


namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currAct;

        public void StartAction(IAction action)
        {
            if (currAct == action) return;
            if (currAct != null)
            {
                currAct.Cancel();
            }
            currAct = action;
        }

        public void CancelCurrAction()
        {
            StartAction(null);
        }
    }
}