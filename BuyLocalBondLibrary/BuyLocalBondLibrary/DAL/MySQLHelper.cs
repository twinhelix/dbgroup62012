﻿// Copyright (c) 2004-2008 MySQL AB, 2008-2009 Sun Microsystems, Inc.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as published by
// the Free Software Foundation
//
// There are special exceptions to the terms and conditions of the GPL 
// as it is applied to this software. View the full text of the 
// exception in file EXCEPTIONS in the directory of this software 
// distribution.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Configuration;

namespace StockValuationLibrary.DAL
{
    /// <summary>
    /// Helper class that makes it easier to work with the provider.
    /// </summary>
    public sealed class MySqlHelper
    {
        //below is implementation of connection string - HN 2/26/2010
        public static readonly string SV_CONN_STRING = ConfigurationManager.AppSettings["SV_CONN_STRING"];

        private static string stringOfBackslashChars = "\u005c\u00a5\u0160\u20a9\u2216\ufe68\uff3c";
        private static string stringOfQuoteChars =
            "\u0027\u0060\u00b4\u02b9\u02ba\u02bb\u02bc\u02c8\u02ca\u02cb\u02d9\u0300\u0301\u2018\u2019\u201a\u2032\u2035\u275b\u275c\uff07";

        // this class provides only static methods
        private MySqlHelper()
        {
        }

        #region ExecuteNonQuery

        /// <summary>
        /// Executes a single command against a MySQL database.  The <see cref="MySqlConnection"/> is assumed to be
        /// open when the method is called and remains open after the method completes.
        /// </summary>
        /// <param name="connection"><see cref="MySqlConnection"/> object to use</param>
        /// <param name="commandText">SQL command to be executed</param>
        /// <param name="commandParameters">Array of <see cref="MySqlParameter"/> objects to use with the command.</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.Text;

            if (commandParameters != null)
                foreach (MySqlParameter p in commandParameters)
                    cmd.Parameters.Add(p);

            int result = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            return result;
        }

