using BE.Services.SqlDbConnections;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;

namespace BE.Services.Tool
{
    public class GenerateUnitOfWork
    {
        #region Common Property

        SqlCommand _sqlCommand;
        SqlDataAdapter _sqlDataAdapter;
        DataSet _dataSet;
        DataTable dtAllTables = new DataTable();
        DataTable dtAllColumns = new DataTable();

        #endregion

        #region Replace Sql To C# Data Type

        private static readonly string[] SqlServerTypes = { "bigint", "binary", "bit", "char", "date", "datetime", "datetime2", "datetimeoffset", "decimal", "filestream", "float", "geography", "geometry", "hierarchyid", "image", "int", "money", "nchar", "ntext", "numeric", "nvarchar", "real", "rowversion", "smalldatetime", "smallint", "smallmoney", "sql_variant", "text", "time", "timestamp", "tinyint", "uniqueidentifier", "varbinary", "varchar", "xml" };
        private static readonly string[] CSharpTypes = { "long", "byte[]", "bool", "char", "DateTime", "DateTime", "DateTime", "DateTimeOffset", "decimal", "byte[]", "double", "MicrBEnterprisesoft.SqlServer.Types.SqlGeography", "MicrBEnterprisesoft.SqlServer.Types.SqlGeometry", "MicrBEnterprisesoft.SqlServer.Types.SqlHierarchyId", "byte[]", "int", "decimal", "string", "string", "decimal", "string", "Single", "byte[]", "DateTime", "short", "decimal", "object", "string", "TimeSpan", "byte[]", "byte", "Guid", "bite[]", "string", "string" };
        public static string ConvertSqlServerFormatToCSharp(string typeName)
        {
            var index = Array.IndexOf(SqlServerTypes, typeName);

            return index > -1
                ? CSharpTypes[index]
                : "object";
        }
        public static string ConvertCSharpFormatTBEnterprisesqlServer(string typeName)
        {
            var index = Array.IndexOf(CSharpTypes, typeName);

            return index > -1
                ? SqlServerTypes[index]
                : null;
        }

        #endregion

        #region Constructure

        public GenerateUnitOfWork()
        {
            GenerateModelFiles();
        }

        #endregion

        #region Block For Get All Tables and Column Respective Particullar Table

        public DataTable GetAllTables()
        {
            string sqlQuery = @"SELECT TABLE_NAME
                                FROM INFORMATION_SCHEMA.TABLES
                                WHERE (TABLE_SCHEMA = 'Paylite4.5Standard_Dev' OR TABLE_SCHEMA = 'dbo')
                                --AND TABLE_NAME='CTGEMP0'
                                ORDER BY TABLE_NAME";

            dtAllTables = new DataTable();

            using (SqlConnection connection = new SqlSqlDbConnection()._sqlConn)
            {
                connection.Open();
                _dataSet = new DataSet();
                try
                {
                    _sqlCommand = new SqlCommand { CommandText = sqlQuery };
                    _sqlDataAdapter = new SqlDataAdapter(Convert.TBEnterprisestring(sqlQuery), connection);
                    _sqlDataAdapter.Fill(dtAllTables);

                }
                catch (Exception)
                {
                    _sqlCommand.DispBEnterprisese();
                }
                finally
                {
                    connection.ClBEnterprisese();
                    _sqlCommand.DispBEnterprisese();
                    if (_sqlDataAdapter != null)
                        _sqlDataAdapter.DispBEnterprisese();
                }
                return dtAllTables;
            }
        }

