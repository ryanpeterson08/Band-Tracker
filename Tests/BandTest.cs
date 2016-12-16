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

    // [Fact]
    // public void Find_FindsBandInDatabase_true()
    // {
    //   //Arrange
    //   Band testBand = new Band("Ryan");
    //   testBand.Save();
    //
    //   //Act
    //   Band foundBand = Band.Find(testBand.GetId());
    //
    //   //Assert
    //   Assert.Equal(testBand, foundBand);
    // }
    //
    // [Fact]
    // public void AddBook_AddsBookToBand_True()
    // {
    //   Book newBook = new Book("Mr Wiggles");
    //   newBook.Save();
    //   Band newBand = new Band("Ryan");
    //   newBand.Save();
    //   newBand.AddBook(newBook);
    //   List<Book> expected = new List<Book>{newBook};
    //   List<Book> result = newBand.GetBooks();
    //
    //   Assert.Equal(expected, result);
    // }

    // [Fact]
    // public void Test_Deletes_Band()
    // {
    //   Band newBand = new Band("Virgina Woolf");
    //
    //   newBand.Save();
    //   newBand.Delete();
    //
    //   List<Band> expected = new List<Band>{};
    //   List<Band> result = Band.GetAll();
    //
    //   Assert.Equal(expected, result);
    //
    // }
    // [Fact]
    // public void Delete_DeletesAssociation_True()
    // {
    //   Book newBook = new Book("Mr Wiggles");
    //   newBook.Save();
    //   Band newBand = new Band("JRR Tolkien");
    //   newBand.Save();
    //   newBand.AddBook(newBook);
    //   newBand.Delete();
    //
    //   List<Book> result = newBand.GetBooks();
    //   List<Book> expected = new List<Book>{};
    //   Assert.Equal(expected, result);
    //
    // }
    // [Fact]
    // public void Update_UpdatesDatabase_True()
    // {
    //   Band newBand = new Band("Frank Herbert");
    //   newBand.Save();
    //   newBand.Update("Frankie Herbert");
    //
    //   Band testBand = new Band("Frankie Herbert");
    //   Band result = Band.Find(newBand.GetId());
    //
    //   Assert.Equal(testBand, result);
    // }

    public void Dispose()
    {
      Band.DeleteAll();
      //Book.DeleteAll();
    }
  }
}
