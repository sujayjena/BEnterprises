using BE.Services.DbConnections;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;

namespace BE.Services.Tool
{
    public class GenerateModel
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
        private static readonly string[] CSharpTypes = { "long", "byte[]", "bool", "char", "DateTime", "DateTime", "DateTime", "DateTimeOffset", "decimal", "byte[]", "double", "Microsoft.SqlServer.Types.SqlGeography", "Microsoft.SqlServer.Types.SqlGeometry", "Microsoft.SqlServer.Types.SqlHierarchyId", "byte[]", "int", "decimal", "string", "string", "decimal", "string", "Single", "byte[]", "DateTime", "short", "decimal", "object", "string", "TimeSpan", "byte[]", "byte", "Guid", "bite[]", "string", "string" };
        public static string ConvertSqlServerFormatToCSharp(string typeName)
        {
            var index = Array.IndexOf(SqlServerTypes, typeName);

            return index > -1
                ? CSharpTypes[index]
                : "object";
        }
        public static string ConvertCSharpFormatToSqlServer(string typeName)
        {
            var index = Array.IndexOf(CSharpTypes, typeName);

            return index > -1
                ? SqlServerTypes[index]
                : null;
        }

        #endregion

        #region Constructure

        public GenerateModel()
        {
            GenerateModelFiles();
        }

        #endregion

        #region Block For Get All Tables and Column Respective Particullar Table

        public DataTable GetAllTables()
        {
            string sqlQuery = @"SELECT TABLE_NAME
                                FROM INFORMATION_SCHEMA.TABLES
                                WHERE (TABLE_SCHEMA = 'dbo')
                                AND TABLE_NAME <> 'sysdiagrams'
                                ORDER BY TABLE_NAME";

            dtAllTables = new DataTable();

            using (SqlConnection connection = new SqlDbConnection()._sqlConn)
            {
                connection.Open();
                _dataSet = new DataSet();
                try
                {
                    _sqlCommand = new SqlCommand { CommandText = sqlQuery };
                    _sqlDataAdapter = new SqlDataAdapter(Convert.ToString(sqlQuery), connection);
                    _sqlDataAdapter.Fill(dtAllTables);

                }
                catch (Exception)
                {
                    _sqlCommand.Dispose();
                }
                finally
                {
                    connection.Close();
                    _sqlCommand.Dispose();
                    if (_sqlDataAdapter != null)
                        _sqlDataAdapter.Dispose();
                }
                return dtAllTables;
            }
        }

        public DataTable GetAllColumns(string TableName)
        {
            string sqlQuery = @"SELECT COLUMN_NAME,DATA_TYPE,CASE WHEN IS_NULLABLE = 'YES' THEN 1 ELSE 0 END AS IS_NULLABLE
                                FROM INFORMATION_SCHEMA.COLUMNS
                                WHERE TABLE_NAME = '" + TableName + "' ORDER BY ORDINAL_POSITION";

            dtAllColumns = new DataTable();
            using (SqlConnection connection = new SqlDbConnection()._sqlConn)
            {
                connection.Open();
                _dataSet = new DataSet();
                try
                {
                    _sqlCommand = new SqlCommand { CommandText = sqlQuery };
                    _sqlDataAdapter = new SqlDataAdapter(Convert.ToString(sqlQuery), connection);
                    DataSet ds = new DataSet();
                    _sqlDataAdapter.Fill(ds, TableName);
                    dtAllColumns = ds.Tables[0];
                }
                catch (Exception)
                {
                    _sqlCommand.Dispose();
                }
                finally
                {
                    connection.Close();
                    _sqlCommand.Dispose();
                    if (_sqlDataAdapter != null)
                        _sqlDataAdapter.Dispose();
                }
                return dtAllColumns;
            }
        }