        public DataTable GetAllColumns(string TableName)
        {
            string sqlQuery = @"SELECT COLUMN_NAME,DATA_TYPE,CASE WHEN IS_NULLABLE = 'YES' THEN 1 ELSE 0 END AS IS_NULLABLE
                                FROM INFORMATION_SCHEMA.COLUMNS
                                WHERE TABLE_NAME = '" + TableName + "' ORDER BY ORDINAL_PBEnterprisesITION";

            dtAllColumns = new DataTable();
            using (SqlConnection connection = new SqlSqlDbConnection()._sqlConn)
            {
                connection.Open();
                _dataSet = new DataSet();
                try
                {
                    _sqlCommand = new SqlCommand { CommandText = sqlQuery };
                    _sqlDataAdapter = new SqlDataAdapter(Convert.TBEnterprisestring(sqlQuery), connection);
                    DataSet ds = new DataSet();
                    _sqlDataAdapter.Fill(ds, TableName);
                    dtAllColumns = ds.Tables[0];
                }
                catch (Exception)
                {
                    _sqlCommand.DispBEnterprisese();
                }
                finally
                {
                    connection.ClBEnterprisese();
                    _sqlCommand.DispBEnterprisese();
                    if (_sqlDataAdapter != null)
                        _sqlDataAdapter.DispBEnterprisese();
                }
                return dtAllColumns;
            }
        }

        #endregion

        #region Block For Generate Model Class Files

