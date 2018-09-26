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

    public static List<Book> GetAll()
    {
      List<Book> allBooks = new List<Book>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM books;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        string bookTitle = rdr.GetString(1);
        string bookAuthor = rdr.GetString(2);
        int bookCopies = rdr.GetInt32(3);
        string bookDescription = rdr.GetString(4);

        Book newBook = new Book(bookTitle, bookAuthor, bookCopies, bookDescription, bookId);
        allBooks.Add(newBook);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allBooks;
    }

    public static Book Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM books WHERE id = (@searchId);";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int bookId = 0;
        string bookTitle = "";
        string bookAuthor = "";
        int bookCopies = 0;
        string bookDescription = "";

        rdr.Read();
        bookId = rdr.GetInt32(0);
        bookTitle = rdr.GetString(1);
        bookAuthor = rdr.GetString(2);
        bookCopies = rdr.GetInt32(3);
        bookDescription = rdr.GetString(4);

        Book newBook = new Book(bookTitle, bookAuthor, bookCopies, bookDescription, bookId);
        conn.Close();
        if (conn != null)
      {
        conn.Dispose();
      }

      return newBook;
      }


        public void AddAuthor(Author newAuthor)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO books_authors (book_id, author_id) VALUES (@BookId, @AuthorId);";

        MySqlParameter author_id = new MySqlParameter();
        author_id.ParameterName = "@AuthorId";
        author_id.Value = newAuthor.GetId();
        cmd.Parameters.Add(author_id);

        MySqlParameter book_id = new MySqlParameter();
        book_id.ParameterName = "@BookId";
        book_id.Value = _id;
        cmd.Parameters.Add(book_id);

        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public List<Author> GetAuthors()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT authors.* FROM books
      JOIN books_authors ON (books.id = books_authors.book_id)
      JOIN authors ON (books_authors.author_id = authors.id)
      WHERE books.id = @BookId;";

      MySqlParameter bookIdParameter = new MySqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = _id;
      cmd.Parameters.Add(bookIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Author> allAuthors = new List<Author>{};

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



  }
}