        public DataTable CheckPrimaryKeyOfTable(string TableName)
        {
            string sqlQuery = @"SELECT     X.NAME AS INDEXNAME,
                                COL_NAME(IC.OBJECT_ID,IC.COLUMN_ID) AS COLUMNNAME
                                FROM       SYS.INDEXES  X 
                                INNER JOIN SYS.INDEX_COLUMNS  IC 
                                ON X.OBJECT_ID = IC.OBJECT_ID
                                AND X.INDEX_ID = IC.INDEX_ID
                                WHERE      X.IS_PRIMARY_KEY = 1
                                AND      OBJECT_NAME(IC.OBJECT_ID)='" + TableName + "'";

            DataTable dtAllPrimaryKey = new DataTable();
            using (SqlConnection connection = new SqlDbConnection()._sqlConn)
            {
                connection.Open();
                _dataSet = new DataSet();
                try
                {
                    _sqlCommand = new SqlCommand { CommandText = sqlQuery };
                    _sqlDataAdapter = new SqlDataAdapter(Convert.ToString(sqlQuery), connection);
                    DataSet ds = new DataSet();
                    _sqlDataAdapter.Fill(ds, TableName);
                    dtAllPrimaryKey = ds.Tables[0];
                }
                catch (Exception)
                {
                    _sqlCommand.Dispose();
                }
                finally
                {
                    connection.Close();
                    _sqlCommand.Dispose();
                    if (_sqlDataAdapter != null)
                        _sqlDataAdapter.Dispose();
                }
                return dtAllPrimaryKey;
            }
        }

        public DataTable CheckForeignKeyOfTable(string TableName)
        {
            string sqlQuery = @"SELECT 
                                ccu.table_name AS SourceTable
                                ,ccu.constraint_name AS SourceConstraint
                                ,ccu.column_name AS SourceColumn
                                ,kcu.table_name AS TargetTable
                                ,kcu.column_name AS TargetColumn
                                FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu
                                INNER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc
                                ON ccu.CONSTRAINT_NAME = rc.CONSTRAINT_NAME 
                                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu 
                                ON kcu.CONSTRAINT_NAME = rc.UNIQUE_CONSTRAINT_NAME  
	                            WHERE ccu.table_name ='" + TableName + "' ORDER BY ccu.table_name";

            DataTable dtAllForeignKey = new DataTable();
            using (SqlConnection connection = new SqlDbConnection()._sqlConn)
            {
                connection.Open();
                _dataSet = new DataSet();
                try
                {
                    _sqlCommand = new SqlCommand { CommandText = sqlQuery };
                    _sqlDataAdapter = new SqlDataAdapter(Convert.ToString(sqlQuery), connection);
                    DataSet ds = new DataSet();
                    _sqlDataAdapter.Fill(ds, TableName);
                    dtAllForeignKey = ds.Tables[0];
                }
                catch (Exception)
                {
                    _sqlCommand.Dispose();
                }
                finally
                {
                    connection.Close();
                    _sqlCommand.Dispose();
                    if (_sqlDataAdapter != null)
                        _sqlDataAdapter.Dispose();
                }
                return dtAllForeignKey;
            }
        }