        public void GenerateModelFiles()
        {
            try
            {
                //StackFrame sf = Ex.JndGetStackFram
                //"Project Name:   " + Assembly.GetCallingAssembly().GetName().Name + "<br>" +
                //"File Name:      " + sf.GetFileName() + "<br>" +
                //"Class Name:     " + sf.GetMethod().DeclaringType + "<br>" +
                //"Method Name:    " + sf.GetMethod() + "<br>" +
                //"Line Number:    " + sf.GetFileLineNumber() + "<br>" +
                //"Line Column:    " + sf.GetFileColumnNumber() + "<br>" +
                //"Error Message:  " + Ex.Message + "<br>" +
                //"Inner Message : " + Ex.InnerException.Message

                //string[] solutionName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName).Split('.');
                StringBuilder stringBuilder = new StringBuilder();

                GetAllTables();

                #region Block For Files Root Path

                string sFolderNameModel = ".UnitOfWork";
                string sProjectName = Assembly.GetCallingAssembly().GetName().Name + sFolderNameModel;

                string root = AppDomain.CurrentDomain.BaseDirectory;
                string[] sArray = root.Split('\\');

                string sSolutionFolderName = "BE\\1.Libraries\\BE.Services\\UnitOfWork\\";

                StringBuilder sbPath = new StringBuilder();
                foreach (var item in sArray)
                {
                    if (item.Contains("BEnterprises"))
                        break;
                    else
                        sbPath.Append(item + "\\");
                }

                string sConcatPath = sbPath + sSolutionFolderName;

                //string subdir = root + sModelClassFilesSaveInFolder;
                //string sPath = subdir;

                string subdir = sConcatPath;
                string sPath = sConcatPath;

                #endregion

                #region Caption
                //------------------------------------------------------------------------------
                // <auto-generated>
                //     This code was generated from a template.
                //
                //     Manual changes to this file may cause unexpected behavior in your application.
                //     Manual changes to this file will be overwritten if the code is regenerated.
                // </auto-generated>
                //------------------------------------------------------------------------------
                #endregion


                string path = @"" + sPath + "UnitOfWork.cs";
                //if (File.Exists(path))
                //{
                //    File.Delete(path);
                //    File.Create(path);
                //}
                //else
                //    File.Create(path);

                // Default NameSpace
                //using System;
                //using System.Collections.Generic;
                //using System.Linq;
                //using System.Web;

                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(@"//------------------------------------------------------------------------------");
                    sw.WriteLine(@"// <auto-generated>");
                    sw.WriteLine(@"//     This code was generated from a template.");
                    sw.WriteLine(@"//     Manual changes to this file may cause unexpected behavior in your application.");
                    sw.WriteLine(@"//     Manual changes to this file will be overwritten if the code is regenerated.");
                    sw.WriteLine(@"// </auto-generated>");
                    sw.WriteLine(@"//------------------------------------------------------------------------------");
                    sw.WriteLine(@"");

                    sw.WriteLine(@"using System;");
                    //sw.WriteLine(@"using System.Collections.Generic;");
                    //sw.WriteLine(@"using System.ComponentModel;");
                    //sw.WriteLine(@"using System.ComponentModel.DataAnnotations;");
                    //sw.WriteLine(@"using System.Linq;");
                    sw.WriteLine(@"using BE.Services.RepBEnterprisesitory;");
                    sw.WriteLine(@"using BE.Services.SqlDbConnections;");
                    sw.WriteLine(@"using BE.Core;");
                    sw.WriteLine(@"");

                    sw.WriteLine(@"namespace " + Convert.TBEnterprisestring(sProjectName));
                    sw.WriteLine(@"{");

                    sw.WriteLine(@"    " + "public partial class UnitOfWork : IUnitOfWork, System.IDispBEnterprisesable");
                    sw.WriteLine(@"    " + "{");

                    sw.WriteLine(@"        " + "private readonly SqlDbContext _context;");
                    sw.WriteLine(@"        " + "private bool dispBEnterprisesed;");
                    sw.WriteLine(@"");

                    // Constructure
                    sw.WriteLine(@"        " + "public UnitOfWork()");
                    sw.WriteLine(@"        " + "{");
                    sw.WriteLine(@"            " + "this._context = new SqlDbContext();");
                    sw.WriteLine(@"        " + "}");

                    // DispBEnterprisese
                    sw.WriteLine(@"        " + "public void DispBEnterprisese()");
                    sw.WriteLine(@"        " + "{");
                    sw.WriteLine(@"            " + "DispBEnterprisese(true);");
                    sw.WriteLine(@"            " + "GC.SuppressFinalize(this);");
                    sw.WriteLine(@"        " + "}");

                    // Save
                    sw.WriteLine(@"        " + "public void Save()");
                    sw.WriteLine(@"        " + "{");
                    sw.WriteLine(@"            " + "_context.SaveChanges();");
                    sw.WriteLine(@"        " + "}");

                    // DispBEnterprisese
                    sw.WriteLine(@"        " + "public virtual void DispBEnterprisese(bool dispBEnterprisesing)");
                    sw.WriteLine(@"        " + "{");
                    sw.WriteLine(@"            " + "if (!dispBEnterprisesed)");
                    sw.WriteLine(@"            " + "{");
                    sw.WriteLine(@"                " + "if (dispBEnterprisesing)");
                    sw.WriteLine(@"                " + "{");
                    sw.WriteLine(@"                    " + "_context.DispBEnterprisese();");
                    sw.WriteLine(@"                " + "}");
                    sw.WriteLine(@"            " + "}");
                    sw.WriteLine(@"            " + "dispBEnterprisesed = true;");
                    sw.WriteLine(@"        " + "}");
                    sw.WriteLine(@"");
                    sw.WriteLine(@"");


                    // Models Inherit From IUnitOfWork
                    //public IGenericRepBEnterprisesitory<Dto_Test> Dto_Test_RepBEnterprisesitory { get; set; }
                    //public IGenericRepBEnterprisesitory<Dto_Test> _Dto_Test_RepBEnterprisesitory
                    //{
                    //    get { return Dto_Test_RepBEnterprisesitory ?? (Dto_Test_RepBEnterprisesitory = new GenericRepBEnterprisesitory<Dto_Test>(_context)); }
                    //    set { Dto_Test_RepBEnterprisesitory = value; }
                    //}

                    string propertyTypes = string.Empty;
                    string getSet = "{ get; set; }";

                    foreach (DataRow item in dtAllTables.Rows)
                    {
                        string TablesName = item["TABLE_NAME"].TBEnterprisestring();

                        sw.WriteLine(@"        " + "public IGenericRepBEnterprisesitory<" + TablesName + "> " + TablesName + "_RepBEnterprisesitory " + getSet);

                        sw.WriteLine(@"        " + "public IGenericRepBEnterprisesitory<" + TablesName + "> _" + TablesName + "_RepBEnterprisesitory");
                        sw.WriteLine(@"        " + "{");

                        sw.WriteLine(@"            " + "get { return " + TablesName + "_RepBEnterprisesitory ?? (" + TablesName + "_RepBEnterprisesitory" + "= new GenericRepBEnterprisesitory<" + TablesName + ">(_context)); }");

                        sw.WriteLine(@"            " + "set { _" + TablesName + "_RepBEnterprisesitory" + " = value; }");

                        sw.WriteLine(@"        " + "}");
                    }

                    sw.WriteLine(@"    " + "}");
                    sw.WriteLine(@"}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}