using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader _instance; // 实例
    public Transition currentTransition; // 当前的过场预设
    public bool LoadAble{ get; private set;} // 现在是否可以转场

    private void Awake() {
        if(_instance)
            Destroy(gameObject);
        else{
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        LoadAble = true;
    }

    /// <summary>
    /// 使用转场加载场景
    /// </summary>
    /// <param name="SceneName">场景名称</param>
    /// <param name="transition">转场方式</param>
    public void LoadScene(string SceneName, Transition transition = null){
        // 如果现在不能转换场景直接返回
        if(!LoadAble) return;

        // 如果有设置过场就设置过场
        if (transition != null)
            currentTransition = transition;

        // 开始转换场景
        StartCoroutine(LoadLevel(SceneName));
    }

    /// <summary>
    /// 转换场景并且使用过场动画
    /// </summary>
    /// <param name="levelName">场景名称</param>
    /// <returns></returns>
    private IEnumerator LoadLevel(string levelName){
        // 异步加载场景
        AsyncOperation loading = SceneManager.LoadSceneAsync(levelName);

        // 不允许场景加载完后直接转换
        loading.allowSceneActivation = false;

        // 现在不再能转换场景
        LoadAble = false;
        
        // 开始过场
        currentTransition.StartTrans();

        // 等待一帧
        // 理由再下面有解释，但其实这里本来不需要，因为检查动画前还夹着一个检查加载的过程。基本不会在一帧内就加载完
        // 但是保险起见还是在播放动画后延迟一帧
        yield return null;
        
        // 等待场景加载几乎完成
        while(loading.progress < 0.899)
            yield return null;

        // 等待动画播放完成
        while(!currentTransition.IsAnimationDone())
            yield return null;

        // 允许场景加载完成
        loading.allowSceneActivation = true;

        // 等待场景加载彻底完成
        while(loading.progress != 1)
            yield return null;
        
        // 结束过场
        currentTransition.EndTrans();

        // 等待一帧
        // 因为我发现如果在开始动画后不等待一帧的话，第二个动画其实还没开始播放，
        // 后面检测动画完成检测的就是第一个动画，就起不到检测第二个动画的作用。
        yield return null;

        // 等待动画播放完成
        while(!currentTransition.IsAnimationDone())
            yield return null;

        print("Done");

        // 可以继续转换场景
        LoadAble = true;
    }
}