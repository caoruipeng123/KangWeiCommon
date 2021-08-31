using KangWeiCommon.Entity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace KangWeiCommon
{
    /// <summary>
    /// SQL Server数据库访问操作类
    /// </summary>
    public partial class DbUtil
    {
        static ConcurrentDictionary<Type, PropertyInfo[]> propertyCache = new ConcurrentDictionary<Type, PropertyInfo[]>();

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="SQLString">查询sql</param>
        /// <param name="cmdParms">查询参数</param>
        /// <returns></returns>
        public static DataSet Query(string connStr, string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ds");
                    cmd.Parameters.Clear();
                    return ds;
                }
            }
        }

        /// <summary>
        /// 根据查询语句获取单个实体
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">查询参数（没有参数传null）</param>
        /// <returns></returns>
        public static T Get<T>(string connStr, string sql, params SqlParameter[] parameters) where T : new()
        {
            DataTable dt = Query(connStr, sql, parameters).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return ConvertDataRowToEntity<T>(dt.Rows[0]);
            }
            return default(T);
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回第一行第一列查询结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr">链接字符串</param>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public static T GetSingle<T>(string connStr, string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(cmd, connection, null, sql, parameters);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, DBNull.Value)))
                    {
                        return default(T);
                    }
                    else
                    {
                        return obj.To<T>();
                    }
                }
            }
        }

        /// <summary>
        /// 根据查询语句获取实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">查询参数（没有参数传null）</param>
        /// <returns></returns>
        public static List<T> Gets<T>(string connStr, string sql, params SqlParameter[] parameters) where T : new()
        {
            DataTable dt = Query(connStr, sql, parameters).Tables[0];
            return ConvertDataTableToEntityList<T>(dt);
        }

        /// <summary>
        /// 根据查询语句获取实体集合[当泛型T为int、double、decimal、string等基础类型时，使用该方法]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="sql">查询语句[sql查询语句应该只有一列]</param>
        /// <param name="parameters">查询参数（没有参数传null）</param>
        /// <returns></returns>
        public static List<T> GetBases<T>(string connStr, string sql, params SqlParameter[] parameters)
        {
            DataTable dt = Query(connStr, sql, parameters).Tables[0];
            Type type = typeof(T);
            List<T> list = new List<T>(dt.Rows.Count);
            if (dt == null || dt.Rows.Count == 0)
            {
                return list;
            }
            foreach (DataRow dr in dt.Rows)
            {
                var t = (T)Convert.ChangeType(dr[0], typeof(T));
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="sql">查询sql</param>
        /// <param name="pageIndex">页(从1开始)</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public static PageModel<T> GetPage<T>(string connStr, string sql, int pageIndex, int pageSize, params SqlParameter[] parameters) where T : new()
        {
            DataSet ds = Query(connStr, sql, parameters);
            if (ds != null && ds.Tables.Count == 2)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    PageModel<T> pageData = new PageModel<T>()
                    {
                        PageIndex = pageIndex,
                        PageSize = pageSize,
                        RowCount = ds.Tables[1].Rows[0][0].ToInt(),
                        PageCount = (int)Math.Ceiling(ds.Tables[1].Rows[0][0].ToInt() * 1.0 / pageSize),
                        PageData = ConvertDataTableToEntityList<T>(ds.Tables[0])
                    };
                    return pageData;
                }
            }
            return new PageModel<T>() { PageIndex = pageIndex, PageSize = pageSize, PageData = new List<T>(), RowCount = ds.Tables[1].Rows[0][0].ToInt(), PageCount = (int)Math.Ceiling(ds.Tables[1].Rows[0][0].ToInt() * 1.0 / pageSize) };

        }

        /// <summary>
        /// 执行sql语句，返回受影响的行数
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数（没有参数传null）</param>
        /// <returns></returns>
        public static int Execute(string connStr, string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(cmd, connection, null, sql, parameters);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
            }
        }

        /// <summary>
        /// 装配SqlCommand对象
        /// </summary>
        /// <param name="cmd">被装配的SqlCommand对象</param>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="trans">事务</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">sql参数</param>
        static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            // 如果存在参数，则表示用户是用参数形式的SQL语句，可以替换
            if (cmdParms != null && cmdParms.Length > 0)
                cmdText = cmdText.Replace("?", "@").Replace(":", "@");
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    // 如果存在参数，则表示用户是用参数形式的SQL语句，可以替换
                    parameter.ParameterName = parameter.ParameterName.Replace("?", "@").Replace(":", "@");
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// 通过反射将DataRow中的一行数据转换为Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row">数据行</param>
        /// <returns></returns>
        public static T ConvertDataRowToEntity<T>(DataRow row) where T : new()
        {
            Type type = typeof(T);
            PropertyInfo[] pros = null;
            if (propertyCache.ContainsKey(type))
            {
                pros = propertyCache[type];
            }
            else
            {
                pros = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propertyCache.TryAdd(type, pros);
            }
            if (pros != null && pros.Length > 0)
            {
                T t = new T();
                foreach (PropertyInfo p in pros)
                {
                    if (p.CanWrite)
                    {
                        if (row.Table.Columns.Contains(p.Name) && !Convert.IsDBNull(row[p.Name]))
                        {
                            p.SetValue(t, row[p.Name]);
                        }
                    }
                }
                return t;
            }
            return default(T);
        }

        /// <summary>
        /// 通过反射将DataTable中的数据转换为实体类集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dt">数据表</param>
        /// <returns></returns>
        public static List<T> ConvertDataTableToEntityList<T>(DataTable dt) where T : new()
        {
            Type type = typeof(T);
            List<T> list = new List<T>(dt.Rows.Count);
            if (dt == null || dt.Rows.Count == 0)
            {
                return list;
            }
            PropertyInfo[] pros = null;
            if (propertyCache.ContainsKey(type))
            {
                pros = propertyCache[type];
            }
            else
            {
                pros = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propertyCache.TryAdd(type, pros);
            }
            foreach (DataRow dr in dt.Rows)
            {
                var t = new T();
                foreach (var p in pros)
                {
                    if (p.CanWrite && dt.Columns.Contains(p.Name) && !Convert.IsDBNull(dr[p.Name]))
                    {
                        p.SetValue(t, dr[p.Name]);
                    }
                }
                list.Add(t);
            }
            return list;
        }
    }
}