        /// <summary>
        /// Executes a single command against a MySQL database.  A new <see cref="MySqlConnection"/> is created
        /// using the <see cref="MySqlConnection.ConnectionString"/> given.
        /// </summary>
        /// <param name="connectionString"><see cref="MySqlConnection.ConnectionString"/> to use</param>
        /// <param name="commandText">SQL command to be executed</param>
        /// <param name="parms">Array of <see cref="MySqlParameter"/> objects to use with the command.</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, string commandText, params MySqlParameter[] parms)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (MySqlConnection cn = new MySqlConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteNonQuery(cn, commandText, parms);
            }
        }
        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// Executes a single SQL command and returns the first row of the resultset.  A new MySqlConnection object
        /// is created, opened, and closed during this method.
        /// </summary>
        /// <param name="connectionString">Settings to be used for the connection</param>
        /// <param name="commandText">Command to execute</param>
        /// <param name="parms">Parameters to use for the command</param>
        /// <returns>DataRow containing the first row of the resultset</returns>
        public static DataRow ExecuteDataRow(string connectionString, string commandText, params MySqlParameter[] parms)
        {
            DataSet ds = ExecuteDataset(connectionString, commandText, parms);
            if (ds == null) return null;
            if (ds.Tables.Count == 0) return null;
            if (ds.Tables[0].Rows.Count == 0) return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// Executes a single SQL command and returns the resultset in a <see cref="DataSet"/>.  
        /// A new MySqlConnection object is created, opened, and closed during this method.
        /// </summary>
        /// <param name="connectionString">Settings to be used for the connection</param>
        /// <param name="commandText">Command to execute</param>
        /// <returns><see cref="DataSet"/> containing the resultset</returns>
        public static DataSet ExecuteDataset(string connectionString, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteDataset(connectionString, commandText, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a single SQL command and returns the resultset in a <see cref="DataSet"/>.  
        /// A new MySqlConnection object is created, opened, and closed during this method.
        /// </summary>
        /// <param name="connectionString">Settings to be used for the connection</param>
        /// <param name="commandText">Command to execute</param>
        /// <param name="commandParameters">Parameters to use for the command</param>
        /// <returns><see cref="DataSet"/> containing the resultset</returns>
        public static DataSet ExecuteDataset(string connectionString, string commandText, params MySqlParameter[] commandParameters)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (MySqlConnection cn = new MySqlConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteDataset(cn, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Executes a single SQL command and returns the resultset in a <see cref="DataSet"/>.  
        /// The state of the <see cref="MySqlConnection"/> object remains unchanged after execution
        /// of this method.
        /// </summary>
        /// <param name="connection"><see cref="MySqlConnection"/> object to use</param>
        /// <param name="commandText">Command to execute</param>
        /// <returns><see cref="DataSet"/> containing the resultset</returns>
        public static DataSet ExecuteDataset(MySqlConnection connection, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteDataset(connection, commandText, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a single SQL command and returns the resultset in a <see cref="DataSet"/>.  
        /// The state of the <see cref="MySqlConnection"/> object remains unchanged after execution
        /// of this method.
        /// </summary>
        /// <param name="connection"><see cref="MySqlConnection"/> object to use</param>
        /// <param name="commandText">Command to execute</param>
        /// <param name="commandParameters">Parameters to use for the command</param>
        /// <returns><see cref="DataSet"/> containing the resultset</returns>
        public static DataSet ExecuteDataset(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.Text;

            if (commandParameters != null)
                foreach (MySqlParameter p in commandParameters)
                    cmd.Parameters.Add(p);

            //create the DataAdapter & DataSet
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            //fill the DataSet using default values for DataTable names, etc.
            da.Fill(ds);

            // detach the MySqlParameters from the command object, so they can be used again.			
            cmd.Parameters.Clear();

            //return the dataset
            return ds;
        }

        /// <summary>
        /// Updates the given table with data from the given <see cref="DataSet"/>
        /// </summary>
        /// <param name="connectionString">Settings to use for the update</param>
        /// <param name="commandText">Command text to use for the update</param>
        /// <param name="ds"><see cref="DataSet"/> containing the new data to use in the update</param>
        /// <param name="tablename">Tablename in the dataset to update</param>
        public static void UpdateDataSet(string connectionString, string commandText, DataSet ds, string tablename)
        {
            MySqlConnection cn = new MySqlConnection(connectionString);
            cn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(commandText, cn);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            cb.ToString();
            da.Update(ds, tablename);
            cn.Close();
        }

        #endregion

        #region ExecuteDataReader

        /// <summary>
        /// Executes a single command against a MySQL database, possibly inside an existing transaction.
        /// </summary>
        /// <param name="connection"><see cref="MySqlConnection"/> object to use for the command</param>
        /// <param name="transaction"><see cref="MySqlTransaction"/> object to use for the command</param>
        /// <param name="commandText">Command text to use</param>
        /// <param name="commandParameters">Array of <see cref="MySqlParameter"/> objects to use with the command</param>
        /// <param name="ExternalConn">True if the connection should be preserved, false if not</param>
        /// <returns><see cref="MySqlDataReader"/> object ready to read the results of the command</returns>
        private static MySqlDataReader ExecuteReader(MySqlConnection connection, MySqlTransaction transaction, string commandText, MySqlParameter[] commandParameters, bool ExternalConn)
        {
            //create a command and prepare it for execution
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.Text;

            if (commandParameters != null)
                foreach (MySqlParameter p in commandParameters)
                    cmd.Parameters.Add(p);

            //create a reader
            MySqlDataReader dr;

            // call ExecuteReader with the appropriate CommandBehavior
            if (ExternalConn)
            {
                dr = cmd.ExecuteReader();
            }
            else
            {
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

            // detach the SqlParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();

            return dr;
        }

        /// <summary>
        /// Executes a single command against a MySQL database.
        /// </summary>
        /// <param name="connectionString">Settings to use for this command</param>
        /// <param name="commandText">Command text to use</param>
        /// <returns><see cref="MySqlDataReader"/> object ready to read the results of the command</returns>
        public static MySqlDataReader ExecuteReader(string connectionString, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteReader(connectionString, commandText, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a single command against a MySQL database.
        /// </summary>
        /// <param name="connectionString">Settings to use for this command</param>
        /// <param name="commandText">Command text to use</param>
        /// <param name="commandParameters">Array of <see cref="MySqlParameter"/> objects to use with the command</param>
        /// <returns><see cref="MySqlDataReader"/> object ready to read the results of the command</returns>
        public static MySqlDataReader ExecuteReader(string connectionString, string commandText, params MySqlParameter[] commandParameters)
        {
            //create & open a SqlConnection
            MySqlConnection cn = new MySqlConnection(connectionString);
            cn.Open();

            try
            {
                //call the private overload that takes an internally owned connection in place of the connection string
                return ExecuteReader(cn, null, commandText, commandParameters, false);
            }
            catch
            {
                //if we fail to return the SqlDatReader, we need to close the connection ourselves
                cn.Close();
                throw;
            }
        }
        #endregion

        #region ExecuteScalar

        /// <summary>
        /// Execute a single command against a MySQL database.
        /// </summary>
        /// <param name="connectionString">Settings to use for the update</param>
        /// <param name="commandText">Command text to use for the update</param>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
        public static object ExecuteScalar(string connectionString, string commandText)
        {
            //pass through the call providing null for the set of MySqlParameters
            return ExecuteScalar(connectionString, commandText, (MySqlParameter[])null);
        }

        /// <summary>
        /// Execute a single command against a MySQL database.
        /// </summary>
        /// <param name="connectionString">Settings to use for the command</param>
        /// <param name="commandText">Command text to use for the command</param>
        /// <param name="commandParameters">Parameters to use for the command</param>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
        public static object ExecuteScalar(string connectionString, string commandText, params MySqlParameter[] commandParameters)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (MySqlConnection cn = new MySqlConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteScalar(cn, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Execute a single command against a MySQL database.
        /// </summary>
        /// <param name="connection"><see cref="MySqlConnection"/> object to use</param>
        /// <param name="commandText">Command text to use for the command</param>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
        public static object ExecuteScalar(MySqlConnection connection, string commandText)
        {
            //pass through the call providing null for the set of MySqlParameters
            return ExecuteScalar(connection, commandText, (MySqlParameter[])null);
        }

        /// <summary>
        /// Execute a single command against a MySQL database.
        /// </summary>
        /// <param name="connection"><see cref="MySqlConnection"/> object to use</param>
        /// <param name="commandText">Command text to use for the command</param>
        /// <param name="commandParameters">Parameters to use for the command</param>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
        public static object ExecuteScalar(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.Text;

            if (commandParameters != null)
                foreach (MySqlParameter p in commandParameters)
                    cmd.Parameters.Add(p);

            //execute the command & return the results
            object retval = cmd.ExecuteScalar();

            // detach the SqlParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();
            return retval;

        }

        #endregion

        #region Utility methods

        /// <summary>
        /// Escapes the string.
        /// </summary>
        /// <param name="value">The string to escape</param>
        /// <returns>The string with all quotes escaped.</returns>
        public static string EscapeString(string value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                if (stringOfQuoteChars.IndexOf(c) >= 0 ||
                    //sb.Append(c);
                    //else if (
                stringOfBackslashChars.IndexOf(c) >= 0)
                    sb.Append("\\");
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static string DoubleQuoteString(string value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                if (stringOfQuoteChars.IndexOf(c) >= 0)
                    sb.Append(c);
                else if (stringOfBackslashChars.IndexOf(c) >= 0)
                    sb.Append("\\");
                sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Helper method that translates a character or string value from the Database into the bool quivalent.
        /// The client to this method must supply the string equivalent of what the database stores
        /// as a true value.
        /// </summary>
        /// <param name="objFromDB">Object representation of a bool value from the database</param>
        /// <param name="trueStr">String representing a true value dictated by the db model</param>
        /// <returns></returns>
        public static bool ConvertToBool(object objFromDB, string trueStr)
        {
            if (objFromDB == System.DBNull.Value)
            {
                return false;
            }
            string str = objFromDB.ToString();
            str = str.Trim();
            if (str == string.Empty) { return true; }
            str = str.ToLower();
            return trueStr.ToLower().Equals(str);
        }

        public static string ConvertReaderToString(MySqlDataReader rdr, string columnName)
        {
            if (HasColumn(rdr, columnName))
            {
                return Convert.ToString(rdr[columnName] == System.DBNull.Value ?
                string.Empty : rdr[columnName]).Trim();
            }
            else { return string.Empty; }
        }

        public static int ConvertReaderToInt(MySqlDataReader rdr, string columnName)
        {
            if (HasColumn(rdr, columnName))
            {
                return Convert.ToInt32(rdr[columnName] == System.DBNull.Value ?
                0 : rdr[columnName]);
            }
            else { return 0; }
        }

        public static double ConvertReaderToDouble(MySqlDataReader rdr, string columnName)
        {
            if (HasColumn(rdr, columnName))
            {
                return Convert.ToDouble(rdr[columnName] == System.DBNull.Value ?
                0 : rdr[columnName]);
            }
            else { return 0; }
        }

        public static decimal ConvertReaderToDecimal(MySqlDataReader rdr, string columnName)
        {
            if (HasColumn(rdr, columnName))
            {
                return Convert.ToDecimal(rdr[columnName] == System.DBNull.Value ?
                0 : rdr[columnName]);
            }
            else { return 0; }
        }

        public static DateTime ConvertReaderToDateTime(MySqlDataReader rdr, string columnName)
        {
            if (HasColumn(rdr, columnName))
            {
                return Convert.ToDateTime(rdr[columnName] == System.DBNull.Value ?
                DateTime.MinValue : rdr[columnName]);
            }
            else { return DateTime.MinValue; }
        }

        public static bool HasColumn(IDataReader r, string columnName)
        {
            try
            {
                return r.GetOrdinal(columnName) >= 0;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        #endregion
    }
}
