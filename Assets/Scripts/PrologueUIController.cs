using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueUIController : MonoBehaviour
{
    public Button nextButton;
    public Text prologueText;

    private int state = 0;
    private List<string> texts;
    private void Awake()
    {
        texts = new List<string>();
        texts.Add("あなたは餌を求めてどこか宇宙の遠いところ（アノマ）から地球にやってきたエイリアン（コズモ）です。" +
            "地球基準で言うと、あなたは凶悪な見た目のわりに戦闘能力がとても低いです。ニンゲンの子供にすら勝てません。");
        texts.Add("ところで、率直に申し上げてあなたは”グロい”です。だから、ニンゲンはあなたのことを良く思っていません。" +
            "”グロい”し”汚らしい”あなたに対して敵対心（Hostility）を持っています。このままでは成すすべなく駆除されてしまうでしょう。");
        texts.Add("でも、そんな”グロい”あなただからこそ同情（Sympathy）もされているのです。そこであなたは考えました。" +
            "生き残るためには、敵対心を上回る同情を稼げばよいのだと。そうすればニンゲンはあなたを生かしてくれるでしょう。");
        texts.Add("でもどうやって同情を稼げばよいのでしょう。あなたは一日に一度だけ、ニンゲンを捕食（Siphon）することができます。" +
            "捕食といっても肉体を食べてしまうわけではありません。ニンゲンが持つ生命エネルギーをカードとして奪うのです。");
        texts.Add("ニンゲンは一人一人が、良いとされるカードや悪いとされるカード、色々なカードを持っています。" +
            "ニンゲン界ではこういう神話があります。「人はみな配られたカードを持っている。そのカードでどうやりくりして生きるかを考えなさい」");
        texts.Add("ニンゲンからの同情を稼ぐにはこのカードを利用するのです。特に、ニンゲン界で「悪い」とされているカードはあなたにとっては価値があります。" +
            "例えば、もしあなたが「金持ち生まれ」ではなく「底辺生まれ」であれば、同情を稼げるからです。");
        texts.Add("カードは消費（Consume）するか売買することができます。消費すればそのカードが持つ”グロい”といった特性（Trait）はあなたのものになります。" +
            "消費した時点でのその特性に対する同情ポイントが手に入り、ゲーム中蓄積されていきます。");
        texts.Add("まとめると、ニンゲンからカードを奪って、カードをアノマ界で取引することによって、同情という資産を築いていく、投資シミュレーションゲームみたいなものですね！　" +
            "ちなみに今あなたが持っている”グロい”というカードは結構レアものなので、喜んでください！");

    }
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(()=>{
            state++;
            prologueText.text = GetText(state);
            if (state == 7)
            {
                nextButton.transform.GetChild(0).GetComponent<Text>().text = "ニンゲン界へ！";
                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene("SiphonScene");
                });
            
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string GetText(int state)
    {
        return texts[state];
    }


}
