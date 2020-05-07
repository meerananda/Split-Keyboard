using UnityEngine;
using UnityEngine.UI;

namespace UI.CustomSelectable
{
    public class CustomizedNavigation : Button
    {
        [SerializeField]
        //private Selectable upSelectable;

        //[SerializeField]
        //private Selectable downSelectable;

        //[SerializeField]
        //private Selectable leftSelectable;

        //[SerializeField]
        //private Selectable rightSelectable;


        public override Selectable FindSelectableOnUp()
        {
            //if (navigation.mode == Navigation.Mode.Automatic || base.FindSelectableOnUp() == null)
            //if(base.FindSelectableOnUp() == null)
            //{
            //    if (base.FindSelectableOnLeft() == null)
            //        return base.FindSelectableOnRight();
            //    return base.FindSelectableOnLeft();
            //}
            //return base.FindSelectableOnUp();
            return base.FindSelectableOnLeft();
        }


        public override Selectable FindSelectableOnRight()
        {
            return base.FindSelectableOnLeft();
        }




    }
}
