using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker
{
  public class Venue
  {

    private int _id;
    private string _name;


    public Venue(string Name, int Id = 0)
    {
      _name = Name;
      _id = Id;
    }

    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool nameEquality = (this.GetName() == newVenue.GetName());
        return (nameEquality);
      }
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
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@VenueName);", conn);

      SqlParameter venueNameParameter = new SqlParameter("@VenueName", this.GetName());
      cmd.Parameters.Add(venueNameParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int VenueId = rdr.GetInt32(0);
        string VenueName = rdr.GetString(1);
        Venue newVenue = new Venue(VenueName, VenueId);
        allVenues.Add(newVenue);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allVenues;
    }

    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);
      SqlParameter venueIdParameter = new SqlParameter("@VenueId", id.ToString());
      cmd.Parameters.Add(venueIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenueName = null;
      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
      }

      Venue foundVenue = new Venue(foundVenueName, foundVenueId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundVenue;
    }

    public void Update(string VenueName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @VenueName WHERE id = @VenueId;", conn);

      SqlParameter venueIdParameter = new SqlParameter("@VenueId", this.GetId());
      SqlParameter venueNameParameter = new SqlParameter("@VenueName", VenueName);
       cmd.Parameters.Add(venueIdParameter);
       cmd.Parameters.Add(venueNameParameter);

       SqlDataReader rdr = cmd.ExecuteReader();

       while(rdr.Read())
       {
         _id = rdr.GetInt32(0);
         _name = rdr.GetString(1);
       }
       if (rdr != null)
       {
         rdr.Close();
       }
       if (conn != null)
       {
         conn.Close();
       }
     }

    // public void AddAuthor(Author newAuthor)
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("INSERT INTO authors_venues (author_id, venue_id) VALUES (@AuthorId, @VenueId);", conn);
    //
    //   SqlParameter venueIdParameter = new SqlParameter("@VenueId", this.GetId());
    //   cmd.Parameters.Add(venueIdParameter);
    //
    //   SqlParameter authorIdParameter = new SqlParameter("@AuthorId", newAuthor.GetId());
    //   cmd.Parameters.Add(authorIdParameter);
    //
    //   cmd.ExecuteNonQuery();
    //
    //   if(conn != null)
    //   {
    //     conn.Close();
    //   }
    // }

    // public List<Author> GetAuthors()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //   SqlCommand cmd = new SqlCommand("SELECT authors.* FROM venues JOIN authors_venues ON (venues.id = authors_venues.venue_id) JOIN authors ON (authors_venues.author_id = authors.id) WHERE venues.id=@VenueId;",conn);
    //
    //   SqlParameter VenueIdParameter = new SqlParameter("@VenueId", this.GetId());
    //   cmd.Parameters.Add(VenueIdParameter);
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   List<Author> authors = new List<Author>{};
    //   while(rdr.Read())
    //   {
    //     int authorId = rdr.GetInt32(0);
    //     string authorName = rdr.GetString(1);
    //     Author newAuthor = new Author(authorName, authorId);
    //     authors.Add(newAuthor);
    //   }
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //   return authors;
    // }


    //  public static List<Venue> SearchAuthor(string authorName)
    //  {
    //    SqlConnection conn = DB.Connection();
    //    conn.Open();
    //    SqlCommand cmd = new SqlCommand("SELECT venues.* FROM authors JOIN authors_venues ON (authors.id = authors_venues.author_id) JOIN venues ON (authors_venues.venue_id = venues.id) WHERE authors.name=@AuthorName;",conn);
     //
    //    SqlParameter authorNameParameter = new SqlParameter("@AuthorName", authorName);
    //    cmd.Parameters.Add(authorNameParameter);
    //    SqlDataReader rdr = cmd.ExecuteReader();
     //
    //    List<Venue> venues = new List<Venue>{};
    //    while(rdr.Read())
    //    {
    //      int venueId = rdr.GetInt32(0);
    //      string venueName = rdr.GetString(1);
    //      Venue newVenue = new Venue(venueName, venueId);
    //      venues.Add(newVenue);
    //    }
    //    if (rdr != null)
    //    {
    //      rdr.Close();
    //    }
    //    if (conn != null)
    //    {
    //      conn.Close();
    //    }
    //    return venues;
    //  }

    //  public static Venue SearchName(string Name)
    //  {
    //    SqlConnection conn = DB.Connection();
    //    conn.Open();
     //
    //    SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE venues.name = @VenueName;", conn);
     //
    //    SqlParameter venueNameParameter = new SqlParameter("@VenueName", Name);
    //    cmd.Parameters.Add(venueNameParameter);
    //    SqlDataReader rdr = cmd.ExecuteReader();
     //
    //    int foundVenueId = 0;
    //    string foundVenueName = null;
    //    while(rdr.Read())
    //    {
    //      foundVenueId = rdr.GetInt32(0);
    //      foundVenueName = rdr.GetString(1);
    //    }
     //
    //    Venue foundVenue = new Venue(foundVenueName, foundVenueId);
     //
    //    if (rdr != null)
    //    {
    //      rdr.Close();
    //    }
    //    if (conn != null)
    //    {
    //      conn.Close();
    //    }
     //
    //    return foundVenue;
    //  }


    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @VenueId; DELETE FROM authors_venues WHERE venue_id = @VenueId", conn);
      SqlParameter venueIdParameter = new SqlParameter("@VenueId", this.GetId());
      cmd.Parameters.Add(venueIdParameter);
      cmd.ExecuteNonQuery();

      if(conn!=null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
