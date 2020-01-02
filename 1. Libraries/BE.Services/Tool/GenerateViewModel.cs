using BE.Services.SqlDbConnections;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;

namespace BE.Services.Tool
{
    public class GenerateViewModel
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

        public GenerateViewModel()
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
                //SystemDictionary systemDictionary = new SystemDictionary();

                GetAllTables();

                #region Block For Files Root Path

                string sFolderNameForModel = ".ViewModel";
                string sModelClassFilesSaveInFolder = "ViewModel";
                string sProjectName = Assembly.GetCallingAssembly().GetName().Name + sFolderNameForModel;

                string root = AppDomain.CurrentDomain.BaseDirectory;
                string[] sArray = root.Split('\\');

                string sSolutionFolderName = "BE\\1. Libraries\\BE.Core\\";

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
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
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
                    string path = @"" + sPath + "\\" + Convert.TBEnterprisestring(item["TABLE_NAME"]) + ".cs";
                    if (File.Exists(path))
                    {
                        continue;
                        //File.Delete(path);
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
                        //sw.WriteLine(@"using System.Collections.Generic;");
                        sw.WriteLine(@"using System.ComponentModel;");
                        sw.WriteLine(@"using System.ComponentModel.DataAnnotations;");
                        string sProjectNameForInheritance = Assembly.GetCallingAssembly().GetName().Name;
                        //sw.WriteLine(string.Format(@"using {0}.{1};", sProjectNameForInheritance, sInheritFromBaseClass));
                        //sw.WriteLine(string.Format(@"using BE.Core.Model;"));
                        //sw.WriteLine(@"using System.Linq;");
                        //sw.WriteLine(@"using System.Web;");
                        sw.WriteLine(@"");

                        sw.WriteLine(@"namespace " + Convert.TBEnterprisestring("BE.Core"));
                        sw.WriteLine(@"{");

                        string sPartialClassMetaDataType = Convert.TBEnterprisestring(item["TABLE_NAME"]) + "_MetaData";
                        //[MetadataType(typeof(urlmetadata))]
                        //public partial class TBurl
                        //{
                        //}

                        string getSet = "{ get; set; }";

                        sw.WriteLine(@"    " + "[MetadataType(typeof(" + sPartialClassMetaDataType + "))]");
                        sw.WriteLine(@"    " + "public partial class " + Convert.TBEnterprisestring(item["TABLE_NAME"]));
                        sw.WriteLine(@"    " + "{");

                        //sw.WriteLine(@"        " + "public string sErrorMessage" + " " + getSet);
                        //sw.WriteLine(@"        " + "public bool bIsSucess" + " " + getSet);

                        sw.WriteLine(@"    " + "}");
                        sw.WriteLine(@"");

                        sw.WriteLine(@"    " + "public class " + sPartialClassMetaDataType);
                        sw.WriteLine(@"    " + "{");

                        GetAllColumns(Convert.TBEnterprisestring(item["TABLE_NAME"]));
                        foreach (DataRow row in dtAllColumns.Rows)
                        {
                            string propertyTypes = string.Empty;

                            string columnName = row["COLUMN_NAME"].TBEnterprisestring();
                            string dataType = row["DATA_TYPE"].TBEnterprisestring();
                            bool isNullable = Convert.ToBoolean(row["IS_NULLABLE"]);

                            //if (columnName == "Photo")
                            //{
                            //    if (dataType == "image")
                            //    {
                            //    }
                            //}

                            propertyTypes = ConvertSqlServerFormatToCSharp(dataType.TBEnterprisestring());
                            if (isNullable && propertyTypes != "string")
                            {
                                if (propertyTypes.Contains("[]"))
                                    propertyTypes = "Nullable<" + (propertyTypes.Split('[')[0]) + ">" + "[]";
                                else
                                {
                                    if (propertyTypes == "DateTime" || propertyTypes == "Date")
                                        propertyTypes = "Nullable<" + "System.DateTime" + ">";
                                    else
                                        propertyTypes = "Nullable<" + ConvertSqlServerFormatToCSharp(dataType.TBEnterprisestring()) + ">";
                                }
                            }
                            else
                            {
                                if (propertyTypes == "DateTime" || propertyTypes == "Date")
                                    propertyTypes = "System.DateTime";
                                else
                                    propertyTypes = ConvertSqlServerFormatToCSharp(dataType.TBEnterprisestring());
                            }

                            //[DisplayName("Amount per square meter")]
                            //[Required(ErrorMessage = "{0} is required")]
                            //public string BackupContactFirstName { get; set; }

                            sw.WriteLine(string.Format(@"        " + "[Display(Name= \"{0}\")]", columnName));
                            if (!isNullable)
                            {
                                string sErrorTag = "{0} is required";
                                sw.WriteLine(string.Format(@"        " + "[Required(ErrorMessage = \"{0}\")]", sErrorTag));
                            }

                            //if (systemDictionary.PhoneNumberInternalDictionary.ContainsKey(columnName))
                            //{
                            //    //[DataType(DataType.PhoneNumber)]
                            //    //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
                            //    sw.WriteLine(string.Format(@"        " + "[RegularExpression(@\" ^\\(? ([0 - 9]{ 3})\\)?[-. ]? ([0 - 9]{3})[-. ]? ([0 - 9]{4})$\", ErrorMessage = \"Not a valid phone number\")]"));
                            //}
                            //if (systemDictionary.EmailInternalDictionary.ContainsKey(columnName))
                            //{
                            //    //[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            //    //@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            //    //@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            //    //ErrorMessage = "Email is not valid")]
                            //    sw.WriteLine(string.Format(@"        " + "[RegularExpression(@\" ^ ([a - zA - Z0 - 9_\\-\\.] +)@((\\[[0 - 9]{ 1,3}\" +@\"\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\" +@\".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$\",ErrorMessage = \"Email is not valid\")]"));
                            //}

                            sw.WriteLine(@"        " + "public" + " " + propertyTypes + " " + columnName + " " + getSet);
                            sw.WriteLine(@"");
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