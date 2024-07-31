
num = 0

function increase()
    num = num + 1
    print('>>>> lua file: num = '..num)
    return num
end

function increase_num(tmp_num)
    num = num + tmp_num
    print('>>>> lua file: num = '..num)
    return num
end

nums = {1, 2, 3, 4}
nums.test_func = function ( ... )
    print('>>>> lua file: test_func')
end