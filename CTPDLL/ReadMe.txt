C#与DLL之间的接口描述

C#发给DLL和DLL发给C#的数据，均以字符串形式实现，不同的字段之间以 >< 进行分割
具体各个字段的含义，根据不同接口而不同，但前3个字段为：交易号、功能分类、具体功能名称

1 登录请求
[nRequestID]><sys><login><[BrokerID]><[UserName]><[PassWord]
