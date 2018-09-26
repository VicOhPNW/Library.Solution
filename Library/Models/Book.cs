using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class Book
    {
      private int _id;
      private string _title;
      private string _author;
      private int _copies;
      private string _description;

      public Book (string title, string author, int copies, string description, int id=0)
      {
        _id=id;
        _title=title;
        _author=author;
        _copies=copies;
        _description=description;
      }

      public int GetId()
      {
        return _id;
      }

      public string GetTitle()
      {
        return _title;
      }

      public string GetAuthor()
      {
        return _author;
      }

      public int GetCopies()
      {
        return _copies;
      }

      public string GetDescription()
      {
        return _description;
      }

      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO books (title, author, copies, description) VALUES (@title, @author, @copies, @description);";

        MySqlParameter title = new MySqlParameter();
        title.ParameterName = "@title";
        title.Value = this._title;
        cmd.Parameters.Add(title);

        MySqlParameter author = new MySqlParameter();
        author.ParameterName = "@author";
        author.Value = this._author;
        cmd.Parameters.Add(author);

        MySqlParameter copies = new MySqlParameter();
        copies.ParameterName = "@copies";
        copies.Value = this._copies;
        cmd.Parameters.Add(copies);

        MySqlParameter description = new MySqlParameter();
        description.ParameterName = "@description";
        description.Value = this._description;
        cmd.Parameters.Add(description);

        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }
    }
}
