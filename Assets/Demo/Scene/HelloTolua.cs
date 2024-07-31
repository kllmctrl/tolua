using UnityEngine;
using LuaInterface;
public class HelloTolua : MonoBehaviour
{
    private string lua_file = "hello_tolua";
    private string lua_string = "print('hello tolua')";
    void Start()
    {
        LuaState lua_state = new LuaState();// 1. 建立Lua虚拟机
        lua_state.Start(); // 2. 启动虚拟机
        // lua.DoString(lua_string);// 3. 使用string调用Lua
        //3. 使用文件调用Lua
        // print(Application.dataPath);
        string sceneFile = Application.dataPath + "/Demo/Lua";//手动添加一个lua文件搜索地址
        lua_state.AddSearchPath(sceneFile);
        lua_state.DoFile(lua_file);
        // lua.Require(lua_file);
        
        lua_state.Dispose();//4. 使用完毕回收虚拟机
        lua_state = null;
    }
}
