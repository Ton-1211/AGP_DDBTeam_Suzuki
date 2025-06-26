using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static MapData.Preset;

public class MapDataEditer : EditorWindow
{
    [MenuItem("Window/OriginalPanels/EditorWindow", priority = 0)]
    static public void CreateWindow()
    {
        EditorWindow.GetWindow<MapDataEditer>();
    }

    [SerializeField]
    private MapData baseData;
    [SerializeField]
    private string baseDataPath;

    const int BOARD_MAX = 80;
    private Map_State setState;
    private Vector2Int posLD, posRU;
    private Vector2 mapScroll;

    private int sliderPreset = 0;
    private Vector2 areaScroll;
    private void OnEnable()
    {
        var defaultData = AssetDatabase.LoadAssetAtPath<MapData>("Assets/Saeki/ScriptableObjects/MapData.asset");
        //var defaultData = Resources.Load<>("");
        this.baseData = defaultData.Clone();
        this.baseDataPath = AssetDatabase.GetAssetPath(defaultData);
    }


    void ColorGUI(Map_State map_Object)
    {
        switch (map_Object)
        {
            case Map_State.Destructible:
                GUI.backgroundColor = Color.white;
                break;

            case Map_State.Indestructible:
                GUI.backgroundColor = Color.black;
                break;

            default:
                GUI.backgroundColor = Color.gray;
                break;
        }
    }
    private void OnGUI()
    {
        using (new EditorGUILayout.VerticalScope())
        {
            //Debug.Log(BaseData);
            if (baseData == null) this.baseData = AssetDatabase.LoadAssetAtPath<MapData>(this.baseDataPath).Clone();

            Undo.RecordObject(baseData, "Modify MapDataBase");
            
            using (new EditorGUILayout.HorizontalScope())
            {
                sliderPreset = (int)EditorGUILayout.Slider("preset_ID", sliderPreset, 0, this.baseData.preset.Length - 1, GUILayout.MaxWidth(300f));
                EditorGUILayout.LabelField("||Map:", GUILayout.MaxWidth(40f));

                EditorGUILayout.LabelField("壁の数",baseData.preset[sliderPreset].destValue.ToString());
                

                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField("|:", GUILayout.MaxWidth(40f));
                if (GUILayout.Button("プリセットを追加", GUILayout.MaxWidth(120f), GUILayout.MaxHeight(20f)))
                {
                    Undo.RecordObject(baseData, "Add preset");
                    int Map_Length = this.baseData.preset.Length;
                    System.Array.Resize(ref this.baseData.preset, Map_Length + 1);
                    this.baseData.preset[Map_Length] = new MapData.Preset()
                    {
                        area = new MapData.Preset.Area[1],
                        height = new Height[BOARD_MAX],
                    };
                    for (int _Y = 0; BOARD_MAX > _Y; _Y++)
                    {
                        this.baseData.preset[Map_Length].height[_Y] = new MapData.Preset.Height()
                        {
                            width = new Map_State[BOARD_MAX],
                        };
                        for (int _X = 0; BOARD_MAX > _X; _X++)
                            this.baseData.preset[Map_Length].height[_Y].width[_X] = Map_State.Destructible;
                    }                   
                }

                if (GUILayout.Button("エリアを追加", GUILayout.MaxWidth(120f), GUILayout.MaxHeight(20f)))
                {
                    Undo.RecordObject(baseData, "Add Area");
                    int Area_Length = this.baseData.preset[sliderPreset].area.Length;
                    System.Array.Resize(ref this.baseData.preset[sliderPreset].area, Area_Length + 1);
                    this.baseData.preset[sliderPreset].area[Area_Length] = new MapData.Preset.Area()
                    {
                        posLeftDown = new(0, 0),
                        posRightUp = new(0, 0),              
                    };
                }
                if (GUILayout.Button("元に戻す", GUILayout.MaxWidth(60f), GUILayout.MaxHeight(20f)))
                {
                    this.baseData = AssetDatabase.LoadAssetAtPath<MapData>(this.baseDataPath).Clone();
                    EditorGUIUtility.editingTextField = false;
                }

                if (GUILayout.Button("保存", GUILayout.MaxWidth(60f), GUILayout.MaxHeight(20f)))
                {
                    var data = AssetDatabase.LoadAssetAtPath<MapData>(this.baseDataPath);
                    EditorUtility.CopySerialized(this.baseData, data);
                    EditorUtility.SetDirty(data);
                    AssetDatabase.SaveAssets();
                }
            }
        }

        using (new EditorGUILayout.HorizontalScope(GUILayout.MaxWidth(20f)))
        {
                
            if (0 < this.baseData.preset.Length)
            {

                using (var scrollMap = new EditorGUILayout.ScrollViewScope(mapScroll, GUILayout.MinWidth(1000f),GUILayout.MaxHeight(1000f)))
                {
                    int destvalue = 0;
                    mapScroll = scrollMap.scrollPosition;
                    for (int Y = BOARD_MAX - 1; Y >= 0; Y--)                       
                    {
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            using (new EditorGUILayout.VerticalScope())
                            {
                                if (Y == BOARD_MAX - 1)
                                {
                                    EditorGUILayout.LabelField("Y/X", GUILayout.MaxWidth(22f));
                                }
                                using (new EditorGUILayout.VerticalScope())
                                {
                                    EditorGUILayout.LabelField(Y.ToString(), GUILayout.MaxWidth(22f));
                                }
                                if(Y == 0)
                                {                                    
                                    EditorGUILayout.LabelField("Y/X", GUILayout.MaxWidth(22f));                                                                  
                                }                                    
                            }

                            for (int X = 0; X < BOARD_MAX; X++)
                            {
                                using (new EditorGUILayout.VerticalScope())                      
                                {
                                    if (Y == BOARD_MAX - 1)
                                    {
                                        EditorGUILayout.LabelField(X.ToString(), GUILayout.MaxWidth(20f));
                                    }

                                    Map_State map_State = baseData.preset[sliderPreset].height[Y].width[X];
                                    if(map_State != Map_State.None)
                                        destvalue++;

                                    ColorGUI(map_State);

                                    baseData.preset[sliderPreset].height[Y].width[X] =
                                    (Map_State)EditorGUILayout.EnumPopup(baseData.preset[sliderPreset].height[Y].width[X],
                                    GUILayout.MaxWidth(20f), GUILayout.MaxHeight(20f));
                                    
                                    GUI.backgroundColor = Color.white;

                                    if (Y == 0)
                                    {
                                        EditorGUILayout.LabelField(X.ToString(), GUILayout.MaxWidth(20f));
                                    }
                                }
                            }

                            using (new EditorGUILayout.VerticalScope())
                            {
                                if (Y == BOARD_MAX - 1)
                                {
                                    EditorGUILayout.LabelField("X/Y", GUILayout.MaxWidth(22f));
                                }
                                using (new EditorGUILayout.VerticalScope())
                                {
                                    EditorGUILayout.LabelField(Y.ToString(), GUILayout.MaxWidth(22f));
                                }
                                if (Y == 0)
                                {
                                    EditorGUILayout.LabelField("X/Y", GUILayout.MaxWidth(22f));
                                }
                            }
                        }
                    }

                    baseData.preset[sliderPreset].destValue = destvalue;
                }
            }




            using (new EditorGUILayout.HorizontalScope(GUILayout.MaxWidth(300f)))
            {
                using (new EditorGUILayout.VerticalScope(GUILayout.MaxWidth(300f)))
                {
                    EditorGUILayout.LabelField("    範囲設置", GUILayout.MinWidth(180f));
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        using (new EditorGUILayout.VerticalScope())
                        {
                            using (new EditorGUILayout.HorizontalScope())
                            {
                                Map_State Set = setState;
                                ColorGUI(Set);
                                EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                                setState = (Map_State)EditorGUILayout.EnumPopup(setState, GUILayout.MaxWidth(20f), GUILayout.MaxHeight(20f));
                                EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                                GUI.backgroundColor = Color.white;
                            }

                            if (GUILayout.Button("設置", GUILayout.MaxWidth(40f)))
                            {
                                Undo.RecordObject(baseData, "SET");
                                //int FloorPoint = i;
                                for (int _X = posLD.x; _X < BOARD_MAX && _X <= posRU.x; _X++)
                                    for (int _Y = posLD.y; _Y < BOARD_MAX && _Y <= posRU.y; ++_Y)
                                        baseData.preset[sliderPreset].height[_Y].width[_X] = setState;
                            }
                        }

                        using (new EditorGUILayout.VerticalScope())
                        {
                            EditorGUILayout.LabelField("左下", GUILayout.MaxWidth(30f));
                            EditorGUILayout.LabelField("右上", GUILayout.MaxWidth(30f));
                        }

                        using (new EditorGUILayout.VerticalScope())
                        {
                            posLD = EditorGUILayout.Vector2IntField("", posLD, GUILayout.MaxWidth(80f));
                            posRU = EditorGUILayout.Vector2IntField("", posRU, GUILayout.MaxWidth(80f));
                        }
                    }

                    if (0 < this.baseData.preset.Length && 0 < this.baseData.preset[sliderPreset].area.Length)
                    {
                        EditorGUILayout.LabelField("エリアの設定", GUILayout.MinWidth(180f));
                        using (var scrollArea = new EditorGUILayout.ScrollViewScope(areaScroll, GUILayout.MinWidth(180f)))
                        {
                            areaScroll = scrollArea.scrollPosition;
                            for (int i = 0; i < this.baseData.preset[sliderPreset].area.Length; i++)
                            {
                                GUILayout.Box("", GUILayout.Height(10), GUILayout.ExpandWidth(true));

                                using (new EditorGUILayout.HorizontalScope())
                                {
                                    using (new EditorGUILayout.VerticalScope())
                                    {
                                        EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(20f));
                                    }

                                    using (new EditorGUILayout.VerticalScope())
                                    {
                                        EditorGUILayout.LabelField("左下", GUILayout.MaxWidth(40f));
                                        EditorGUILayout.LabelField("右上", GUILayout.MaxWidth(40f));
                                    }

                                    using (new EditorGUILayout.VerticalScope())
                                    {
                                        baseData.preset[sliderPreset].area[i].posLeftDown =
                                                EditorGUILayout.Vector2IntField("", baseData.preset[sliderPreset].area[i].posLeftDown,
                                                GUILayout.MaxWidth(80f));

                                        baseData.preset[sliderPreset].area[i].posRightUp =
                                                EditorGUILayout.Vector2IntField("", baseData.preset[sliderPreset].area[i].posRightUp,
                                                GUILayout.MaxWidth(80f));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

























        if (Event.current.type == EventType.DragUpdated)
        {
            if (DragAndDrop.objectReferences != null &&
                DragAndDrop.objectReferences.Length > 0 &&
                DragAndDrop.objectReferences[0] is MapData)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                Event.current.Use();
            }
        }
        else if (Event.current.type == EventType.DragPerform)
        {
            Undo.RecordObject(this, "Change MapData");
            this.baseData = ((MapData)DragAndDrop.objectReferences[0]).Clone();
            this.baseDataPath = DragAndDrop.paths[0];
            DragAndDrop.AcceptDrag();
            Event.current.Use();
        }
        if (DragAndDrop.visualMode == DragAndDropVisualMode.Copy)
        {
            var rect = new Rect(Vector2.zero, this.position.size);
            var bgColor = Color.white * new Color(1f, 1f, 1f, 0.2f);
            EditorGUI.DrawRect(rect, bgColor);
            EditorGUI.LabelField(rect, "ここにアイテムデータをドラッグ＆ドロップしてください");
        }
    }


        public void AddItemsToMenu(GenericMenu menu)
    {
        menu.AddItem(new GUIContent("Original Menu"), false, () => Debug.Log("Press Menu!"));
    }
}
