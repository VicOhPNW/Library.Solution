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

      public void AddBook(Book newBook)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO books_authors (book_id, author_id) VALUES (@BookId, @AuthorId);";

      MySqlParameter author_id = new MySqlParameter();
      author_id.ParameterName = "@AuthorId";
      author_id.Value = _id;
      cmd.Parameters.Add(author_id);

      MySqlParameter book_id = new MySqlParameter();
      book_id.ParameterName = "@BookId";
      book_id.Value = newBook.GetId();
      cmd.Parameters.Add(book_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

        public List<Book> GetBooks()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT books.* FROM authors
      JOIN books_authors ON (authors.id = books_authors.author_id)
      JOIN books ON (books_authors.book_id = books.id)
      WHERE authors.id = @AuthorId;";

      MySqlParameter authorIdParameter = new MySqlParameter();
      authorIdParameter.ParameterName = "@AuthorId";
      authorIdParameter.Value = _id;
      cmd.Parameters.Add(authorIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Book> books = new List<Book>{};

      while(rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        string bookTitle = rdr.GetString(1);
        string bookAuthor = rdr.GetString(2);
        int bookCopies = rdr.GetInt32(3);
        string bookDescription = rdr.GetString(4);

        Book newBook = new Book(bookTitle, bookAuthor, bookCopies, bookDescription, bookId);
        books.Add(newBook);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return books;
    }



  }
}
