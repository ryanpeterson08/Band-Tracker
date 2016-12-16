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

      Venue venueOne = new Venue("Crystal Ballroom");
      venueOne.Save();
      Venue venueTwo = new Venue("Moda Center");
      venueTwo.Save();

      int result = Venue.GetAll().Count;


      Assert.Equal(2, result);
    }

    [Fact]
    public void Save_SavesToDatabase_true()
    {
      
      Venue testVenue = new Venue("Crystal Ballroom");
      testVenue.Save();

      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Find_FindsVenueInDatabase_true()
    {

      Venue testVenue = new Venue("Crystal Ballroom");
      testVenue.Save();

      Venue foundVenue = Venue.Find(testVenue.GetId());

      Assert.Equal(testVenue, foundVenue);
    }

    [Fact]
    public void Update_UpdatesDatabase_True()
    {
      Venue newVenue = new Venue("Rose Garden");
      newVenue.Save();
      newVenue.Update("Moda Center");

      Venue testVenue = new Venue("Moda Center");
      Venue result = Venue.Find(newVenue.GetId());

      Assert.Equal(testVenue, result);
    }

    [Fact]
    public void AddBand_AddsBandToVenue_True()
    {
      Venue newVenue = new Venue("Crystal Ballroom");
      newVenue.Save();
      Band newBand = new Band("Black Keys");
      newBand.Save();

      newVenue.AddBand(newBand);
      List<Band> expected = new List<Band>{newBand};
      List<Band> result = newVenue.GetBands();

      Assert.Equal(expected, result);
    }

    //
    // [Fact]
    // public void Search_SearchVenueByBand()
    // {
    //   Band newBand = new Band("PD Woodhouse");
    //   newBand.Save();
    //
    //   Venue venueOne = new Venue("Mr Wiggles");
    //   venueOne.Save();
    //   venueOne.AddBand(newBand);
    //
    //   Venue venueTwo = new Venue("Mr Muffin");
    //   venueTwo.Save();
    //   venueTwo.AddBand(newBand);
    //
    //   Venue venueThree = new Venue("Mr Floof");
    //   venueThree.Save();
    //
    //   List<Venue> venuesByBand = Venue.SearchBand("PD Woodhouse");
    //   List<Venue> expectedVenues = new List<Venue> {venueOne, venueTwo};
    //
    //   Assert.Equal(expectedVenues, venuesByBand);
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

    [Fact]
    public void Delete_DeletesAssociation_True()
    {
      Venue newVenue = new Venue("Crystal Ballroom");
      newVenue.Save();
      Band newBand = new Band("Black Keys");
      newVenue.Save();
      newVenue.AddBand(newBand);
      newVenue.Delete();

      List<Band> result = newVenue.GetBands();
      List<Band> expected = new List<Band>{};

      Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_Deletes_Venue()
    {
      Venue newVenue = new Venue("Mr Wiggles");

      newVenue.Save();
      newVenue.Delete();

      List<Venue> expected = new List<Venue>{};
      List<Venue> result = Venue.GetAll();

      Assert.Equal(expected, result);
    }

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
