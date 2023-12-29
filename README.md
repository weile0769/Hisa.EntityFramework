<div align="center">
	<h1 align="center" style="color:#4da7fd"><b>Snail.EntityFramework</b></h1>
</div>
<div align="center">
<span align="center" style="font-weight:bold" >⚡一款NET开源多库的、开箱即用的、功能全面的ORM框架</span>
</div>
<br>
<p align="center">
<img alt="csharp" src="https://img.shields.io/badge/language-csharp-brightgreen.svg">
<img alt="license" src="https://img.shields.io/badge/license-MIT-blue.svg">
</p>

## 🚩 项目介绍
Snail.EntityFramework 一款NET开源多库的、开箱即用的、功能全面的ORM框架。目前数据库仅支持MYSQL。未来一直会致力追求卓越的性能、支持更多的数据库，为开源中国添砖加瓦。

## 🏅 开源地址
[![Gitee](https://shields.io/badge/Gitee-https://gitee.com/weile0796/Snail.EntityFramework-green?logo=gitee&style=flat&logoColor=red)](https://gitee.com/weile0796/Snail.EntityFramework.git)
<br>
[![Github](https://shields.io/badge/Github-https://github.com/weile0769/Snail.EntityFramework-green?logo=github&style=flat&logoColor=white)](https://github.com/weile0769/Snail.EntityFramework)

## ✨ 近期目标
**原生SQL**
- [x] 无实体原生SQL数据库访问操作

**条件查询**
- [x] SQL语法查询
- [ ] Lambda表达式查询
- [x] SQL语法条件查询（还不太完善）
- [ ] Lambda表达式条件查询

</dl>

## 🎯 安装
```csharp
var builder = WebApplication.CreateBuilder(args);
//注册Snail数据库实体框架
builder.Services.AddSnailEntityFramework(options =>
{
	options.ConfigureOptions = new List<DatabaseConfigureOptions>
	{
		new()
		{
			Enabled = true,
			Default = true,
			ConnectionName = Configure.ConnectionName,
			ConnectionString = Configure.ConnectionString
		}
	};

	options.UseMySqlConnector();
});
```
**说明：** <br>
DatabaseConfigureOptions配置项：
- Enabled：是否开启,默认true
- Default：是否默认数据库,默认false
- ConnectionName：数据库连接对象标识
- ConnectionString：数据库连接字符串
- CommandTimeOut：数据库命令执行等待时间,默认值：300秒

## 🎉 功能介绍
### **原生SQL**
| 方法名             | 描述                                   | 返回值     |
| ------------------ | -------------------------------------- | ---------- |
| SqlQuery\<T>       | 查询所有返回实体集合                   | List\<T>   |
| SqlQuerySingle\<T> | 查询第一条记录                         | T          |
| GetDataTable       | 查询数据表格DataTable                  | DataTable  |
| GetDataReader      | 查询数据读取器DataReader，需要手动释放 | DataReader |
| GetDataSet         | 查询数据结果集DataSet                  | DataSet    |
| GetScalar          | 获取首行首列                           | object     |
| ExecuteCommand     | 执行SQL返回受影响行数，一般用于增删改  | int        |