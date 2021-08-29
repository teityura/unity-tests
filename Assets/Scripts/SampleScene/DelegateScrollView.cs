using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;

public class DelegateScrollView : MonoBehaviour
{
    [SerializeField]
    Text text;

    void Start()
    {
        DelegateApplication delegateApp = new DelegateApplication();

        // html取得後に、Logを出力させるようにしておく
        delegateApp.CompleteHandler = (
            html => Debug.Log("DelegateApplication:\n" + html)
        );

        // html取得後に、Textを更新させるようにしておく
        delegateApp.CompleteHandler += UpdateTextCallback;
        
        // リクエストを投げる(内部でコールバックを実行する)
        delegateApp.GetHtmlAsync();
    }

    void UpdateTextCallback(string html)
    {
        text.text = html;
    }
}

public class DelegateApplication
{
    // eventのコールバックは = での代入ができる
    public delegate void OnCompleteDelegate(string html);
    public OnCompleteDelegate CompleteHandler;

    public async void GetHtmlAsync()
    {
        HttpClient client = new HttpClient();
        var result = await client.GetStringAsync("https://example.com/");
        
        // Webサイトからhtmlを取得した後に
        // CompleteHandler に登録した関数を処理を実行するようにしておく
        CompleteHandler?.Invoke(result);

        // CompleteHandler?.Invoke(result); は
        // if(CompleteHandler != null) CompleteHandler(result); と同義
        // CompleteHandler に result を渡して コールバック実行する
        // コールバックの中身は、別の場所で定義する前提になってる
    }
}
