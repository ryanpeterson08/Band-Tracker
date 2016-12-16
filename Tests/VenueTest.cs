using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void ReplacesEqualObjects_True()
    {

      Venue venueOne = new Venue("Crystal Ballroom");
      Venue venueTwo = new Venue("Crystal Ballroom");

      Assert.Equal(venueOne, venueTwo);
    }

    [Fact]
    public void GetAll_true()
    {
      //Arrange
      Venue venueOne = new Venue("Crystal Ballroom");
      venueOne.Save();
      Venue venueTwo = new Venue("Moda Center");
      venueTwo.Save();
      // Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.Equal(2, result);
    }

    [Fact]
    public void Save_SavesToDatabase_true()
    {
      //Arrange
      Venue testVenue = new Venue("Crystal Ballroom");
      testVenue.Save();
      //Act

      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};
      //Assert
      Assert.Equal(testList, result);
    }

    // [Fact]
    // public void AddAuthor_AddsAuthorToVenue_True()
    // {
    //   Venue newVenue = new Venue("Mr Wiggles");
    //   newVenue.Save();
    //   Author newAuthor = new Author("Ryan");
    //   newAuthor.Save();
    //   newVenue.AddAuthor(newAuthor);
    //   List<Author> expected = new List<Author>{newAuthor};
    //   List<Author> result = newVenue.GetAuthors();
    //
    //   Assert.Equal(expected, result);
    // }
    //
    // [Fact]
    // public void Find_FindsVenueInDatabase_true()
    // {
    //   //Arrange
    //   Venue testVenue = new Venue("Mr Wiggles");
    //   testVenue.Save();
    //
    //   //Act
    //   Venue foundVenue = Venue.Find(testVenue.GetId());
    //
    //   //Assert
    //   Assert.Equal(testVenue, foundVenue);
    // }
    //
    // [Fact]
    // public void Test_Deletes_Venue()
    // {
    //   Venue newVenue = new Venue("Mr Wiggles");
    //
    //   newVenue.Save();
    //   newVenue.Delete();
    //
    //   List<Venue> expected = new List<Venue>{};
    //   List<Venue> result = Venue.GetAll();
    //
    //   Assert.Equal(expected, result);
    //
    // }
    // [Fact]
    // public void Delete_DeletesAssociation_True()
    // {
    //   Venue newVenue = new Venue("Mr Wiggles");
    //   newVenue.Save();
    //   Author newAuthor = new Author("Shel Silverstein");
    //   newVenue.Save();
    //   newVenue.AddAuthor(newAuthor);
    //   newVenue.Delete();
    //
    //   List<Author> result = newVenue.GetAuthors();
    //   List<Author> expected = new List<Author>{};
    //   Assert.Equal(expected, result);
    //
    // }
    //
    // [Fact]
    // public void Update_UpdatesDatabase_True()
    // {
    //   Venue newVenue = new Venue("Mr Wiggles");
    //   newVenue.Save();
    //   newVenue.Update("Mr Jiggles");
    //
    //   Venue testVenue = new Venue("Mr Jiggles");
    //   Venue result = Venue.Find(newVenue.GetId());
    //
    //   Assert.Equal(testVenue, result);
    // }
    //
    // [Fact]
    // public void Search_SearchVenueByAuthor()
    // {
    //   Author newAuthor = new Author("PD Woodhouse");
    //   newAuthor.Save();
    //
    //   Venue venueOne = new Venue("Mr Wiggles");
    //   venueOne.Save();
    //   venueOne.AddAuthor(newAuthor);
    //
    //   Venue venueTwo = new Venue("Mr Muffin");
    //   venueTwo.Save();
    //   venueTwo.AddAuthor(newAuthor);
    //
    //   Venue venueThree = new Venue("Mr Floof");
    //   venueThree.Save();
    //
    //   List<Venue> venuesByAuthor = Venue.SearchAuthor("PD Woodhouse");
    //   List<Venue> expectedVenues = new List<Venue> {venueOne, venueTwo};
    //
    //   Assert.Equal(expectedVenues, venuesByAuthor);
    // }
    //
    // public void Search_SearchVenueByName()
    // {
    //   Venue newVenue = new Venue("Mr Wiggles");
    //   newVenue.Save();
    //
    //   Venue foundVenue = Venue.SearchName("Mr Wiggles");
    //
    //   Assert.Equal(newVenue, foundVenue);
    //  }

    public void Dispose()
    {
      Venue.DeleteAll();
      //Band.DeleteAll();
    }
  }
}
