using UnityEngine;
using MoheyGeneralMethods;
using TMPro;
namespace EndlessRunner
{
    public class UIManager : Singletons<UIManager>
    {
        [SerializeField] Transform canvas;
        [SerializeField] TextMeshProUGUI[] scoreTexts;
        [SerializeField] TextMeshProUGUI[] highScoreTexts;
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
        public void Setup(int score,int highScore)
        {
            UpdateScore(score);
            UpdateHighScore(highScore);
        }
        public void UpdateScore(int score,bool updateHighScore=false)
        {
            foreach (TextMeshProUGUI text in scoreTexts)
                text.text = score.ToString();
            if (updateHighScore)
                UpdateHighScore(score);
        }
        private void UpdateHighScore(int highScore)
        {
            foreach (TextMeshProUGUI text in highScoreTexts)
                text.text = highScore.ToString();
        }
    }
}