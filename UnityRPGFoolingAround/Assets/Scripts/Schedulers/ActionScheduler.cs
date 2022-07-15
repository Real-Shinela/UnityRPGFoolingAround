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
                print("Cancelling" + currAct);
                currAct.Cancel();
            }
            currAct = action;
        }
    }
}