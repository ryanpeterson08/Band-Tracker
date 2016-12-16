using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void ReplacesEqualObjects_True()
    {

      Band bandOne = new Band("Black Keys");
      Band bandTwo = new Band("Black Keys");

      Assert.Equal(bandOne, bandTwo);
    }

    [Fact]
    public void GetAll_true()
    {
      //Arrange
      Band bandOne = new Band("Black Keys");
      bandOne.Save();
      Band bandTwo = new Band("Modest Mouse");
      bandTwo.Save();
      // Act
      int result = Band.GetAll().Count;

      //Assert
      Assert.Equal(2, result);
    }

    [Fact]
    public void Save_SavesToDatabase_true()
    {
      //Arrange
      Band testBand = new Band("Black Keys");
      testBand.Save();
      //Act

      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};
      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Find_FindsBandInDatabase_true()
    {
      //Arrange
      Band testBand = new Band("Black Keys");
      testBand.Save();

      //Act
      Band foundBand = Band.Find(testBand.GetId());

      //Assert
      Assert.Equal(testBand, foundBand);
    }

    [Fact]
    public void AddVenue_AddsVenueToBand_True()
    {
      Venue newVenue = new Venue("Crystal Ballroom");
      newVenue.Save();
      Band newBand = new Band("Black Keys");
      newBand.Save();
      newBand.AddVenue(newVenue);
      List<Venue> expected = new List<Venue>{newVenue};
      List<Venue> result = newBand.GetVenues();

      Assert.Equal(expected, result);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
