-- lua_call_csharp.lua
print('--进入Lua调用--')
local go = UnityEngine.GameObject.Find("Main Camera")
local access=go:GetComponent("LuaCallCharp")
access:Debug("Lua调用C#方法")
access.AccessVar="--这是修改值--"
print('--Lua调用结束--')
