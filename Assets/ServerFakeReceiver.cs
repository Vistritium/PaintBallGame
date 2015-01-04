using Assets;
#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleJSON;
using UnityEngine;

public class ServerFakeReceiver : MonoBehaviour
{
    // private readonly Dictionary<float, JSONNode> fakeServerData = new Dictionary<float, JSONNode>();
    private readonly LinkedList<float> fakeServerTimes = new LinkedList<float>();
    private readonly LinkedList<JSONNode> fakeServerValues = new LinkedList<JSONNode>();

    public TextAsset serverData;
    private StateUpdater stateUpdater;

    // Use this for initialization
    private void Start()
    {
        // var serverData = (TextAsset) Resources.Load("server.txt");
        // Debug.Log(serverData);
        stateUpdater = GetComponent<StateUpdater>();
        JSONArray parsedJson = JSON.Parse(serverData.text).AsArray;
        for (int i = 0; i < parsedJson.Count; i++)
        {
            JSONNode data = parsedJson[i].AsObject[0];
            string resRegExp = Regex.Match(parsedJson[i].ToString(), "\"[^\"]*\"").Value.TrimEnd('"').TrimStart('"');
            string time = parsedJson.ToString();
            //        Debug.Log(string.Format("parsing {0} into {1} for node {2}",resRegExp,float.Parse(resRegExp),i));
            fakeServerTimes.AddLast(float.Parse(resRegExp));
            fakeServerValues.AddLast(data);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (fakeServerTimes.First != null && fakeServerTimes.First.Value < stateUpdater.TimeFromStart())
        {
            fakeServerTimes.RemoveFirst();
            JSONNode value = fakeServerValues.First.Value;
            fakeServerValues.RemoveFirst();
            Debug.Log("Sending: " + value.Value);
            SendMessage("SendJSon", value.Value);
        }
    }
}
#endif