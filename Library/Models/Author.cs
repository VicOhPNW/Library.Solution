using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
  public class Author
  {
    private int _id;
    private string _name;

    public Author(string name, int id=0)
    {
      _id=id;
      _name=name;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO authors (name) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Author> GetAll()
    {
      List<Author> allAuthors = new List<Author>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM authors;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int authorId = rdr.GetInt32(0);
        string authorName = rdr.GetString(1);

        Author newAuthor = new Author(authorName, authorId);
        allAuthors.Add(newAuthor);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allAuthors;
    }

    public static Author Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM authors WHERE id = (@searchId);";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int authorId = 0;
        string authorName = "";

        rdr.Read();
        authorId = rdr.GetInt32(0);
        authorName = rdr.GetString(1);

        Author newAuthor = new Author(authorName, authorId);
        conn.Close();
        if (conn != null)
      {
        conn.Dispose();
      }

      return newAuthor;
      }

  }
}
