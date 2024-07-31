using UnityEngine;
using LuaInterface;

public class CSharpCallLua : MonoBehaviour
{
    private string lua_file = "csharp_call_lua";
    void Start()
    {
        LuaState lua_state = new LuaState();
        lua_state.Start();
        string sceneFile = Application.dataPath + "/Demo/Lua";
        lua_state.AddSearchPath(sceneFile);
        lua_state.Require(lua_file);
        
        // 1. lua变量
        Debug.LogWarning(">>>> c# file: num = " + lua_state["num"]);
        lua_state["num"] = 10;
        Debug.LogWarning(">>>> c# file: num = " + lua_state["num"]);
        
        // 2. 无参函数
        // 方法1：转换为 LuaFunction 类型后调用
        LuaFunction luaFunc = lua_state.GetFunction("increase");
        luaFunc.Call();
        Debug.LogWarning(">>>> c# file: C#调用LuaFunction, 函数返回值：" + lua_state["num"]);
        // 方法2：直接调用Call
        lua_state.Call("increase", false);
        Debug.LogWarning(">>>> c# file: C#直接调用Call, 函数返回值：" + lua_state["num"]);

        //3. 有参函数
        // 方法1：转换为 LuaFunction 类型后调用
        LuaFunction luaParamFunc = lua_state.GetFunction("increase_num");
        luaParamFunc.BeginPCall();
        luaParamFunc.Push(10);
        luaParamFunc.PCall();
        luaParamFunc.EndPCall();
        Debug.LogWarning(">>>> c# file: C#调用LuaFunction有参函数, 函数返回值：" + lua_state["num"]);
        // 方法2：直接调用Call
        luaParamFunc.Call(10);
        Debug.LogWarning(">>>> c# file: C#直接调用Call有参函数, 函数返回值：" + lua_state["num"]);

        //4. Table
        LuaTable table = lua_state.GetTable("nums");
        Debug.LogWarning(">>>> c# file: 获取table中的num值：" + table[1]);
        //通过下标直接获取
        for (int i = 0; i < table.Length; i++)
        {
            Debug.LogWarning(">>>> c# file: 获取table中的值：" + table[i+1]);
        }
        table.Call("test_func");
        LuaFunction tableFunc = table.GetLuaFunction("test_func");
        tableFunc.Call();
        
        lua_state.Dispose();
        lua_state = null;
    }
}
