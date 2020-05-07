using UnityEngine;
using UnityEngine.UI;

namespace UI.CustomSelectable
{
    public class SpaceNavi : Button
    {
        //[SerializeField]


        public override Selectable FindSelectableOnDown()
        {
            return base.FindSelectableOnUp().FindSelectableOnDown();
        }

        public override Selectable FindSelectableOnLeft()
        {
            return base.FindSelectableOnUp();
        }

        public override Selectable FindSelectableOnRight()
        {
            return base.FindSelectableOnUp();
        }
    }
}
