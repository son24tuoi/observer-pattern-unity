#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace One.Pattern.Observer
{
    [CustomEditor(typeof(EventManager))]
    public class EventManagerEditor : Editor
    {
        private bool showWithoutData = true;
        private bool showWithData = true;

        private const string Key = "Key";
        private const string Amount = "Amount";

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EventManager manager = (EventManager)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("=== Debug Event Info ===", EditorStyles.boldLabel);

            // Sự kiện không có data
            showWithoutData = EditorGUILayout.Foldout(showWithoutData, $"Event Without Data [{GetCount(manager, false)}]");
            if (showWithoutData)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal("CN EntryBackOdd");
                GUILayout.Label("#", EditorStyles.boldLabel, GUILayout.Width(24));
                GUILayout.Label(Key, EditorStyles.boldLabel, GUILayout.ExpandWidth(true));
                GUILayout.Label(Amount, EditorStyles.boldLabel, GUILayout.Width(70));
                EditorGUILayout.EndHorizontal();

                var list = GetEventInfo(manager, false);
                for (int i = 0; i < list.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal(i % 2 == 0 ? "CN EntryBackEven" : "CN EntryBackOdd");
                    GUILayout.Label(i.ToString(), GUILayout.Width(24));
                    GUILayout.Label(list[i].Key, GUILayout.ExpandWidth(true));
                    GUILayout.Label(list[i].Count.ToString(), GUILayout.Width(70));
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            // Sự kiện có data
            showWithData = EditorGUILayout.Foldout(showWithData, $"Event With Data [{GetCount(manager, true)}]");
            if (showWithData)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal("CN EntryBackOdd");
                GUILayout.Label("#", EditorStyles.boldLabel, GUILayout.Width(24));
                GUILayout.Label(Key, EditorStyles.boldLabel, GUILayout.ExpandWidth(true));
                GUILayout.Label(Amount, EditorStyles.boldLabel, GUILayout.Width(70));
                EditorGUILayout.EndHorizontal();

                var list = GetEventInfo(manager, true);
                for (int i = 0; i < list.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal(i % 2 == 0 ? "CN EntryBackEven" : "CN EntryBackOdd");
                    // EditorGUILayout.BeginHorizontal();
                    GUILayout.Label(i.ToString(), GUILayout.Width(24));
                    GUILayout.Label(list[i].Key, GUILayout.ExpandWidth(true));
                    GUILayout.Label(list[i].Count.ToString(), GUILayout.Width(70));
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
            }

            if (Application.isEditor && Application.isPlaying)
            {
                Repaint();
            }
        }

        private int GetCount(EventManager manager, bool withData)
        {
            var field = manager.GetType().GetField(
                withData ? "eventsWithData" : "eventsWithoutData",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
            );
            var dict = field.GetValue(manager) as System.Collections.IDictionary;
            return dict?.Count ?? 0;
        }

        private System.Collections.Generic.List<EventInfo> GetEventInfo(EventManager manager, bool withData)
        {
            var field = manager.GetType().GetField(
                withData ? "eventsWithData" : "eventsWithoutData",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
            );
            var dict = field.GetValue(manager) as System.Collections.IDictionary;
            var list = new System.Collections.Generic.List<EventInfo>();
            if (dict != null)
            {
                foreach (System.Collections.DictionaryEntry kvp in dict)
                {
                    var handlers = kvp.Value as System.Collections.ICollection;
                    list.Add(new EventInfo
                    {
                        Key = kvp.Key.ToString(),
                        Count = handlers?.Count ?? 0
                    });
                }
            }
            return list;
        }

        [System.Serializable]
        public class EventInfo
        {
            public string Key;
            public int Count;
        }
    }
}
#endif