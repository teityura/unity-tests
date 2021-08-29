using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;

public class EventScrollView : MonoBehaviour
{
    [SerializeField]
    Text text;

    void Start()
    {
        EventApplication eventApp = new EventApplication();

        // = での代入が不可になり、
        // += か -= しか利用できないので、下記はエラーになる
        // eventApp.CompleteHandler = UpdateTextCallback;

        eventApp.CompleteHandler += (
            html => Debug.Log("EventApplication:\n" + html)
        );
        eventApp.CompleteHandler += UpdateTextCallback;

        eventApp.GetHtmlAsync();
    }

    void UpdateTextCallback(string html)
    {
        text.text = html;
    }
}

public class EventApplication
{
    // eventのコールバックは = での代入ができない
    public delegate void OnCompleteDelegate(string html);
    public event OnCompleteDelegate CompleteHandler; // event修飾子を追加

    public async void GetHtmlAsync()
    {
        HttpClient client = new HttpClient();
        var result = await client.GetStringAsync("https://example.com/");

        CompleteHandler?.Invoke(result);
    }
}
