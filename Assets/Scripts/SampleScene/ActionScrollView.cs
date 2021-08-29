using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System;

public class ActionScrollView : MonoBehaviour
{
    [SerializeField]
    Text text;

    void Start()
    {
        ActionApplication actionApp = new ActionApplication();

        actionApp.CompleteHandler += (
            html => Debug.Log("ActionApplication:\n" + html)
        );

        actionApp.CompleteHandler += UpdateTextCallback;

        actionApp.GetHtmlAsync();
    }

    void UpdateTextCallback(string html)
    {
        text.text = html;
    }
}

public class ActionApplication
{
    // eventのコールバックは = での代入ができない
    public event Action<string> CompleteHandler;
 
    public async void GetHtmlAsync()
    {
        HttpClient client = new HttpClient();
        var result = await client.GetStringAsync("https://example.com/");
        CompleteHandler?.Invoke(result);
    }
}
