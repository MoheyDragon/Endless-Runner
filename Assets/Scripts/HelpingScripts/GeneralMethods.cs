using System.Linq;
using UnityEngine;
namespace MoheyGeneralMethods
{
    public static class GeneralMethods
    {
        public static T[] GetShuffledArray<T>(T[] arrayToShuffle)
        {
            T[] shuffledArray = arrayToShuffle.ToArray();
            for (int i = shuffledArray.Length - 1; i > 0; i--)
            {
                int elementIndex = Random.Range(0, i + 1);
                T temp = shuffledArray[i];
                shuffledArray[i] = shuffledArray[elementIndex];
                shuffledArray[elementIndex] = temp;
            }
            return shuffledArray;
        }
        public static int[] GetShuffledIndexes(int length)
        {
            int[] indexes = Enumerable.Range(0, length).ToArray();

            for (int i = indexes.Length - 1; i > 0; i--)
            {
                int elementIndex = Random.Range(0, i + 1);
                int temp = indexes[i];
                indexes[i] = indexes[elementIndex];
                indexes[elementIndex] = temp;
            }
            return indexes;
        }
        public static (T[] shuffledArray, int[] originalIndexes) GetShuffledArrayWithIndexes<T>(T[] arrayToShuffle)
        {
            T[] shuffledArray = arrayToShuffle.ToArray();
            int[] originalIndexes = Enumerable.Range(0, arrayToShuffle.Length).ToArray();

            for (int i = shuffledArray.Length - 1; i > 0; i--)
            {
                int elementIndex = Random.Range(0, i + 1);

                T tempElement = shuffledArray[i];
                shuffledArray[i] = shuffledArray[elementIndex];
                shuffledArray[elementIndex] = tempElement;

                int tempIndex = originalIndexes[i];
                originalIndexes[i] = originalIndexes[elementIndex];
                originalIndexes[elementIndex] = tempIndex;
            }

            return (shuffledArray, originalIndexes);
        }
        public static T[] PopulateArrayFromParent<T>(Transform parent)
        {
            int childs = parent.childCount;
            T[] newArray = new T[childs];
            for (int i = 0; i < childs; i++)
            {
                newArray[i] = parent.GetChild(i).GetComponent<T>();
            }
            return newArray;
        }
        public static GameObject[] PopulateArrayFromParent(Transform parent)
        {
            int childs = parent.childCount;
            GameObject[] newArray = new GameObject[childs];
            for (int i = 0; i < childs; i++)
            {
                newArray[i] = parent.GetChild(i).gameObject;
            }
            return newArray;
        }
        public static string ConvertToMinutesAndSeconds(float timeInSeconds)
        {
            int minutes = Mathf.FloorToInt(timeInSeconds / 60);
            float seconds = timeInSeconds % 60;
            return string.Format("{0:00}:{1:00.00}", minutes, seconds);
        }

    }
}