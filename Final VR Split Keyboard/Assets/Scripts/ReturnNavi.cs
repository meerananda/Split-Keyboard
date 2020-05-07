using UnityEngine;
using UnityEngine.UI;

namespace UI.CustomSelectable
{
    public class ReturnNavi : Button
    {
        [SerializeField]

        public override Selectable FindSelectableOnUp()
        {
            return base.FindSelectableOnRight();
        }


        public override Selectable FindSelectableOnLeft()
        {
            return base.FindSelectableOnRight();
        }
    }
}
