using System;
using UnityEngine;
using LuaInterface;

public class LuaCallCharp : MonoBehaviour
{
    private string lua_file = "lua_call_csharp";
    void Start()
    {
        LuaState lua_state = new LuaState();
        lua_state.Start();
        string sceneFile = Application.dataPath + "/Demo/Lua";
        lua_state.AddSearchPath(sceneFile);
        
        // 注册方法调用
        LuaBinder.Bind(lua_state);
        Bind(lua_state);
        
        Debug.LogWarning(">>>> c# file: AccessVar初始值：" + AccessVar);
        
        lua_state.Require(lua_file);
        
        Debug.LogWarning(">>>> c# file: AccessVar 执行lua后：" + AccessVar);

        lua_state.Dispose();
        lua_state = null;
    }

    private void Bind(LuaState L)
    {
        L.BeginModule(null);
        L.BeginClass(typeof(LuaCallCharp), typeof(UnityEngine.MonoBehaviour));
        L.RegFunction("Debug", PrintCall);
        L.RegVar("AccessVar", GetAccessVar, SetAccessVar);
        L.EndClass();
        L.EndModule();
    }

    private int PrintCall(System.IntPtr L)
    {
        try
        {
            ToLua.CheckArgsCount(L, 2);//对参数进行校验
            LuaCallCharp obj = (LuaCallCharp)ToLua.CheckObject(L, 1, typeof(LuaCallCharp));//获取目标对象并转换格式
            string arg0 = ToLua.CheckString(L, 2);//获取特定值
            Debug.LogWarning(">>>> c# file: 输出变量值 : " + arg0);
            return 1;
        }
        catch (System.Exception e)
        {
            return LuaDLL.toluaL_exception(L, e);
        }
    }
    
    
    [System.NonSerialized]
    public string AccessVar = "++这是初始值++";
    private int GetAccessVar(System.IntPtr L)
    {
        object o = null;

        try
        {
            o = ToLua.ToObject(L, 1); //获得变量实例
            LuaCallCharp obj = (LuaCallCharp)o; //转换目标格式
            string ret = obj.AccessVar; //获取目标值
            ToLua.Push(L, ret);//将目标对象传入虚拟机
            return 1;
        }
        catch (System.Exception e)
        {
            return LuaDLL.toluaL_exception(L, e, o, "attempt to index AccessVar on a nil value");
        }
    }
    private int SetAccessVar(System.IntPtr L)
    {
        object o = null;

        try
        {
            o = ToLua.ToObject(L, 1);//获得变量实例
            LuaCallCharp obj = (LuaCallCharp)o;//转换目标格式
            obj.AccessVar = ToLua.ToString(L, 2);//将要修改的值进行设定
            return 1;
        }
        catch (System.Exception e)
        {
            return LuaDLL.toluaL_exception(L, e, o, "attempt to index AccessVar on a nil value");
        }
    }
    
    






}
