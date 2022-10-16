using Microsoft.MixedReality.Toolkit.UX;
using UnityEngine;

namespace Controller
{
    public class PSAController : MonoBehaviour
    {
        [SerializeField] private Dialog psaPrefab;
        [SerializeField] private Dialog confirmationPrefab;
        private int i = 1;
        private string newMessage;


        public void Start()
        {
        }


        public void DisplayPsa()
        {
            if (i < 5)
            {
                Dialog.InstantiateFromPrefab(psaPrefab,
                    new DialogProperty("Alert " + i,
                        NewMessage(),
                        DialogButtonHelpers.OK), true, true);
                i++;
            }
        }

        private string NewMessage()
        {
            if (i == 1)
            {
                newMessage = " This is the first alert to tell you about some warning";
            }
            if (i == 2)
            {
                newMessage = " This is the second alert ";
            }
            if (i == 3)
            {
                newMessage = " This is the third alert but its fine";
            }
            if (i == 4)
            {
                newMessage = " This is the fourth alert but Gate C is closed";
            }
            return newMessage;
        }


        public void DisplayConfimation()
        {
            Dialog.InstantiateFromPrefab(confirmationPrefab,
                new DialogProperty("Confirmation",
                    "This is an example of a Confirmation Alert with a choice message for the user, placed at near interaction range",
                    DialogButtonHelpers.YesNo), true, true);
        }
    }
}