        public DataTable CheckListOfForeignKeyReferenceFromThisTable(string TableName)
        {
            string sqlQuery = @"SELECT 
                                ccu.table_name AS SourceTable
                                ,ccu.constraint_name AS SourceConstraint
                                ,ccu.column_name AS SourceColumn
                                ,kcu.table_name AS TargetTable
                                ,kcu.column_name AS TargetColumn
                                FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu
                                INNER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc
                                ON ccu.CONSTRAINT_NAME = rc.CONSTRAINT_NAME 
                                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu 
                                ON kcu.CONSTRAINT_NAME = rc.UNIQUE_CONSTRAINT_NAME  
	                            WHERE kcu.table_name ='" + TableName + "' ORDER BY ccu.table_name";

            DataTable dtAllForeignKey = new DataTable();
            using (SqlConnection connection = new SqlDbConnection()._sqlConn)
            {
                connection.Open();
                _dataSet = new DataSet();
                try
                {
                    _sqlCommand = new SqlCommand { CommandText = sqlQuery };
                    _sqlDataAdapter = new SqlDataAdapter(Convert.ToString(sqlQuery), connection);
                    DataSet ds = new DataSet();
                    _sqlDataAdapter.Fill(ds, TableName);
                    dtAllForeignKey = ds.Tables[0];
                }
                catch (Exception)
                {
                    _sqlCommand.Dispose();
                }
                finally
                {
                    connection.Close();
                    _sqlCommand.Dispose();
                    if (_sqlDataAdapter != null)
                        _sqlDataAdapter.Dispose();
                }
                return dtAllForeignKey;
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

                string sFolderNameModel = ".Model";
                string sModelClassFilesSaveInFolder = "Model";
                string sProjectName = Assembly.GetCallingAssembly().GetName().Name + sFolderNameModel;

                string root = AppDomain.CurrentDomain.BaseDirectory;
                string[] sArray = root.Split('\\');

                string sSolutionFolderName = "BEnterprises\\1. Libraries\\BE.Core\\";

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

                string subdir = sConcatPath + sModelClassFilesSaveInFolder;
                string sPath = sConcatPath + sModelClassFilesSaveInFolder;

                // If directory does not exist, create it. 
                if (!Directory.Exists(subdir))
                {
                    Directory.CreateDirectory(subdir);
                }

                // Create sub directory
                //if (!Directory.Exists(subdir))
                //{
                //    Directory.CreateDirectory(subdir);
                //}

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

                foreach (DataRow item in dtAllTables.Rows)
                {
                    string path = @"" + sPath + "\\" + Convert.ToString(item["TABLE_NAME"]) + ".cs";
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

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
                        sw.WriteLine(@"using System.Collections.Generic;");
                        //sw.WriteLine(@"using System.ComponentModel;");
                        sw.WriteLine(@"using System.ComponentModel.DataAnnotations;");
                        //sw.WriteLine(@"using System.Linq;");
                        //sw.WriteLine(@"using System.Web;");
                        sw.WriteLine(@"");

                        sw.WriteLine(@"namespace " + Convert.ToString("BE.Core"));
                        sw.WriteLine(@"{");

                        sw.WriteLine(@"    " + "public partial class " + Convert.ToString(item["TABLE_NAME"]));
                        sw.WriteLine(@"    " + "{");



                        // Check if any FK reference from this table
                        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
                        //public Menu_Categories()
                        //{
                        //    this.Employees = new HashSet<Employee>();
                        //    this.U_User = new HashSet<U_User>();
                        //}
                        DataTable dtListOfForeignKeyReferenceFromThisTable = CheckListOfForeignKeyReferenceFromThisTable(Convert.ToString(item["TABLE_NAME"]));
                        if (dtListOfForeignKeyReferenceFromThisTable.Rows.Count > 0)
                        {
                            string guidListOfForeignKeyReference = Guid.NewGuid().ToString();
                            sw.WriteLine(@"        " + "[System.Diagnostics.CodeAnalysis.SuppressMessage(\"Microsoft.Usage\", \"" + guidListOfForeignKeyReference + ":DoNotCallOverridableMethodsInConstructors\")]");
                            sw.WriteLine(@"        " + "public " + Convert.ToString(item["TABLE_NAME"]) + "()");
                            sw.WriteLine(@"        " + "{");
                            foreach (DataRow itemListOfForeignKeyReference in dtListOfForeignKeyReferenceFromThisTable.Rows)
                            {
                                sw.WriteLine(@"        " + "   this.{0} = new HashSet<{1}>();", Convert.ToString(itemListOfForeignKeyReference["SourceTable"]), Convert.ToString(itemListOfForeignKeyReference["SourceTable"]));
                            }
                            sw.WriteLine(@"        " + "}");
                            sw.WriteLine(@"");
                        }

                        GetAllColumns(Convert.ToString(item["TABLE_NAME"]));
                        DataTable dtPrimaryKey = CheckPrimaryKeyOfTable(Convert.ToString(item["TABLE_NAME"]));
                        string sPrimaryKey = string.Empty;
                        if (dtPrimaryKey.Rows.Count > 0)
                        {
                            sPrimaryKey = Convert.ToString(dtPrimaryKey.Rows[0]["COLUMNNAME"]);
                        }
                        DataTable dtForeignKey = CheckForeignKeyOfTable(Convert.ToString(item["TABLE_NAME"]));



                        string getSet = "{ get; set; }";
                        int iInstedOfPrimaryKey = 0;
                        foreach (DataRow row in dtAllColumns.Rows)
                        {
                            string propertyTypes = string.Empty;
                            string columnName = row["COLUMN_NAME"].ToString();
                            string dataType = row["DATA_TYPE"].ToString();
                            bool isNullable = Convert.ToBoolean(row["IS_NULLABLE"]);

                            //if (columnName == "Photo")
                            //{
                            //    if (dataType == "image")
                            //    {
                            //    }
                            //}

                            propertyTypes = ConvertSqlServerFormatToCSharp(dataType.ToString());
                            if (isNullable && propertyTypes != "string")
                            {
                                if (propertyTypes.Contains("[]"))
                                    propertyTypes = "Nullable<" + (propertyTypes.Split('[')[0]) + ">" + "[]";
                                else
                                {
                                    if (propertyTypes == "DateTime" || propertyTypes == "Date")
                                        propertyTypes = "Nullable<" + "System.DateTime" + ">";
                                    else
                                        propertyTypes = "Nullable<" + ConvertSqlServerFormatToCSharp(dataType.ToString()) + ">";
                                }
                            }
                            else
                            {
                                if (propertyTypes == "DateTime" || propertyTypes == "Date")
                                    propertyTypes = "System.DateTime";
                                else
                                    propertyTypes = ConvertSqlServerFormatToCSharp(dataType.ToString());
                            }

                            if (sPrimaryKey.Length > 0 && sPrimaryKey == columnName)
                            {
                                sw.WriteLine(@"        [Key]");
                                iInstedOfPrimaryKey++;
                            }
                            else if (iInstedOfPrimaryKey == 0)
                            {
                                sw.WriteLine(@"[Key]");
                                iInstedOfPrimaryKey++;
                            }

                            sw.WriteLine(@"        " + "public" + " " + propertyTypes + " " + columnName + " " + getSet);
                        }
                        // Check Foreign Key
                        sw.WriteLine(@"");
                        foreach (DataRow dritem in dtForeignKey.Rows)
                        {
                            //public virtual User User { get; set; }
                            sw.WriteLine(@"        " + "public" + " virtual" + " " + Convert.ToString(dritem["TargetTable"]) + " " + Convert.ToString(dritem["TargetTable"]) + " " + getSet);
                        }

                        // Check if any FK reference from this table
                        if (dtListOfForeignKeyReferenceFromThisTable.Rows.Count > 0)
                        {
                            sw.WriteLine(@"");
                            string guidListOfForeignKeyReference1 = Guid.NewGuid().ToString();
                            foreach (DataRow itemListOfForeignKeyReference in dtListOfForeignKeyReferenceFromThisTable.Rows)
                            {
                                //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
                                //public virtual ICollection<Employee> Employees { get; set; }
                                //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
                                //public virtual ICollection<U_User> U_User { get; set; }

                                sw.WriteLine(@"        " + "[System.Diagnostics.CodeAnalysis.SuppressMessage(\"Microsoft.Usage\", \"" + guidListOfForeignKeyReference1 + ":CollectionPropertiesShouldBeReadOnly\")]");
                                //sw.WriteLine(@"        " + "public virtual ICollection<{0}> {1} { get; set; }", Convert.ToString(itemListOfForeignKeyReference["SourceTable"]).Trim(), Convert.ToString(itemListOfForeignKeyReference["SourceTable"]).Trim());
                                sw.WriteLine(@"        " + "public virtual ICollection<" + Convert.ToString(itemListOfForeignKeyReference["SourceTable"]) + "> " + Convert.ToString(itemListOfForeignKeyReference["SourceTable"]) + " " + getSet);
                            }
                        }

                        sw.WriteLine(@"    " + "}");
                        sw.WriteLine(@"}");
                    }
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