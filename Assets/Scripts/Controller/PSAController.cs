using Microsoft.MixedReality.Toolkit.UX;
using UnityEngine;

namespace Controller
{
    public class PSAController : MonoBehaviour
    {
        [SerializeField] private Dialog psaPrefab;
        [SerializeField] private Dialog confirmationPrefab;
        private int i = 1;


        public void Start()
        {
        }


        public void DisplayPsa()
        {
            if (i < 100)
            {
                Dialog.InstantiateFromPrefab(psaPrefab,
                    new DialogProperty("Alert " + i,
                        "This is an example of an Alert with a choice message for the user",
                        DialogButtonHelpers.OK), true, true);
                i++;
            }
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