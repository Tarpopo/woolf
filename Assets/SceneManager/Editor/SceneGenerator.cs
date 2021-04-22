
using DefaultNamespace.Starter;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class SceneGenerator:MonoBehaviour
{
    static SceneGenerator()
    {
        EditorSceneManager.newSceneCreated += SceneCreating;
    }

    public static void SceneCreating(Scene scene, NewSceneSetup setup,NewSceneMode mode)
    {
        var _cameras=new GameObject("Cameras").transform;
        
        var _setup=new GameObject("[Setup]").transform;
        _cameras.parent = _setup;
        
        var _world=new GameObject("[World]").transform;
        
        var _static = new GameObject("Static").transform;
            _static.parent = _world;
            
        new GameObject("Dynamic").transform.parent = _world;

        new GameObject("[UI]");
        Debug.Log("New Scene Created");

        Instantiate(Resources.Load<GameObject>("Main Camera"),_cameras);
        Starter start=GameObject.Find("[Setup]").AddComponent<Starter>();
        start.managers.Add(ScriptableObject.CreateInstance<ManagerUpdate>());
        //start.managers.Add(ScriptableObject.CreateInstance<GameManager>());
        
        Instantiate(Resources.Load<GameObject>("auf (1)"),_static);
    }

}
