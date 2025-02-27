using UnityEngine;
using MoheyGeneralMethods;
namespace EndlessRunner
{
    public class UIManager : Singletons<UIManager>
    {
        [SerializeField] Transform canvas;
        GameObject[] UiPage;
        protected override void Awake()
        {
            base.Awake();
            SetupPages();
        }
        private void SetupPages()
        {
            if (canvas == null) canvas = GameObject.Find("Canvas").transform;
            int childCount = canvas.childCount;
            UiPage = GeneralMethods.PopulateArrayFromParent(canvas);
            SwitchToPage(0);
        }
        public void SwitchToPage(int index)
        {
            for (int i = 0; i < UiPage.Length; i++)
                UiPage[i].SetActive(false);

            UiPage[index].SetActive(true);
        }
    }
}