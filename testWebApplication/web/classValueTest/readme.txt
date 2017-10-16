ObjectCache["name1"]中赋值，在将原值赋值为NULL，ObjectCache["name1"]的值不会是NULL

var newObject = ObjectCache["name1"]; 修改newObject中属性的值，ObjectCache["name1"]会跟着变化，但修改newObject本身，ObjectCache["name1"]不变