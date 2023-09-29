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

## 🎉 功能介绍
### 1. **原生SQL**
#### **同步**
| 方法名             | 描述                                   | 返回值     |
| ------------------ | -------------------------------------- | ---------- |
| SqlQuery\<T>       | 查询所有返回实体集合                   | List       |
| SqlQuerySingle\<T> | 查询第一条记录                         | T          |
| GetDataTable       | 查询数据表格DataTable                  | DataTable  |
| GetDataReader      | 查询数据读取器DataReader，需要手动释放 | DataReader |
| GetDataSet         | 查询数据结果集DataSet                  | DataSet    |
| GetScalar          | 获取首行首列                           | object     |
| ExecuteCommand     | 执行SQL返回受影响行数，一般用于增删改  | int        |
#### **异步**
| 方法名                  | 描述                                   | 返回值            |
| ----------------------- | -------------------------------------- | ----------------- |
| SqlQueryAsync\<T>       | 查询所有返回实体集合                   | Task\<List>       |
| SqlQuerySingleAsync\<T> | 查询第一条记录                         | Task\<T>          |
| GetDataTableAsync       | 查询数据表格DataTable                  | Task\<DataTable>  |
| GetDataReaderAsync      | 查询数据读取器DataReader，需要手动释放 | Task\<DataReader> |
| GetDataSetAsync         | 查询数据结果集DataSet                  | Task\<DataSet>    |
| GetScalarAsync          | 获取首行首列                           | Task\<object>     |
| ExecuteCommandAsync     | 执行SQL返回受影响行数，一般用于增删改  | Task\<int>        |