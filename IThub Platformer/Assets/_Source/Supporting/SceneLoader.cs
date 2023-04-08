using UnityEngine;
using UnityEngine.SceneManagement;

namespace Supporting
{
    public class SceneLoader
    {


        public static void NextLevel()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            if(activeScene.buildIndex < SceneManager.sceneCount-1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Debug.Log("��������� ����� �����������"); // �������� ������ ������ � ���� � �.�.
            }
        }

        public static void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

