using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIUploader : MonoBehaviour
{
    private string apiURL = "https://somnia-streams-hub.vercel.app/api/publish";



    public LeaderboardManager leaderboardManager;
    public GameObject EndPanel;

    /// <summary>
    /// Call this function on game end
    /// </summary>
    public void SendDataToSomnia()
    {
        string wallet = PlayerPrefs.GetString("WALLET_ADDRESS", "0xce73c2729bc87c4e5759e28a1fad6c3d603a8169");

        float playTime = Mathf.Round(TimerController.Instance.GetCurrentTime());
        int score = ScoreManager.score; // using uploaded file

        Debug.Log("📤 Sending To API -> wallet: " + wallet + " | score: " + score + " | time: " + playTime);

        StartCoroutine(SendData(wallet, score, playTime));
    }

    IEnumerator SendData(string wallet, int score, float playTime)
    {
        string jsonBody = "{\"player\":\"" + wallet + "\",\"score\":\"" + score + "\",\"playTime\":\"" + playTime + "\"}";
        Debug.Log("📤 JSON Body: " + jsonBody);

        UnityWebRequest request = new UnityWebRequest(apiURL, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // 🔐 IMPORTANT: Add your secret key into header
       // request.SetRequestHeader("SECRET_API_KEY", "sk_live_7f8a9b2c4d5e6f1a8b9c0d1e2f3a4b5c6d7e8f9a0b1c2d3e4f5a6b7c8d9e0f1a");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            leaderboardManager.FetchLeaderboard();
            EndPanel.SetActive(true);
            Debug.Log("✅ Upload Success: " + request.downloadHandler.text);
        }
            
        

        else
            Debug.LogError("❌ Upload Failed: " + request.error + " | Response: " + request.downloadHandler.text);
    }


}
